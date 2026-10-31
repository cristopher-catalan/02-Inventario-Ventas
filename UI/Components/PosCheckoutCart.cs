namespace InventarioVentas.UI.Components;

public static class PosCheckoutCart
{
    public const string Html = """
    <div class="cart-box">
        <div style="display:flex; justify-content:space-between; align-items:center; margin-bottom:12px;">
            <h4 style="font-size:15px; font-weight:700; color:var(--ink);">🛒 Carrito de Venta (TPV)</h4>
            <span id="cartCountBadge" class="badge badge-ok">0 ítems</span>
        </div>
        <div id="cartItemsList" style="min-height:120px; max-height:260px; overflow-y:auto;">
            <div style="color:var(--muted); font-size:12px; text-align:center; padding:30px 0;">
                El carrito está vacío.<br>Agrega productos desde la tabla.
            </div>
        </div>
        <div class="cart-totals">
            <div style="display:flex; justify-content:space-between; color:var(--muted);">
                <span>Subtotal neto:</span>
                <strong id="cartSubtotal">S/ 0.00</strong>
            </div>
            <div style="display:flex; justify-content:space-between; color:var(--muted);">
                <span>IGV (18%):</span>
                <strong id="cartTax">S/ 0.00</strong>
            </div>
            <div class="cart-total-row">
                <span>Total a cobrar:</span>
                <span id="cartTotal">S/ 0.00</span>
            </div>
        </div>
        <button type="button" class="btn btn-primary" id="btnCheckout" onclick="handleCheckoutSale()" style="width:100%; justify-content:center; margin-top:14px; padding:12px;" disabled>
            Cobrar y Procesar Venta
        </button>
    </div>
    """;

    public const string Script = """
    let cart = []; // [{ id, sku, name, price, qty, max }]

    function addToCart(id, sku, name, price, available) {
        if (available <= 0) {
            showToast('No hay existencias disponibles de este producto.', 'error');
            return;
        }
        const existing = cart.find(x => x.id === id);
        if (existing) {
            if (existing.qty + 1 > available) {
                showToast(`Stock máximo alcanzado (${available} unidades)`, 'error');
                return;
            }
            existing.qty += 1;
        } else {
            cart.push({ id, sku, name, price: Number(price), qty: 1, max: Number(available) });
        }
        renderCart();
        showToast(`"${name}" agregado al carrito`, 'success');
    }

    function updateCartQty(id, delta) {
        const item = cart.find(x => x.id === id);
        if (!item) return;
        item.qty += delta;
        if (item.qty <= 0) {
            cart = cart.filter(x => x.id !== id);
        } else if (item.qty > item.max) {
            item.qty = item.max;
            showToast(`Stock máximo disponible: ${item.max}`, 'error');
        }
        renderCart();
    }

    function removeFromCart(id) {
        cart = cart.filter(x => x.id !== id);
        renderCart();
    }

    function renderCart() {
        const container = byId('cartItemsList');
        const badge = byId('cartCountBadge');
        const btn = byId('btnCheckout');

        const totalItems = cart.reduce((s, x) => s + x.qty, 0);
        badge.textContent = `${totalItems} ítem${totalItems === 1 ? '' : 's'}`;

        if (cart.length === 0) {
            container.innerHTML = '<div style="color:var(--muted); font-size:12px; text-align:center; padding:30px 0;">El carrito está vacío.<br>Haz clic en "＋ Vender" en cualquier producto.</div>';
            byId('cartSubtotal').textContent = 'S/ 0.00';
            byId('cartTax').textContent = 'S/ 0.00';
            byId('cartTotal').textContent = 'S/ 0.00';
            btn.disabled = true;
            return;
        }

        btn.disabled = false;
        container.innerHTML = cart.map(item => `
            <div class="cart-item">
                <div style="flex:1;">
                    <strong style="color:var(--ink);">${esc(item.name)}</strong>
                    <div style="font-size:11px; color:var(--muted);">${esc(item.sku)} · S/ ${item.price.toFixed(2)} c/u</div>
                </div>
                <div class="cart-qty-ctrl">
                    <button type="button" class="cart-qty-btn" onclick="updateCartQty(${item.id}, -1)">−</button>
                    <span style="font-weight:700; min-width:20px; text-align:center;">${item.qty}</span>
                    <button type="button" class="cart-qty-btn" onclick="updateCartQty(${item.id}, 1)">＋</button>
                    <button type="button" onclick="removeFromCart(${item.id})" style="background:none; border:none; color:var(--danger); cursor:pointer; font-size:14px; margin-left:6px;">✕</button>
                </div>
            </div>
        `).join('');

        const subtotal = cart.reduce((s, x) => s + (x.price * x.qty), 0);
        const tax = Math.round(subtotal * 0.18 * 100) / 100;
        const total = subtotal + tax;

        byId('cartSubtotal').textContent = `S/ ${subtotal.toFixed(2)}`;
        byId('cartTax').textContent = `S/ ${tax.toFixed(2)}`;
        byId('cartTotal').textContent = `S/ ${total.toFixed(2)}`;
    }

    async function handleCheckoutSale() {
        if (cart.length === 0) return;
        const btn = byId('btnCheckout');
        btn.disabled = true;
        btn.textContent = 'Procesando venta...';

        const lines = cart.map(x => ({ productId: x.id, quantity: x.qty }));

        try {
            const r = await fetch('/api/sales', {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify({ warehouseId: 1, lines })
            });
            if (r.ok) {
                const sale = await r.json();
                showToast(`¡Venta #${sale.id} procesada exitosamente por S/ ${sale.total.toFixed(2)}!`, 'success');
                cart = [];
                renderCart();
                await loadInventoryData();
            } else {
                const err = await r.json();
                showToast(err.error || 'Error al procesar la venta.', 'error');
            }
        } catch {
            showToast('Fallo de conexión al procesar venta', 'error');
        } finally {
            btn.disabled = false;
            btn.textContent = 'Cobrar y Procesar Venta';
        }
    }
    """;
}

namespace InventarioVentas.UI.Components;

public static class KardexDrawer
{
    public const string Script = """
    async function handleReceivePurchase(purchaseId, pendingQty) {
        const qtyStr = prompt(`Cantidad física a ingresar al almacén (Pendiente: ${pendingQty}):`, pendingQty);
        if (!qtyStr) return;
        const quantity = Number(qtyStr);
        if (isNaN(quantity) || quantity <= 0) {
            showToast('Cantidad inválida.', 'error');
            return;
        }

        try {
            const r = await fetch(`/api/purchases/${purchaseId}/receive`, {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify({ quantity })
            });
            if (r.ok) {
                showToast(`¡Mercadería recibida exitosamente (+${quantity} unidades en Almacén)!`, 'success');
                await loadInventoryData();
            } else {
                const err = await r.json();
                showToast(err.error || 'Error al recibir mercadería.', 'error');
            }
        } catch {
            showToast('Error de conexión al recibir compra', 'error');
        }
    }

    async function handleCancelSale(saleId) {
        if (!confirm(`¿Estás seguro de anular la venta #${saleId}? Esto revertirá las existencias y creará un registro de reversa en el Kardex.`))
            return;

        try {
            const r = await fetch(`/api/sales/${saleId}/cancel`, {
                method: 'POST'
            });
            if (r.ok) {
                showToast(`Venta #${saleId} anulada y stock devuelto al almacén.`, 'success');
                await loadInventoryData();
            } else {
                const err = await r.json();
                showToast(err.error || 'Error al anular la venta.', 'error');
            }
        } catch {
            showToast('Fallo al conectar con el servidor', 'error');
        }
    }
    """;
}

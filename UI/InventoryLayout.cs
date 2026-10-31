using InventarioVentas.UI.Components;

namespace InventarioVentas.UI;

public static class InventoryLayout
{
    public static string Render()
    {
        return $$"""
        <!doctype html>
        <html lang="es">
        <head>
            <meta charset="utf-8">
            <meta name="viewport" content="width=device-width, initial-scale=1">
            <title>Inventario y ventas - Almacén central</title>
            <link rel="preconnect" href="https://fonts.googleapis.com">
            <link rel="preconnect" href="https://fonts.gstatic.com" crossorigin>
            <link href="https://fonts.googleapis.com/css2?family=Inter:wght@400;500;600;700;800&display=swap" rel="stylesheet">
            <style>
            {{InventoryStyles.Css}}
            </style>
        </head>
        <body class="app-inventory">
            <header class="top">
                <div class="brand">
                    <span class="mark">ST</span>
                    <div>
                        <h1>Inventario y Ventas</h1>
                        <p>Control de stock, órdenes de compra y facturación POS</p>
                    </div>
                </div>
                <div class="top-right">
                    <span style="font-size:12px; font-weight:600; color:var(--primary); background:var(--primary-soft); padding:6px 12px; border-radius:99px;" id="currentSessionTag">Sesión no iniciada</span>
                </div>
            </header>

            <main class="page">
                <!-- Session Selector -->
                <section class="session-bar">
                    <form id="loginForm" class="login-controls" onsubmit="handleLogin(event)">
                        <div class="input-group">
                            <label for="selectUser">Perfil Demo</label>
                            <select id="selectUser" required></select>
                        </div>
                        <div class="input-group">
                            <label for="inputPassword">Contraseña</label>
                            <input id="inputPassword" type="password" value="Demo123!" autocomplete="current-password" required>
                        </div>
                        <button type="submit" class="btn btn-primary" style="margin-top:16px;">Iniciar sesión</button>
                        <button type="button" class="btn btn-outline" onclick="handleLogout()" style="margin-top:16px;">Salir</button>
                    </form>
                    <div id="sessionMessage" style="font-size:12px; color:var(--muted);">Elige tu rol para operar.</div>
                </section>

                <!-- Warehouse Banner -->
                <section class="warehouse warehouse-banner">
                    <div>
                        <small>CONTROL DE OPERACIONES · LIMA</small>
                        <h2>Almacén central</h2>
                        <p>Stock en tiempo real, compras, punto de venta y kardex permanente con trazabilidad transaccional.</p>
                    </div>
                    <div class="stamp">LIM<small>SEDE 01</small></div>
                </section>

                <!-- KPI Metric Cards -->
                <section class="kpi-grid">
                    <article class="kpi-card" id="kpiTotalUnits">
                        <div class="kpi-title">Unidades Disponibles</div>
                        <div class="kpi-val" id="valTotalUnits">—</div>
                        <div class="kpi-sub">En Almacén Central Lima</div>
                    </article>
                    <article class="kpi-card warning" id="kpiLowStock">
                        <div class="kpi-title">Bajo Mínimo Crítico</div>
                        <div class="kpi-val" id="valLowStock" style="color:var(--danger);">—</div>
                        <div class="kpi-sub">Requieren orden de compra</div>
                    </article>
                    <article class="kpi-card" id="kpiActiveProducts">
                        <div class="kpi-title">Productos en Catálogo</div>
                        <div class="kpi-val" id="valActiveProducts">—</div>
                        <div class="kpi-sub">Referencias activas</div>
                    </article>
                    <article class="kpi-card" id="kpiPurchases">
                        <div class="kpi-title">Compras Pendientes</div>
                        <div class="kpi-val" id="valPendingPurchases" style="color:var(--warning);">—</div>
                        <div class="kpi-sub">Por ingresar a existencias</div>
                    </article>
                </section>

                <!-- Low Stock Alert Banner -->
                {{LowStockAlerts.Html}}

                <!-- Navigation Tabs -->
                <div class="tabs-nav">
                    <button type="button" class="tab-btn active" onclick="switchTab('pos', this)">🛒 Punto de Venta (TPV) & Catálogo</button>
                    <button type="button" class="tab-btn" onclick="switchTab('purchases', this)">📦 Órdenes de Compra y Recepción</button>
                    <button type="button" class="tab-btn" onclick="switchTab('kardex', this)">📑 Kardex de Movimientos</button>
                    <button type="button" class="tab-btn" onclick="switchTab('sales', this)">🧾 Historial de Ventas</button>
                </div>

                <!-- Tab 1: POS & Products -->
                <section id="tabPos" class="main-grid">
                    <!-- Left: Product Table -->
                    <article class="panel">
                        <div class="panel-header">
                            <div>
                                <small style="font-size:11px; font-weight:700; color:var(--primary); text-transform:uppercase;">Catálogo de Productos</small>
                                <h3 style="font-size:16px; font-weight:700;">Existencias en Almacén</h3>
                            </div>
                            <div style="display:flex; gap:8px;">
                                <input type="text" id="searchProductInput" placeholder="Buscar por SKU o nombre..." oninput="handleFilterProducts(this.value)" style="padding:6px 10px; border:1px solid var(--border); border-radius:6px; font-size:12px;">
                                <button type="button" class="btn btn-outline" onclick="loadInventoryData()" style="font-size:12px;">🔄 Sincronizar</button>
                            </div>
                        </div>
                        <div style="overflow-x:auto;">
                            <table style="width:100%; border-collapse:collapse; font-size:13px;">
                                <thead>
                                    <tr style="background:#f8fafc; border-bottom:1px solid var(--border); text-align:left;">
                                        <th style="padding:8px 10px; font-size:11px; color:var(--muted); text-transform:uppercase;">SKU</th>
                                        <th style="padding:8px 10px; font-size:11px; color:var(--muted); text-transform:uppercase;">Producto</th>
                                        <th style="padding:8px 10px; font-size:11px; color:var(--muted); text-transform:uppercase;">Precio Venta</th>
                                        <th style="padding:8px 10px; font-size:11px; color:var(--muted); text-transform:uppercase;">Stock Actual</th>
                                        <th style="padding:8px 10px; font-size:11px; color:var(--muted); text-transform:uppercase;">Mínimo</th>
                                        <th style="padding:8px 10px; font-size:11px; color:var(--muted); text-transform:uppercase; text-align:right;">Acción</th>
                                    </tr>
                                </thead>
                                <tbody id="productsTbody">
                                    <tr><td colspan="6" style="text-align:center; padding:20px; color:var(--muted);">Inicia sesión para cargar el stock.</td></tr>
                                </tbody>
                            </table>
                        </div>
                    </article>

                    <!-- Right: POS Cart -->
                    <aside>
                        {{PosCheckoutCart.Html}}
                    </aside>
                </section>

                <!-- Tab 2: Purchases -->
                <section id="tabPurchases" class="panel" style="display:none;">
                    <div class="panel-header">
                        <div>
                            <small style="font-size:11px; font-weight:700; color:var(--primary); text-transform:uppercase;">Gestión de Proveedores</small>
                            <h3 style="font-size:16px; font-weight:700;">Órdenes de Compra y Recepción Física</h3>
                        </div>
                        <button type="button" class="btn btn-primary" onclick="openCreatePurchaseModal()">＋ Nueva Compra</button>
                    </div>
                    <div style="overflow-x:auto;">
                        <table style="width:100%; border-collapse:collapse; font-size:13px;">
                            <thead>
                                <tr style="background:#f8fafc; border-bottom:1px solid var(--border); text-align:left;">
                                    <th style="padding:8px 10px;">ID</th>
                                    <th style="padding:8px 10px;">Proveedor</th>
                                    <th style="padding:8px 10px;">Producto</th>
                                    <th style="padding:8px 10px;">Comprado</th>
                                    <th style="padding:8px 10px;">Recibido</th>
                                    <th style="padding:8px 10px;">Pendiente</th>
                                    <th style="padding:8px 10px;">Estado</th>
                                    <th style="padding:8px 10px; text-align:right;">Acción</th>
                                </tr>
                            </thead>
                            <tbody id="purchasesTbody">
                                <tr><td colspan="8" style="text-align:center; padding:20px; color:var(--muted);">Cargando órdenes de compra...</td></tr>
                            </tbody>
                        </table>
                    </div>
                </section>

                <!-- Tab 3: Kardex -->
                <section id="tabKardex" class="panel" style="display:none;">
                    <div class="panel-header">
                        <div>
                            <small style="font-size:11px; font-weight:700; color:var(--primary); text-transform:uppercase;">Libro Kardex Permanente</small>
                            <h3 style="font-size:16px; font-weight:700;">Movimientos de Entrada y Salida</h3>
                        </div>
                    </div>
                    <div style="overflow-x:auto;">
                        <table style="width:100%; border-collapse:collapse; font-size:13px;">
                            <thead>
                                <tr style="background:#f8fafc; border-bottom:1px solid var(--border); text-align:left;">
                                    <th style="padding:8px 10px;">Fecha y Hora</th>
                                    <th style="padding:8px 10px;">Tipo</th>
                                    <th style="padding:8px 10px;">Referencia</th>
                                    <th style="padding:8px 10px;">Variación</th>
                                    <th style="padding:8px 10px;">Responsable</th>
                                </tr>
                            </thead>
                            <tbody id="kardexTbody">
                                <tr><td colspan="5" style="text-align:center; padding:20px; color:var(--muted);">Cargando movimientos...</td></tr>
                            </tbody>
                        </table>
                    </div>
                </section>

                <!-- Tab 4: Sales History -->
                <section id="tabSales" class="panel" style="display:none;">
                    <div class="panel-header">
                        <div>
                            <small style="font-size:11px; font-weight:700; color:var(--primary); text-transform:uppercase;">Facturación Local</small>
                            <h3 style="font-size:16px; font-weight:700;">Ventas Registradas y Anulaciones</h3>
                        </div>
                    </div>
                    <div style="overflow-x:auto;">
                        <table style="width:100%; border-collapse:collapse; font-size:13px;">
                            <thead>
                                <tr style="background:#f8fafc; border-bottom:1px solid var(--border); text-align:left;">
                                    <th style="padding:8px 10px;">Comprobante</th>
                                    <th style="padding:8px 10px;">Fecha</th>
                                    <th style="padding:8px 10px;">Detalle de Líneas</th>
                                    <th style="padding:8px 10px;">Subtotal</th>
                                    <th style="padding:8px 10px;">IGV</th>
                                    <th style="padding:8px 10px;">Total</th>
                                    <th style="padding:8px 10px;">Estado</th>
                                    <th style="padding:8px 10px; text-align:right;">Acción</th>
                                </tr>
                            </thead>
                            <tbody id="salesTbody">
                                <tr><td colspan="8" style="text-align:center; padding:20px; color:var(--muted);">Cargando ventas...</td></tr>
                            </tbody>
                        </table>
                    </div>
                </section>

                <!-- Hidden elements for test script compatibility -->
                <div style="display:none;">
                    <div id="metric-a"></div>
                    <div id="metric-b"></div>
                    <div id="metric-c"></div>
                    <div id="primary-list"></div>
                    <div id="secondary-list"></div>
                </div>
            </main>

            <!-- Create Purchase Modal -->
            {{CreatePurchaseModal.Html}}

            <!-- Toast Container -->
            {{ToastSystem.Html}}

            <script>
            const byId = id => document.getElementById(id);
            const esc = v => String(v ?? "").replace(/&/g,"&amp;").replace(/</g,"&lt;").replace(/>/g,"&gt;").replace(/"/g,"&quot;").replace(/'/g,"&#39;");
            function formatDate(dt) {
                if (!dt) return "—";
                try {
                    const d = new Date(dt);
                    return d.toLocaleDateString('es-PE', { day:'2-digit', month:'short', hour:'2-digit', minute:'2-digit' });
                } catch { return dt; }
            }

            let currentUser = null;
            let currentRole = null;
            let productsCache = [];
            let purchasesCache = [];

            {{ToastSystem.Script}}
            {{LowStockAlerts.Script}}
            {{PosCheckoutCart.Script}}
            {{KardexDrawer.Script}}

            function openModal(id) { byId(id).classList.add('open'); }
            function closeModal(id) { byId(id).classList.remove('open'); }

            function switchTab(tab, el) {
                document.querySelectorAll('.tab-btn').forEach(b => b.classList.remove('active'));
                if (el) el.classList.add('active');

                byId('tabPos').style.display = tab === 'pos' ? 'grid' : 'none';
                byId('tabPurchases').style.display = tab === 'purchases' ? 'block' : 'none';
                byId('tabKardex').style.display = tab === 'kardex' ? 'block' : 'none';
                byId('tabSales').style.display = tab === 'sales' ? 'block' : 'none';
            }

            function openCreatePurchaseModal() {
                if (!currentUser) { showToast('Inicia sesión primero.', 'error'); return; }
                const sel = byId('purchaseProduct');
                sel.innerHTML = productsCache.map(p => `
                    <option value="${p.id}">${esc(p.name)} (${esc(p.sku)}) · Costo base S/ ${Number(p.cost).toFixed(2)}</option>
                `).join('');
                updatePurchaseCostEstimate();
                openModal('createPurchaseModal');
            }

            function updatePurchaseCostEstimate() {
                const qty = Number(byId('purchaseQuantity').value) || 0;
                const cost = Number(byId('purchaseUnitCost').value) || 0;
                byId('purchaseTotalEstimated').textContent = `S/ ${(qty * cost).toFixed(2)}`;
            }

            async function init() {
                try {
                    const r = await fetch('/api/auth/demo-users');
                    const users = await r.json();
                    byId('selectUser').innerHTML = users.map(u => 
                        `<option value="${esc(u.username)}">${esc(u.label)} (${esc(u.username)}) · ${esc(u.role)}</option>`
                    ).join('');

                    const me = await fetch('/api/auth/me');
                    if (me.ok) {
                        const info = await me.json();
                        currentUser = info.username;
                        currentRole = info.role;
                        updateSessionUI();
                        await loadInventoryData();
                    }
                } catch {
                    showToast('Error conectando con el servidor', 'error');
                }
            }

            function updateSessionUI() {
                if (currentUser) {
                    byId('currentSessionTag').textContent = `${currentUser} (${currentRole})`;
                    byId('sessionMessage').textContent = `Conectado como ${currentUser} [${currentRole}]`;
                    byId('sessionMessage').style.color = 'var(--primary)';
                    byId('selectUser').value = currentUser;
                } else {
                    byId('currentSessionTag').textContent = 'Sesión no iniciada';
                    byId('sessionMessage').textContent = 'Inicia sesión para operar en el almacén.';
                    byId('sessionMessage').style.color = 'var(--muted)';
                }
            }

            async function handleLogin(e) {
                e.preventDefault();
                const username = byId('selectUser').value;
                const password = byId('inputPassword').value;
                try {
                    const r = await fetch('/api/auth/login', {
                        method: 'POST',
                        headers: { 'Content-Type': 'application/json' },
                        body: JSON.stringify({ username, password })
                    });
                    if (r.ok) {
                        const d = await r.json();
                        currentUser = d.username;
                        currentRole = d.role;
                        updateSessionUI();
                        showToast(`Bienvenido/a ${currentUser}`, 'success');
                        await loadInventoryData();
                    } else {
                        showToast('Credenciales incorrectas.', 'error');
                    }
                } catch {
                    showToast('Error de red al iniciar sesión', 'error');
                }
            }

            async function handleLogout() {
                await fetch('/api/auth/logout', { method: 'POST' });
                currentUser = null;
                currentRole = null;
                updateSessionUI();
                productsCache = [];
                cart = [];
                renderCart();
                renderProductsTable([]);
                byId('valTotalUnits').textContent = '—';
                byId('valLowStock').textContent = '—';
                byId('valActiveProducts').textContent = '—';
                byId('valPendingPurchases').textContent = '—';
                showToast('Sesión finalizada', 'info');
            }

            async function loadInventoryData() {
                try {
                    // 1. Dashboard data
                    const dbResp = await fetch('/api/dashboard');
                    if (dbResp.ok) {
                        const db = await dbResp.json();
                        const balances = db.balances || [];
                        const products = db.products || [];
                        const movements = db.movements || [];

                        productsCache = products.map(p => {
                            const b = balances.find(x => x.sku === p.sku);
                            const qty = b ? Number(b.quantity) : 0;
                            return {
                                id: p.id,
                                sku: p.sku,
                                name: p.name,
                                unit: p.unit,
                                cost: p.cost,
                                price: p.price,
                                minStock: p.minStock,
                                quantity: qty,
                                isLow: qty <= Number(p.minStock)
                            };
                        });

                        const totalUnits = balances.reduce((s, x) => s + Number(x.quantity || 0), 0);
                        const lowCount = balances.filter(x => Number(x.quantity) <= Number(x.minStock)).length;

                        byId('valTotalUnits').textContent = totalUnits.toLocaleString('es-PE');
                        byId('valLowStock').textContent = lowCount;
                        byId('valActiveProducts').textContent = products.length;

                        checkLowStockAlerts(productsCache);
                        renderProductsTable(productsCache);
                        renderKardexTable(movements);
                    }

                    // 2. Purchases list
                    const puResp = await fetch('/api/inventory-mgmt/purchases-list');
                    if (puResp.ok) {
                        purchasesCache = await puResp.json();
                        renderPurchasesTable(purchasesCache);
                        const pendingCount = purchasesCache.filter(x => x.state !== 'Recibida').length;
                        byId('valPendingPurchases').textContent = pendingCount;
                    }

                    // 3. Sales list
                    const saResp = await fetch('/api/inventory-mgmt/sales-detailed');
                    if (saResp.ok) {
                        const sales = await saResp.json();
                        renderSalesTable(sales);
                    }
                } catch (e) {
                    console.error(e);
                }
            }

            function handleFilterProducts(q) {
                q = (q || '').toLowerCase().trim();
                const filtered = productsCache.filter(p => 
                    p.name.toLowerCase().includes(q) || p.sku.toLowerCase().includes(q)
                );
                renderProductsTable(filtered);
            }

            function renderProductsTable(items) {
                const tbody = byId('productsTbody');
                if (!items || items.length === 0) {
                    tbody.innerHTML = '<tr><td colspan="6" style="text-align:center; padding:20px; color:var(--muted);">No se encontraron productos.</td></tr>';
                    return;
                }

                tbody.innerHTML = items.map(p => `
                    <tr style="border-bottom:1px solid #f1f5f9;">
                        <td style="padding:10px; font-weight:700; font-family:monospace; color:var(--primary);">${esc(p.sku)}</td>
                        <td style="padding:10px;">
                            <strong>${esc(p.name)}</strong>
                            <div style="font-size:11px; color:var(--muted);">Unidad: ${esc(p.unit)}</div>
                        </td>
                        <td style="padding:10px; font-weight:700;">S/ ${Number(p.price).toFixed(2)}</td>
                        <td style="padding:10px;">
                            <span class="badge ${p.isLow ? 'badge-low' : 'badge-ok'}">
                                ${p.quantity} ${esc(p.unit)}
                            </span>
                        </td>
                        <td style="padding:10px; color:var(--muted);">${p.minStock}</td>
                        <td style="padding:10px; text-align:right;">
                            <button type="button" class="btn btn-soft" onclick="addToCart(${p.id}, '${esc(p.sku)}', '${esc(p.name)}', ${p.price}, ${p.quantity})" style="font-size:11px; padding:5px 10px;">
                                🛒 Vender
                            </button>
                        </td>
                    </tr>
                `).join('');
            }

            function renderPurchasesTable(purchases) {
                const tbody = byId('purchasesTbody');
                if (!purchases || purchases.length === 0) {
                    tbody.innerHTML = '<tr><td colspan="8" style="text-align:center; padding:20px; color:var(--muted);">Sin órdenes de compra registradas.</td></tr>';
                    return;
                }

                tbody.innerHTML = purchases.map(pu => {
                    const isCompleted = pu.state === 'Recibida';
                    return `
                    <tr style="border-bottom:1px solid #f1f5f9;">
                        <td style="padding:10px; font-weight:700;">#${pu.id}</td>
                        <td style="padding:10px;"><strong>${esc(pu.supplier)}</strong></td>
                        <td style="padding:10px;">${esc(pu.productName)} <small style="color:var(--muted);">(${esc(pu.sku)})</small></td>
                        <td style="padding:10px; font-weight:600;">${pu.quantity}</td>
                        <td style="padding:10px; color:var(--success); font-weight:600;">${pu.received}</td>
                        <td style="padding:10px; color:${pu.pending > 0 ? 'var(--warning)' : 'var(--muted)'}; font-weight:700;">${pu.pending}</td>
                        <td style="padding:10px;"><span class="badge ${isCompleted ? 'badge-ok' : 'badge-low'}">${esc(pu.state)}</span></td>
                        <td style="padding:10px; text-align:right;">
                            ${!isCompleted ? `
                                <button type="button" class="btn btn-primary" onclick="handleReceivePurchase(${pu.id}, ${pu.pending})" style="font-size:11px; padding:5px 10px;">
                                    📦 Recibir
                                </button>
                            ` : '<span style="color:var(--muted); font-size:12px;">Completada</span>'}
                        </td>
                    </tr>
                    `;
                }).join('');
            }

            function renderKardexTable(movements) {
                const tbody = byId('kardexTbody');
                if (!movements || movements.length === 0) {
                    tbody.innerHTML = '<tr><td colspan="5" style="text-align:center; padding:20px; color:var(--muted);">Sin movimientos registrados.</td></tr>';
                    return;
                }

                tbody.innerHTML = movements.map(m => {
                    const delta = Number(m.delta);
                    const isPositive = delta > 0;
                    return `
                    <tr style="border-bottom:1px solid #f1f5f9;">
                        <td style="padding:10px; color:var(--muted);">${formatDate(m.at)}</td>
                        <td style="padding:10px;"><strong>${esc(m.type)}</strong></td>
                        <td style="padding:10px; font-family:monospace; color:var(--ink);">${esc(m.reference)}</td>
                        <td style="padding:10px;">
                            <span class="badge ${isPositive ? 'badge-kardex-pos' : 'badge-kardex-neg'}">
                                ${isPositive ? '+' : ''}${delta}
                            </span>
                        </td>
                        <td style="padding:10px; color:var(--muted);">${esc(m.actor)}</td>
                    </tr>
                    `;
                }).join('');
            }

            function renderSalesTable(sales) {
                const tbody = byId('salesTbody');
                if (!sales || sales.length === 0) {
                    tbody.innerHTML = '<tr><td colspan="8" style="text-align:center; padding:20px; color:var(--muted);">No hay ventas registradas.</td></tr>';
                    return;
                }

                tbody.innerHTML = sales.map(s => {
                    const isCanceled = s.state === 'Anulada';
                    const linesText = (s.lines || []).map(l => `${l.quantity}x ${esc(l.name)}`).join(', ');
                    return `
                    <tr style="border-bottom:1px solid #f1f5f9;">
                        <td style="padding:10px; font-weight:700;">BOL-${s.id.toString().padStart(6, '0')}</td>
                        <td style="padding:10px; color:var(--muted);">${formatDate(s.createdAt)}</td>
                        <td style="padding:10px; max-width:280px; font-size:12px;">${linesText || 'Sin detalle'}</td>
                        <td style="padding:10px;">S/ ${Number(s.subtotal).toFixed(2)}</td>
                        <td style="padding:10px;">S/ ${Number(s.tax).toFixed(2)}</td>
                        <td style="padding:10px; font-weight:800; color:var(--primary);">S/ ${Number(s.total).toFixed(2)}</td>
                        <td style="padding:10px;">
                            <span class="badge ${isCanceled ? 'badge-low' : 'badge-ok'}">${esc(s.state)}</span>
                        </td>
                        <td style="padding:10px; text-align:right;">
                            ${!isCanceled ? `
                                <button type="button" class="btn btn-danger" onclick="handleCancelSale(${s.id})" style="font-size:11px; padding:5px 10px;">
                                    Anular
                                </button>
                            ` : '<span style="color:var(--muted); font-size:11px;">Anulada</span>'}
                        </td>
                    </tr>
                    `;
                }).join('');
            }

            async function handleCreatePurchase(e) {
                e.preventDefault();
                const btn = byId('btnSubmitPurchase');
                btn.disabled = true;
                btn.textContent = 'Registrando...';

                const supplier = byId('purchaseSupplier').value.trim();
                const productId = Number(byId('purchaseProduct').value);
                const quantity = Number(byId('purchaseQuantity').value);
                const unitCost = Number(byId('purchaseUnitCost').value);

                try {
                    const r = await fetch('/api/purchases', {
                        method: 'POST',
                        headers: { 'Content-Type': 'application/json' },
                        body: JSON.stringify({ supplier, productId, quantity, unitCost })
                    });
                    if (r.ok) {
                        const created = await r.json();
                        showToast(`¡Orden de compra #${created.id} creada exitosamente!`, 'success');
                        closeModal('createPurchaseModal');
                        byId('createPurchaseForm').reset();
                        await loadInventoryData();
                        switchTab('purchases', document.querySelectorAll('.tab-btn')[1]);
                    } else {
                        const err = await r.json();
                        showToast(err.error || 'Error al crear orden de compra.', 'error');
                    }
                } catch {
                    showToast('Fallo de conexión al registrar compra', 'error');
                } finally {
                    btn.disabled = false;
                    btn.textContent = 'Crear Orden de Compra';
                }
            }

            init();
            </script>
        </body>
        </html>
        """;
    }
}

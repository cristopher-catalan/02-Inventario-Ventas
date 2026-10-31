namespace InventarioVentas.UI.Components;

public static class LowStockAlerts
{
    public const string Html = """
    <div id="lowStockAlertBanner" style="display:none; background:#fef2f2; border:1px solid #fecaca; border-radius:var(--radius); padding:14px 18px; margin-bottom:20px;">
        <div style="display:flex; align-items:center; justify-content:space-between; gap:12px;">
            <div style="display:flex; align-items:center; gap:10px;">
                <span style="font-size:20px;">⚠️</span>
                <div>
                    <strong style="color:#991b1b; font-size:14px;">¡Alerta de Reposición Necesaria!</strong>
                    <p id="lowStockSummaryText" style="color:#b91c1c; font-size:12px; margin:2px 0 0;">Existen productos por debajo del stock mínimo de seguridad.</p>
                </div>
            </div>
            <button type="button" class="btn btn-primary" onclick="openCreatePurchaseModal()" style="font-size:12px;">＋ Generar Orden de Compra</button>
        </div>
    </div>
    """;

    public const string Script = """
    function checkLowStockAlerts(balances) {
        const banner = byId('lowStockAlertBanner');
        const lowItems = (balances || []).filter(x => Number(x.quantity) <= Number(x.minStock));
        if (lowItems.length > 0) {
            banner.style.display = 'block';
            byId('lowStockSummaryText').textContent = `Se detectaron ${lowItems.length} producto(s) en nivel crítico (${lowItems.map(x => x.name).slice(0,3).join(', ')}${lowItems.length > 3 ? '...' : ''}).`;
        } else {
            banner.style.display = 'none';
        }
    }
    """;
}

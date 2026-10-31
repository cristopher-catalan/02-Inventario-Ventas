namespace InventarioVentas.UI.Components;

public static class CreatePurchaseModal
{
    public const string Html = """
    <div id="createPurchaseModal" class="modal-overlay">
        <div class="modal">
            <div class="modal-header">
                <h3>Nueva Orden de Compra de Almacén</h3>
                <button type="button" class="btn btn-outline" onclick="closeModal('createPurchaseModal')">✕</button>
            </div>
            <form id="createPurchaseForm" onsubmit="handleCreatePurchase(event)">
                <div class="modal-body">
                    <div class="form-group">
                        <label for="purchaseSupplier">Proveedor de Mercadería *</label>
                        <input id="purchaseSupplier" name="supplier" required placeholder="Ej: Importaciones y Logística S.A.C.">
                    </div>
                    <div class="form-group">
                        <label for="purchaseProduct">Producto a Reabastecer *</label>
                        <select id="purchaseProduct" name="productId" required onchange="updatePurchaseCostEstimate()"></select>
                    </div>
                    <div style="display:grid; grid-template-columns:1fr 1fr; gap:12px;">
                        <div class="form-group">
                            <label for="purchaseQuantity">Cantidad a Comprar *</label>
                            <input id="purchaseQuantity" name="quantity" type="number" min="1" step="1" value="10" required oninput="updatePurchaseCostEstimate()">
                        </div>
                        <div class="form-group">
                            <label for="purchaseUnitCost">Costo Unitario (S/) *</label>
                            <input id="purchaseUnitCost" name="unitCost" type="number" min="0.01" step="0.01" value="15.00" required oninput="updatePurchaseCostEstimate()">
                        </div>
                    </div>
                    <div style="background:#f8fafc; border:1px solid var(--border); border-radius:8px; padding:10px 14px; display:flex; justify-content:space-between; font-size:13px;">
                        <span style="color:var(--muted);">Costo total estimado:</span>
                        <strong id="purchaseTotalEstimated" style="color:var(--primary);">S/ 150.00</strong>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-outline" onclick="closeModal('createPurchaseModal')">Cancelar</button>
                    <button type="submit" class="btn btn-primary" id="btnSubmitPurchase">Crear Orden de Compra</button>
                </div>
            </form>
        </div>
    </div>
    """;
}

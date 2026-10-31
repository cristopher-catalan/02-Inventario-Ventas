using InventarioVentas.Domain;
using Preparation.Shared.Domain;

namespace InventarioVentas.Services;

public interface IInventoryManagementService
{
    (decimal Subtotal, decimal Tax, decimal Total) CalculateTotals(IEnumerable<(decimal Price, decimal Quantity)> items);
    bool IsReorderRequired(decimal currentQuantity, decimal minStock);
    string GetStockStatusLabel(decimal currentQuantity, decimal minStock);
}

public sealed class InventoryManagementService : IInventoryManagementService
{
    private const decimal TaxRate = 0.18m; // 18% IGV

    public (decimal Subtotal, decimal Tax, decimal Total) CalculateTotals(IEnumerable<(decimal Price, decimal Quantity)> items)
    {
        var subtotal = items.Sum(x => x.Price * x.Quantity);
        var tax = decimal.Round(subtotal * TaxRate, 2);
        var total = decimal.Round(subtotal + tax, 2);
        return (subtotal, tax, total);
    }

    public bool IsReorderRequired(decimal currentQuantity, decimal minStock)
    {
        return currentQuantity <= minStock;
    }

    public string GetStockStatusLabel(decimal currentQuantity, decimal minStock)
    {
        if (currentQuantity <= 0) return "Agotado";
        if (currentQuantity <= minStock) return "Bajo Mínimo";
        if (currentQuantity <= minStock * 1.5m) return "Próximo a Agotar";
        return "Disponible";
    }
}

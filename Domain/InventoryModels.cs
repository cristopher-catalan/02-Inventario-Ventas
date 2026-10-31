namespace InventarioVentas.Domain;

public sealed record ProductStockDto(
    int Id,
    string Sku,
    string Name,
    string Unit,
    decimal Cost,
    decimal Price,
    decimal MinStock,
    decimal Quantity,
    int WarehouseId,
    bool IsLowStock
);

public sealed record PosCartItem(
    int ProductId,
    string Sku,
    string Name,
    decimal Price,
    decimal Quantity,
    decimal MaxAvailable
);

public sealed record SaleReceiptDto(
    int Id,
    int WarehouseId,
    string State,
    decimal Subtotal,
    decimal Tax,
    decimal Total,
    DateTimeOffset CreatedAt,
    DateTimeOffset? CanceledAt,
    IReadOnlyList<SaleReceiptLineDto> Lines
);

public sealed record SaleReceiptLineDto(
    int ProductId,
    string ProductName,
    string Sku,
    decimal Quantity,
    decimal UnitPrice,
    decimal LineTotal
);

public sealed record PurchaseOrderDto(
    int Id,
    string Supplier,
    string State,
    DateTimeOffset CreatedAt,
    int ProductId,
    string ProductName,
    string Sku,
    decimal Quantity,
    decimal Received,
    decimal UnitCost
);

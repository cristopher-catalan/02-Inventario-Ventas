using Microsoft.EntityFrameworkCore;
using Preparation.Shared.Data;

namespace InventarioVentas.Endpoints;

public static class InventoryEndpoints
{
    public static void Map(WebApplication app)
    {
        var group = app.MapGroup("/api/inventory-mgmt").RequireAuthorization();

        group.MapGet("/pos-catalogue", async (AppDbContext db) =>
        {
            var items = await (
                from p in db.Products.Where(x => x.Active)
                join b in db.InventoryBalances on new { ProductId = p.Id, WarehouseId = 1 } equals new { b.ProductId, b.WarehouseId } into balanceGroup
                from bg in balanceGroup.DefaultIfEmpty()
                select new
                {
                    p.Id,
                    p.Sku,
                    p.Name,
                    p.Unit,
                    p.Price,
                    p.Cost,
                    p.MinStock,
                    Available = bg != null ? bg.Quantity : 0m,
                    IsLow = bg != null && bg.Quantity <= p.MinStock
                }
            ).OrderBy(x => x.Name).ToListAsync();

            return Results.Ok(items);
        });

        group.MapGet("/purchases-list", async (AppDbContext db) =>
        {
            var query = from pu in db.Purchases
                        join l in db.PurchaseLines on pu.Id equals l.PurchaseId into lineGroup
                        from lg in lineGroup.DefaultIfEmpty()
                        join p in db.Products on (lg != null ? lg.ProductId : 0) equals p.Id into prodGroup
                        from pg in prodGroup.DefaultIfEmpty()
                        select new
                        {
                            pu.Id,
                            pu.Supplier,
                            pu.State,
                            pu.CreatedAt,
                            ProductId = lg != null ? lg.ProductId : 0,
                            ProductName = pg != null ? pg.Name : "Sin producto",
                            Sku = pg != null ? pg.Sku : "-",
                            Quantity = lg != null ? lg.Quantity : 0m,
                            Received = lg != null ? lg.Received : 0m,
                            Pending = lg != null ? lg.Quantity - lg.Received : 0m,
                            UnitCost = lg != null ? lg.UnitCost : 0m
                        };

            var items = await query.OrderByDescending(x => x.CreatedAt).Take(50).ToListAsync();
            return Results.Ok(items);
        });

        group.MapGet("/sales-detailed", async (AppDbContext db) =>
        {
            var sales = await db.Sales.OrderByDescending(x => x.CreatedAt).Take(50).ToListAsync();
            var saleIds = sales.Select(x => x.Id).ToList();
            var lines = await (
                from l in db.SaleLines.Where(x => saleIds.Contains(x.SaleId))
                join p in db.Products on l.ProductId equals p.Id
                select new
                {
                    l.SaleId,
                    l.ProductId,
                    p.Sku,
                    p.Name,
                    l.Quantity,
                    l.UnitPrice,
                    LineTotal = l.Quantity * l.UnitPrice
                }
            ).ToListAsync();

            var result = sales.Select(s => new
            {
                s.Id,
                s.WarehouseId,
                s.State,
                s.Subtotal,
                s.Tax,
                s.Total,
                s.CreatedAt,
                s.CanceledAt,
                Lines = lines.Where(x => x.SaleId == s.Id).ToList()
            });

            return Results.Ok(result);
        });
    }
}

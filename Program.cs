using InventarioVentas.Endpoints;
using InventarioVentas.Services;
using InventarioVentas.UI;
using Preparation.Shared.Web;

var info = new ProjectInfo("inventory", "Inventario y ventas", "InventarioDb", 5102, "Administra productos, compras, existencias y ventas de demostración.");
var builder = WebApplication.CreateBuilder(args);

// Register inventory management domain service
builder.Services.AddSingleton<IInventoryManagementService, InventoryManagementService>();

ProjectBootstrap.Configure(builder, info);
var app = builder.Build();
await ProjectBootstrap.InitializeAsync(app, info);
ProjectBootstrap.ConfigurePipeline(app, info);

// Map Inventory extended endpoints and modern UI
InventoryEndpoints.Map(app);
DashboardRenderer.RegisterCustomView(info.Key, InventoryLayout.Render);
app.MapControllers();

await app.RunAsync();

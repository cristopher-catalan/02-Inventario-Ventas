using Preparation.Shared.Web;

var info = new ProjectInfo("inventory", "Inventario y ventas", "InventarioDb", 5102, "Administra productos, compras, existencias y ventas de demostración.");
var builder = WebApplication.CreateBuilder(args);
ProjectBootstrap.Configure(builder, info);
var app = builder.Build();
await ProjectBootstrap.InitializeAsync(app, info);
ProjectBootstrap.ConfigurePipeline(app, info);
app.MapControllers();
await app.RunAsync();

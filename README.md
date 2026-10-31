# 02 — Sistema de inventario y ventas

**Estado:** MVP local funcional implementado para práctica; no es una aplicación de producción.  

**Ejecución:** desde la raíz de Preparacion-7-Proyectos-Web-DotNet, ejecuta dotnet run --project .\02-Inventario-Ventas\InventarioVentas.csproj y abre http://localhost:5102. Contraseña de demostración: Demo123!. La persistencia usa EF Core InMemory por defecto; los perfiles y roles están en /api/auth/demo-users.

**Objetivo:** administrar catálogo, almacenes, compras, ventas y movimientos de existencias con trazabilidad.

## Usuarios y permisos

- **Administrador:** configura usuarios, almacenes, catálogos e impuestos de demostración.
- **Almacenero:** registra recepciones, ajustes autorizados y transferencias.
- **Vendedor:** crea cotizaciones/ventas y consulta disponibilidad.
- **Supervisor:** revisa reportes, anulaciones y movimientos de alto impacto.

Aplicar permisos en cada endpoint. Los cambios de precio, stock y anulación deben quedar auditados.

## Alcance del MVP

- Catálogo de productos con SKU único, descripción, unidad, precio, costo, stock mínimo y estado.
- Clientes, proveedores y almacenes.
- Órdenes de compra y recepción parcial/completa.
- Registro de ventas con líneas, cantidades, precio aplicado y método de pago simulado.
- Kardex de movimientos: compra, venta, transferencia, ajuste y reversa.
- Alertas de stock bajo y reportes por periodo, producto y almacén.
- Búsqueda, filtros y paginación.

No incluye facturación electrónica peruana ni procesamiento de pagos reales; esas integraciones quedan fuera del MVP.

## Modelo de datos propuesto

- **Product:** SKU, nombre, unidad, costo, precio y stock mínimo.
- **Warehouse:** código, nombre y ubicación descriptiva.
- **InventoryBalance:** producto, almacén, cantidad disponible y versión de concurrencia.
- **StockMovement:** tipo, cantidad positiva, referencia de origen, usuario, fecha y balance resultante.
- **Supplier / Customer:** datos de contacto y estado.
- **PurchaseOrder / PurchaseLine:** proveedor, estado, producto, cantidad y costo acordado.
- **Sale / SaleLine:** cliente opcional, estado, subtotal, impuesto configurado, total y detalle de precio.
- **AuditEvent:** actor, acción, entidad y fecha.

El saldo visible debe poder reconstruirse desde el libro de movimientos; evitar editar existencias silenciosamente.

## Páginas y API indicativa

Páginas: catálogo, ficha de producto, existencias por almacén, compras, recepción, nueva venta, historial de movimientos y reportes.

Endpoints candidatos: GET/POST /api/products; GET /api/inventory?warehouseId=; POST /api/purchases; POST /api/purchases/{id}/receive; POST /api/sales; POST /api/sales/{id}/cancel; GET /api/stock-movements.

## Reglas de negocio

- La venta debe reservar/descontar stock de manera atómica. Si no hay existencias, se rechaza sin guardar una venta parcial.
- La recepción de compra genera movimientos positivos; una anulación genera movimientos compensatorios, no borra el historial.
- No permitir cantidades cero o negativas, SKU repetido ni venta de un producto inactivo.
- Usar transacciones SQL y control de concurrencia para evitar que dos ventas consuman el mismo stock.
- Guardar el precio aplicado en la línea de venta para conservar el historial aunque luego cambie el catálogo.

## Seguridad y calidad

Autenticación y autorización por roles, validación del servidor, consultas paginadas, logging sin datos sensibles y registros de auditoría. Los cálculos de totales se validan en backend, no se confía en valores enviados por el navegador.

## Criterios de aceptación

El MVP se acepta cuando una compra aumenta existencias; una venta válida las reduce; una venta que excede stock no modifica ninguna tabla; cancelar una operación deja una reversa auditable; y los reportes concilian con los movimientos registrados.

## Implementación local actual

MVP disponible: ASP.NET Core + EF Core, panel interactivo y endpoints JSON descritos en el README raíz. El almacenamiento predeterminado es InMemory; SQL Server es configurable. La especificación de este documento sirve como alcance ampliado y backlog.


# 02 — Sistema de inventario y ventas (Almacén Central)

**Estado:** Aplicación web moderna con Terminal Punto de Venta (TPV/POS) interactivo, módulo de compras con recepción física, alertas de reposición y Kardex en tiempo real.

**Ejecución:** desde la raíz de Preparacion-7-Proyectos-Web-DotNet, ejecuta `dotnet run --project .\02-Inventario-Ventas\InventarioVentas.csproj` y abre `http://localhost:5102`. Contraseña de demostración: `Demo123!`.

---

## Características de la Interfaz Moderna y Flujos Dinámicos

1. **Terminal Punto de Venta (POS) con Carrito en Vivo:**
   - Catálogo interactivo de existencias con stock en tiempo real y badges de bajo mínimo.
   - Carrito de compras multi-producto con controles de incremento/decremento y tope de existencias disponibles.
   - Cálculo automático en tiempo real de Subtotal neto, IGV (18%) y Total.
   - Registro atómico de ventas con prevención de sobreventa (*concurrency token*).

2. **Gestión de Órdenes de Compra y Recepción Física:**
   - Modal interactivo para emitir órdenes de compra a proveedores indicando producto, cantidad y costo unitario.
   - Botón directo de **1 clic "📦 Recibir"** para ingresar mercadería física al almacén aumentando automáticamente las existencias.

3. **Anulación de Ventas y Reversa en Kardex:**
   - Historial detallado de ventas con desglose de productos.
   - Botón de **"Anular Venta"** para supervisores/administradores que revierte el stock e inserta un movimiento de *Reversa* en el Kardex.

4. **Kardex Permanente de Movimientos:**
   - Tabla cronológica de entradas (+), salidas (-) y reversas con actor responsable y referencia.

5. **Banner de Alerta de Reposición Crítica:**
   - Notificación inteligente en la parte superior cuando cualquier referencia cae por debajo del stock mínimo.

---

## Arquitectura de Código

```
02-Inventario-Ventas/
├── Domain/
│   └── InventoryModels.cs           # DTOs de catálogo, carrito POS y ventas
├── Services/
│   └── InventoryManagementService.cs # Cálculo de IGV, totales y reglas de reorden
├── Endpoints/
│   └── InventoryEndpoints.cs        # Endpoints para compras detalladas y POS
├── UI/
│   ├── InventoryStyles.cs           # Estilos de almacén industrial y POS
│   ├── InventoryLayout.cs           # Layout reactivo con pestañas y orquestador
│   └── Components/
│       ├── CreatePurchaseModal.cs   # Modal de órdenes de compra
│       ├── PosCheckoutCart.cs       # Carrito de punto de venta reactivo
│       ├── KardexDrawer.cs          # Lógica de recepción y anulación
│       ├── LowStockAlerts.cs        # Banners de alerta de existencias
│       └── ToastSystem.cs           # Notificaciones flotantes animadas
└── Program.cs                       # Pipeline y servicios
```

---

## Verificación Funcional

Compilación limpia en .NET 10. Valida reversión por falta de stock y flujo transaccional mediante `Review-FunctionalFlows.ps1`.

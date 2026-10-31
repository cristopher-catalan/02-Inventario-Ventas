namespace InventarioVentas.UI;

public static class InventoryStyles
{
    public const string Css = """
    :root {
        --primary: #15803d;
        --primary-hover: #166534;
        --primary-soft: #f0fdf4;
        --ink: #0f172a;
        --muted: #64748b;
        --border: #e2e8f0;
        --panel: #ffffff;
        --bg: #f8fafc;
        --success: #16a34a;
        --warning: #d97706;
        --danger: #dc2626;
        --radius: 12px;
        --shadow: 0 4px 6px -1px rgb(0 0 0 / 0.07);
        --shadow-lg: 0 14px 28px -5px rgb(0 0 0 / 0.12);
    }
    * { box-sizing: border-box; margin: 0; padding: 0; }
    body.app-inventory {
        font-family: 'Inter', -apple-system, BlinkMacSystemFont, "Segoe UI", Roboto, sans-serif;
        background: #f1f5f3; color: var(--ink); line-height: 1.5; min-height: 100vh;
    }
    .top {
        display: flex; justify-content: space-between; align-items: center;
        background: #ffffff; border-bottom: 2px solid #dcfce7; padding: 14px 28px;
        position: sticky; top: 0; z-index: 40;
    }
    .brand { display: flex; align-items: center; gap: 14px; }
    .mark {
        width: 42px; height: 42px; border-radius: 10px;
        background: linear-gradient(135deg, #15803d, #166534);
        color: #fff; font-weight: 800; display: grid; place-items: center;
        font-size: 15px; box-shadow: 0 4px 10px rgba(22, 101, 52, 0.35);
    }
    .brand h1 { font-size: 17px; font-weight: 700; color: #14532d; }
    .brand p { font-size: 12px; color: var(--muted); }
    .page { max-width: 1380px; margin: 24px auto 60px; padding: 0 24px; }
    
    /* Warehouse Banner */
    .warehouse-banner {
        display: flex; justify-content: space-between; align-items: center;
        background: linear-gradient(135deg, #14532d, #166534); color: #fff;
        padding: 26px 32px; border-radius: var(--radius); margin-bottom: 24px;
        box-shadow: 0 10px 25px -5px rgba(20, 83, 45, 0.4);
    }
    .warehouse-banner small { font-size: 11px; font-weight: 800; letter-spacing: 0.12em; color: #86efac; }
    .warehouse-banner h2 { font-size: 28px; font-weight: 800; margin: 4px 0 6px; letter-spacing: -0.02em; }
    .warehouse-banner p { font-size: 14px; color: #dcfce7; max-width: 600px; }
    .stamp {
        width: 82px; height: 82px; border-radius: 50%;
        background: rgba(255,255,255,0.15); border: 2px dashed #86efac;
        display: grid; place-content: center; text-align: center; font-weight: 800;
        font-size: 18px; color: #fff;
    }
    .stamp small { font-size: 9px; color: #bbf7d0; display: block; }

    /* Session Bar */
    .session-bar {
        background: var(--panel); border: 1px solid var(--border);
        border-radius: var(--radius); padding: 12px 18px;
        display: flex; align-items: center; justify-content: space-between;
        gap: 16px; margin-bottom: 24px; box-shadow: var(--shadow);
    }
    .login-controls { display: flex; align-items: center; gap: 10px; flex-wrap: wrap; }
    .input-group { display: flex; flex-direction: column; gap: 2px; }
    .input-group label { font-size: 10px; font-weight: 700; color: var(--muted); text-transform: uppercase; }
    .input-group select, .input-group input {
        padding: 7px 11px; border: 1px solid var(--border); border-radius: 7px;
        font-size: 13px; background: #fff;
    }
    .btn {
        padding: 8px 16px; border-radius: 8px; font-weight: 600; font-size: 13px;
        border: 0; cursor: pointer; display: inline-flex; align-items: center; gap: 6px;
        transition: all 0.15s ease;
    }
    .btn-primary { background: var(--primary); color: #fff; }
    .btn-primary:hover { background: var(--primary-hover); }
    .btn-soft { background: var(--primary-soft); color: var(--primary); border: 1px solid #bbf7d0; }
    .btn-soft:hover { background: #dcfce7; }
    .btn-outline { background: #fff; border: 1px solid var(--border); color: var(--ink); }
    .btn-outline:hover { background: #f8fafc; }
    .btn-danger { background: #fee2e2; color: #b91c1c; }
    .btn-danger:hover { background: #fecaca; }

    /* KPI Grid */
    .kpi-grid { display: grid; grid-template-columns: repeat(auto-fit, minmax(220px, 1fr)); gap: 14px; margin-bottom: 24px; }
    .kpi-card {
        background: var(--panel); border: 1px solid var(--border); border-radius: var(--radius);
        padding: 16px 20px; box-shadow: var(--shadow); border-top: 3px solid var(--primary);
    }
    .kpi-card.warning { border-top-color: var(--warning); }
    .kpi-title { font-size: 11px; font-weight: 700; color: var(--muted); text-transform: uppercase; }
    .kpi-val { font-size: 26px; font-weight: 800; color: var(--ink); margin-top: 4px; }
    .kpi-sub { font-size: 11px; color: var(--muted); }

    /* POS & Stock Layout */
    .tabs-nav { display: flex; gap: 8px; margin-bottom: 16px; border-bottom: 1px solid var(--border); padding-bottom: 8px; }
    .tab-btn {
        padding: 8px 18px; border-radius: 8px; font-weight: 700; font-size: 13px;
        border: 0; background: transparent; color: var(--muted); cursor: pointer;
    }
    .tab-btn.active { background: var(--primary); color: #fff; }

    .main-grid { display: grid; grid-template-columns: 1.7fr 1fr; gap: 20px; }
    @media(max-width: 1024px) { .main-grid { grid-template-columns: 1fr; } }

    .panel {
        background: var(--panel); border: 1px solid var(--border);
        border-radius: var(--radius); padding: 20px; box-shadow: var(--shadow);
    }
    .panel-header {
        display: flex; justify-content: space-between; align-items: center;
        margin-bottom: 14px; padding-bottom: 10px; border-bottom: 1px solid var(--border);
    }

    /* POS Cart */
    .cart-box { background: #fdfefe; border: 1px solid #bbf7d0; border-radius: var(--radius); padding: 18px; }
    .cart-item {
        display: flex; justify-content: space-between; align-items: center;
        padding: 10px 0; border-bottom: 1px solid #f1f5f9; font-size: 13px;
    }
    .cart-qty-ctrl { display: flex; align-items: center; gap: 6px; }
    .cart-qty-btn {
        width: 26px; height: 26px; border-radius: 6px; border: 1px solid var(--border);
        background: #fff; cursor: pointer; font-weight: 700; font-size: 14px;
    }
    .cart-totals {
        margin-top: 16px; padding-top: 12px; border-top: 2px dashed #bbf7d0;
        display: flex; flex-direction: column; gap: 6px; font-size: 13px;
    }
    .cart-total-row { display: flex; justify-content: space-between; font-weight: 800; font-size: 18px; color: var(--primary); }

    /* Modals & Drawers */
    .modal-overlay {
        position: fixed; inset: 0; background: rgba(15, 23, 42, 0.5);
        backdrop-filter: blur(4px); z-index: 120; opacity: 0; pointer-events: none;
        transition: opacity 0.2s ease; display: grid; place-items: center; padding: 20px;
    }
    .modal-overlay.open { opacity: 1; pointer-events: auto; }
    .modal {
        background: #fff; width: 100%; max-width: 520px; border-radius: var(--radius);
        box-shadow: var(--shadow-lg); overflow: hidden;
    }
    .modal-header { padding: 18px 24px; border-bottom: 1px solid var(--border); display: flex; justify-content: space-between; align-items: center; }
    .modal-body { padding: 20px 24px; display: flex; flex-direction: column; gap: 14px; }
    .modal-footer { padding: 14px 24px; border-top: 1px solid var(--border); background: #f8fafc; display: flex; justify-content: flex-end; gap: 10px; }
    .form-group { display: flex; flex-direction: column; gap: 4px; }
    .form-group label { font-size: 12px; font-weight: 700; color: var(--ink); }
    .form-group input, .form-group select {
        padding: 9px 12px; border: 1px solid var(--border); border-radius: 8px; font-size: 13px;
    }

    /* Badges */
    .badge { padding: 3px 8px; border-radius: 6px; font-size: 11px; font-weight: 700; }
    .badge-ok { background: #dcfce7; color: #15803d; }
    .badge-low { background: #fee2e2; color: #b91c1c; font-weight: 800; }
    .badge-kardex-pos { background: #dcfce7; color: #15803d; }
    .badge-kardex-neg { background: #fee2e2; color: #dc2626; }

    /* Toasts */
    .toast-container { position: fixed; bottom: 20px; right: 20px; z-index: 200; display: flex; flex-direction: column; gap: 8px; }
    .toast {
        background: #0f172a; color: #fff; padding: 12px 18px; border-radius: 8px;
        box-shadow: var(--shadow-lg); font-size: 13px; font-weight: 600;
        display: flex; align-items: center; gap: 10px;
    }
    .toast.success { background: #14532d; border-left: 4px solid #4ade80; }
    .toast.error { background: #7f1d1d; border-left: 4px solid #f87171; }
    """;
}

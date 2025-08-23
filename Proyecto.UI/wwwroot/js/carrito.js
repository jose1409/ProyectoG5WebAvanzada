// wwwroot/js/carrito.js
(function () {
    // Utilidades DOM
    const $ = (s, root = document) => root.querySelector(s);
    const $$ = (s, root = document) => Array.from(root.querySelectorAll(s));

    // --- [Opcional] Anti-Forgery ---
    // En _Layout.cshtml agregá:
    // @inject Microsoft.AspNetCore.Antiforgery.IAntiforgery Xsrf
    // @{ var token = Xsrf.GetAndStoreTokens(Context).RequestToken; }
    // <meta name="RequestVerificationToken" content="@token" />
    const getToken = () =>
        document.querySelector('meta[name="RequestVerificationToken"]')?.getAttribute('content') ?? null;

    const buildHeaders = () => {
        const t = getToken();
        return t ? { 'RequestVerificationToken': t } : {};
    };

    // --- Notificaciones básicas (podés reemplazar por tu librería de toasts) ---
    const notify = {
        ok: (m) => console.log(m),
        warn: (m) => console.warn(m),
        err: (m) => console.error(m)
    };

    // --- Wrapper fetch con JSON + manejo de errores ---
    async function fetchJson(url, options = {}) {
        const opts = {
            method: 'GET',
            headers: { ...buildHeaders(), ...(options.headers || {}) },
            credentials: 'same-origin', // importante si usás cookies de sesión
            ...options
        };
        try {
            const res = await fetch(url, opts);
            if (!res.ok) {
                const txt = await res.text().catch(() => '');
                throw new Error(`HTTP ${res.status} - ${txt || res.statusText}`);
            }
            // Si respuesta no es JSON válido, que no truene
            const ct = res.headers.get('content-type') || '';
            if (!ct.includes('application/json')) return {};
            return await res.json();
        } catch (e) {
            notify.err(e.message || e);
            return { ok: false, error: true, message: e.message || 'Error de red' };
        }
    }

    // Formateo de moneda (CRC)
    const fmt = v => new Intl.NumberFormat('es-CR', { style: 'currency', currency: 'CRC', maximumFractionDigits: 2 }).format(v || 0);

    // Elementos base de la tabla del carrito
    const tbody = $('#cart-body');
    if (!tbody) return; // si no es la vista del carrito, no hace nada

    // Actualiza subtotal/total y el badge del carrito si existe
    function updateTotals(data, row) {
        if (!data) return;
        if (row && data.subtotal !== undefined) {
            $('.subtotal', row).textContent = fmt(data.subtotal);
        }
        if (data.total !== undefined) {
            const totalEl = $('#cart-total');
            if (totalEl) totalEl.textContent = fmt(data.total);
        }
        const badge = document.getElementById('cart-badge');
        if (badge && data.count !== undefined) badge.textContent = data.count;
    }

    // Clicks en +, -, Eliminar
    tbody.addEventListener('click', async (e) => {
        const btn = e.target.closest('button');
        if (!btn) return;
        const row = e.target.closest('tr');
        const id = row?.dataset.id;

        // Evita clicks dobles rápidos
        btn.disabled = true;

        if (btn.classList.contains('btn-plus')) {
            const qtyEl = $('.qty', row);
            const newQty = (parseInt(qtyEl.value || '1', 10) + 1);
            const data = await fetchJson(`/Carrito/ChangeQty?id=${id}&qty=${newQty}`, { method: 'POST' });
            if (!data.error) {
                updateTotals(data, row);
                qtyEl.value = newQty;
                notify.ok('Cantidad actualizada');
            } else {
                notify.warn('No se pudo actualizar la cantidad');
            }
        }

        if (btn.classList.contains('btn-minus')) {
            const qtyEl = $('.qty', row);
            const newQty = Math.max(1, parseInt(qtyEl.value || '1', 10) - 1);
            const data = await fetchJson(`/Carrito/ChangeQty?id=${id}&qty=${newQty}`, { method: 'POST' });
            if (!data.error) {
                updateTotals(data, row);
                qtyEl.value = newQty;
                notify.ok('Cantidad actualizada');
            } else {
                notify.warn('No se pudo actualizar la cantidad');
            }
        }

        if (btn.classList.contains('btn-remove')) {
            const data = await fetchJson(`/Carrito/Remove/${id}`, { method: 'POST' });
            if (!data.error) {
                row.remove();
                updateTotals(data);
                notify.ok('Producto eliminado');
                if (!tbody.children.length) {
                    location.reload(); // si quedó vacío, recarga para mostrar mensaje "carrito vacío"
                }
            } else {
                notify.warn('No se pudo eliminar el producto');
            }
        }

        btn.disabled = false;
    });

    // Cambio manual en el input de cantidad
    tbody.addEventListener('change', async (e) => {
        const input = e.target.closest('.qty');
        if (!input) return;
        const row = e.target.closest('tr');
        const id = row?.dataset.id;
        const val = Math.max(1, parseInt(input.value || '1', 10));
        input.disabled = true;
        const data = await fetchJson(`/Carrito/ChangeQty?id=${id}&qty=${val}`, { method: 'POST' });
        if (!data.error) {
            updateTotals(data, row);
            input.value = val;
            notify.ok('Cantidad actualizada');
        } else {
            notify.warn('No se pudo actualizar la cantidad');
        }
        input.disabled = false;
    });

    // Vaciar carrito
    const btnClear = document.getElementById('btn-clear');
    if (btnClear) {
        btnClear.addEventListener('click', async () => {
            btnClear.disabled = true;
            const data = await fetchJson('/Carrito/Clear', { method: 'POST' });
            if (!data.error) {
                updateTotals(data);
                notify.ok('Carrito vaciado');
                location.reload();
            } else {
                notify.warn('No se pudo vaciar el carrito');
            }
            btnClear.disabled = false;
        });
    }
})();

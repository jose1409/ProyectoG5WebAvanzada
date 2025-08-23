function mostrarMensajesTempData() {
    const mensaje = document.getElementById('mensajeTempData').innerText.trim();
    const error = document.getElementById('errorTempData').innerText.trim();

    if (mensaje.length > 0) {
        showToast('success', mensaje);
    }

    if (error.length > 0) {
        showToast('error', error);
    }
}

// Ejecutar al cargar la página
document.addEventListener("DOMContentLoaded", function () {
    mostrarMensajesTempData();
});
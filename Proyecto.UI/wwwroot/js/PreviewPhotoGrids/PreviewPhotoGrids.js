function previewImageGrids(input) {
    const previewContainer = document.getElementById("previewImagen");

    if (input.files && input.files[0]) {
        const file = input.files[0];

        if (!file.type.startsWith("image/")) {
            previewContainer.innerHTML = '<span class="text-danger">El archivo no es una imagen válida.</span>';
            input.value = ""; // Limpia el input para evitar confusión
            return;
        }

        const reader = new FileReader();
        reader.onload = function (e) {
            const imgHTML = `<img id="previewFoto" src="${e.target.result}" alt="Imagen" style="width: 80px; height: auto;">`;
            previewContainer.innerHTML = imgHTML;
        };
        reader.readAsDataURL(file);
    } else {
        previewContainer.innerHTML = '<span class="text-muted">Sin imagen</span>';
    }
}
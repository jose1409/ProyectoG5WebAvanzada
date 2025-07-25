document.addEventListener("DOMContentLoaded", () => {
    const preview = document.getElementById("previewFoto");

    // Mostrar la imagen si ya existe y no es solo "#"
    if (preview?.src && !preview.src.endsWith("#")) {
        preview.style.display = "block";
    } else {
        preview.style.display = "none";
    }
});

function previewImage(input) {
    const file = input.files[0];
    const preview = document.getElementById("previewFoto");

    if (file && file.type.startsWith("image/")) {
        const reader = new FileReader();

        reader.onload = (e) => {
            preview.src = e.target.result;
            preview.style.display = "block";
        };

        reader.readAsDataURL(file);
    } else {
        preview.src = "#";
        preview.style.display = "none";
    }
}

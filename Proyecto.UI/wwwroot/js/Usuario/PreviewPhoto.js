document.addEventListener("DOMContentLoaded", function () {
    const preview = document.getElementById("previewFoto");
    if (preview && preview.src && !preview.src.endsWith("#")) {
        preview.style.display = "block";
    }
});

function previewImage(input) {
    const archivo = input.files[0];
    const preview = document.getElementById("previewFoto");

    if (archivo && archivo.type.startsWith("image/")) {
        const lector = new FileReader();

        lector.onload = function (e) {
            preview.src = e.target.result;
            preview.style.display = "block";
        };

        lector.readAsDataURL(archivo);
    } else {
        preview.src = "#";
        preview.style.display = "none";
    }
}
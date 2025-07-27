$(function () {
    //Esta funcion es la que se llama y carga la modal con los datos
    $(document).on("click", ".btnEditarCategoria", function () {
        clearCategoriaForm();
        const data = {
            id: $(this).data("id"),
            descripcion: $(this).data("descripcion"),
            activo: $(this).data("activo").toString().toLowerCase() === 'true',
            imagenBase64: $(this).data("imagen"),
            edicion: $(this).data("edicion")
        };
        loadCategoriaForm(data);
    });

    //Esta funcion es la que verifica si se esta creando o editando
    $(document).on("click", ".btnAgregarCategoria", function () {
        clearCategoriaForm();
        $("#edicionEstado").prop("checked", $(this).data("edicion"));
    });

    //Esta funcion envia es la final, carga los datos y dependiendo de si es edicion o creacion, carga distintas URLS
    $("#btnGuardarModalCategoria").on("click", function (event) {
        event.preventDefault();
        const edicion = $("#edicionEstado").prop("checked");
        const formData = buildFormData(edicion);
        const url = edicion ? "/CategoriaA/ActualizarCategoria" : "/CategoriaA/CrearCategoria";

        $.ajax({
            url: url,
            type: "POST",
            processData: false,
            contentType: false,
            data: formData,
            success: function () {
                location.reload();
            }
        });
    });
});

//Esta funcion limpia la modal
function clearCategoriaForm() {
    $("#IdCategoria").val("");
    $("#DescripcionCategoria").val("");
    $("#estadoCategoria").prop("checked", false);
    $("#ImagenGrid").val("");
    $("#previewImagen").html('<span class="text-muted">Sin imagen</span>');
    $("#ImagenBase64Actual").val('');
}

//Esta funcion recibe los datos del buttom para setearlos en el modal
function loadCategoriaForm(data) {
    $("#IdCategoria").val(data.id);
    $("#DescripcionCategoria").val(data.descripcion);
    $("#estadoCategoria").prop("checked", data.activo);
    $("#edicionEstado").prop("checked", data.edicion);

    if (data.imagenBase64) {
        $("#previewImagen").html(`<img id="previewFoto" src="${data.imagenBase64}" alt="Imagen" style="width: 80px; height: auto;">`);
        $("#ImagenBase64Actual").val(data.imagenBase64);
    } else {
        $("#previewImagen").html('<span class="text-muted">Sin imagen</span>');
        $("#ImagenBase64Actual").val('');
    }
}

//Esta funcion hace el formData y obtiene los datos de la modal (verificacion de Id en caso de editar)
function buildFormData(edicion) {
    const formData = new FormData();
    formData.append("Descripcion", $("#DescripcionCategoria").val());
    formData.append("Activo", $("#estadoCategoria").prop("checked"));

    const fileInput = document.getElementById("ImagenGrid");

    if (fileInput.files.length > 0) {
        formData.append("Imagen64", fileInput.files[0]);
    } else if (edicion) {
        formData.append("ImagenBase64", $("#ImagenBase64Actual").val());
    }

    if (edicion) {
        formData.append("IdCategoria", $("#IdCategoria").val());
    }

    return formData;
}

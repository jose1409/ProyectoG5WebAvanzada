$(function () {
    // Cargar modal con datos del producto
    $(document).on("click", ".btnEditarProducto", function () {
        clearProductoForm();
        const data = {
            idProducto: $(this).data("id"),
            descripcion: $(this).data("descripcion"),
            precio: $(this).data("precio"),
            detalle: $(this).data("detalle"),
            imagenBase64: $(this).data("imagen"),
            activo: $(this).data("activo").toString().toLowerCase() === 'true',
            edicion: $(this).data("edicion"),
            idCategoria: $(this).data("categoria")
        };
        loadProductoForm(data);
    });

    // Modal para agregar nuevo producto
    $(document).on("click", ".btnAgregarProducto", function () {
        clearProductoForm();
        $("#edicionEstado").val($(this).data("edicion"));
    });

    // Guardar producto (crear o editar)
    $("#btnGuardarModalProducto").on("click", function (event) {
        event.preventDefault();
        const edicion = $("#edicionEstado").val() === "true";
        const formData = buildFormDataProducto(edicion);
        const url = edicion ? "/ProductosA/ActualizarProducto" : "/ProductosA/CrearProducto";

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

// Limpiar modal
function clearProductoForm() {
    $("#IdProducto").val("");
    $("#DescripcionProducto").val("");
    $("#PrecioProducto").val("");
    $("#DetalleProducto").val("");
    $("#ImagenGrid").val("");
    $("#previewImagen").html('<span class="text-muted">Sin imagen</span>');
    $("#ImagenBase64Actual").val('');
}

// Cargar datos en modal
function loadProductoForm(data) {
    $("#IdProducto").val(data.idProducto);
    $("#DescripcionProducto").val(data.descripcion);
    $("#PrecioProducto").val(data.precio);
    $("#DetalleProducto").val(data.detalle);
    $("#estadoProducto").prop("checked", data.activo);
    $("#edicionEstado").val(data.edicion);

    if (data.idCategoria) {
        $("#CategoriaProducto").val(data.idCategoria);
    } else {
        $("#CategoriaProducto").val("");
    }

    if (data.imagenBase64) {
        $("#previewImagen").html(`<img id="previewFoto" src="${data.imagenBase64}" alt="Imagen" style="width: 80px; height: auto;">`);
        $("#ImagenBase64Actual").val(data.imagenBase64);
    } else {
        $("#previewImagen").html('<span class="text-muted">Sin imagen</span>');
        $("#ImagenBase64Actual").val('');
    }
}

// Armar datos para enviar al servidor
function buildFormDataProducto(edicion) {
    const formData = new FormData();
    formData.append("Descripcion", $("#DescripcionProducto").val());
    formData.append("Precio", $("#PrecioProducto").val());
    formData.append("Detalle", $("#DetalleProducto").val());
    formData.append("IdCategoria", $("#CategoriaProducto").val());
    formData.append("Activo", $("#estadoProducto").prop("checked"));

    const fileInput = document.getElementById("ImagenGrid");
    if (fileInput.files.length > 0) {
        formData.append("Imagen64", fileInput.files[0]);
    } else if (edicion) {
        formData.append("ImagenBase64", $("#ImagenBase64Actual").val());
    }

    if (edicion) {
        formData.append("IdProducto", $("#IdProducto").val());
    }

    return formData;
}

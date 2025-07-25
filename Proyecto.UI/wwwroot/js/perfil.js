$(document).ready(function () {
    const idUsuario = $("#IdUsuario").val();
    console.log("ID del usuario:", idUsuario);

    const API_BASE = "https://localhost:7093";

    if (idUsuario) {
        cargarPerfil(idUsuario);
    }

    $("#formPerfil").on("submit", function (e) {
        e.preventDefault();
        actualizarPerfil();
    });

    function cargarPerfil(idUsuario) {
        $.get(`${API_BASE}/Usuario/getUserProfileData?idUsuario=${idUsuario}`, function (response) {
            if (response.codigo === 0) {
                const u = response.contenido;
                $("#IdUsuario").val(u.idUsuario);
                $("#Cedula").val(u.cedula);
                $("#CorreoElectronico").val(u.correoElectronico);
                $("#Nombre").val(u.nombre);
                $("#Apellidos").val(u.apellidos);
                $("#Telefono").val(u.telefono ?? '');
            } else {
                alert("No se pudo cargar tu perfil.");
            }
        }).fail(function () {
            alert("Error al conectar con el servidor.");
        });
    }

    function actualizarPerfil() {
        const formData = {
            IdUsuario: $("#IdUsuario").val(),
            Cedula: $("#Cedula").val(),
            CorreoElectronico: $("#CorreoElectronico").val(),
            Nombre: $("#Nombre").val(),
            Apellidos: $("#Apellidos").val(),
            Telefono: $("#Telefono").val(),
            Fotografia: null
        };

        $.ajax({
            url: `${API_BASE}/Usuario/updateUserProfileData`,
            type: "PUT",
            data: JSON.stringify(formData),
            contentType: "application/json",
            success: function (response) {
                if (response.codigo === 0) {
                    alert("Perfil actualizado correctamente.");
                } else {
                    alert("Hubo un problema al guardar los cambios.");
                }
            },
            error: function () {
                alert("Error al actualizar el perfil.");
            }
        });
    }
});

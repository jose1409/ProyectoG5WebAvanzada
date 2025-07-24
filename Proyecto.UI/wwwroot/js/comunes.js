function ConsultarPersonaApi() {

    let identificacion = $("#Cedula").val();
    $("#Nombre").val("");
    $("#Apellidos").val("");
  if (identificacion.length >= 9) {
    $.ajax({
      url: "https://apis.gometa.org/cedulas/" + identificacion,
      type: "GET",
      dataType: "json",
        success: function (data) {
            const persona = data.results[0];
            $("#Nombre").val(persona.firstname);
            $("#Apellidos").val(persona.lastname);
      },
    });
  }
}
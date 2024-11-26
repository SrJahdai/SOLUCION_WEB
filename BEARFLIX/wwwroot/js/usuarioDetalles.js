// Asegúrate de que la función esté bien declarada y no falten paréntesis ni llaves
$(document).ready(function () {
    // Sección de puntuación
    $(".star").click(function () {
        var puntaje = $(this).data("value");
        var idPelicula = $(this).closest(".pelicula-detalles").data("id");
        var idUsuario = $("#userId").val(); // Asegúrate de tener el id del usuario disponible

        // Enviar el puntaje al backend
        $.ajax({
            url: "/api/Puntaje", // Ajusta la URL según la API del backend
            type: "POST",
            contentType: "application/json",
            data: JSON.stringify({
                IdUsuario: idUsuario,
                IdPelicula: idPelicula,
                Puntaje: puntaje
            }),
            success: function (response) {
                // Actualizar visualización de estrellas
                actualizarEstrellas(puntaje);
            },
            error: function (xhr, status, error) {
                alert("Error al guardar el puntaje");
            }
        });
    });
});

// Función para actualizar las estrellas visualmente
function actualizarEstrellas(puntaje) {
    $(".star").each(function (index) {
        if (index < puntaje) {
            $(this).addClass("filled"); // Añadir clase 'filled' para mostrar la estrella con color
        } else {
            $(this).removeClass("filled"); // Eliminar la clase 'filled' para las estrellas vacías
        }
    });
}

$(document).ready(function () {
    function mostrarCarga() {
        $("#loading-screen").css("visibility", "visible");
    }

    function ocultarCarga() {
        $("#loading-screen").css("visibility", "hidden");
    }
    var proveedorId = @Model.IdProveedor;
    $('#IdProveedor').val(proveedorId);


    var generoIds = @Html.Raw(Json.Serialize(Model.IdGeneros));
    $('#IdGeneros').val(generoIds);

    $(document).on('click', '.guardar-cambios', function (e) {
        mostrarCarga();
        e.preventDefault();
        var $button = $(this);

        $button.text('Guardando...').prop('disabled', true);

        var id = $button.data('id');
        if (!id) {
            Swal.fire({
                icon: 'error',
                title: 'Error',
                text: 'No se encontró el ID de la película.',
            }).then(() => {
                $button.text('Guardar Cambios').prop('disabled', false);
            });
            return;
        }

        var peliculaData = {
            Titulo: $('#Titulo').val(),
            Descripcion: $('#Descripcion').val(),
            Duracion: $('#Duracion').val(),
            PrecioCompra: $('#PrecioCompra').val(),
            PrecioRenta: $('#PrecioRenta').val(),
            IdProveedor: $('#IdProveedor').val(),
            IdGeneros: $('#IdGeneros').val()
        };

        $.ajax({
            url: '/Administrador/Editar/' + id,
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(peliculaData),
            success: function (response) {
                ocultarCarga();
                Swal.fire({
                    icon: 'success',
                    title: 'Guardado',
                    text: 'Los cambios se han guardado con éxito.',
                    confirmButtonText: 'Aceptar'
                }).then(() => {
                    window.location.href = '/Administrador/Index';
                });
            },
            error: function (xhr, status, error) {
                ocultarCarga();
                Swal.fire({
                    icon: 'error',
                    title: 'Error',
                    text: 'Ocurrió un error al guardar los cambios: ' + error,
                }).then(() => {
                    $button.text('Guardar Cambios').prop('disabled', false);
                });
            }
        });
    });


});
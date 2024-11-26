function eliminarPelicula(id) {
    Swal.fire({
        title: '¿Estás seguro?',
        text: "No podrás revertir esta acción",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Sí, eliminarla',
        cancelButtonText: 'Cancelar'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: '/Administrador/Eliminar/' + id,
                type: 'DELETE',
                success: function (response) {
                    Swal.fire(
                        '¡Eliminada!',
                        'La película ha sido eliminada correctamente.',
                        'success'
                    ).then(() => {
                        location.reload(); // Recargar la página
                    });
                },
                error: function (error) {
                    Swal.fire(
                        'Error',
                        'Hubo un problema al eliminar la película.',
                        'error'
                    );
                }
            });
        }
    });
}

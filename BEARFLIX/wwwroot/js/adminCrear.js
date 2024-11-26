$(document).ready(function () {
    // Cargar géneros y proveedores desde el backend
    cargarGeneros();
    cargarProveedores();

    function mostrarCarga() {
        $("#loading-screen").css("visibility", "visible");
    }

    function ocultarCarga() {
        $("#loading-screen").css("visibility", "hidden");
    }

    $('#formRegistrarPelicula').on("submit", function (event) {
        event.preventDefault();
        mostrarCarga();

        var formData = new FormData(this); // Recoge los datos del formulario

        $.ajax({
            url: '/api/admin/registrar', // Cambia por la URL correcta
            type: 'POST',
            data: formData,
            processData: false,
            contentType: false,
            success: function (response) {
                ocultarCarga();
                if (response.success) {
                    // Si la película se registró correctamente
                    Swal.fire({
                        icon: 'success',
                        title: 'Éxito',
                        text: response.message
                    }).then(function () {
                        // Redirigir o realizar otras acciones si es necesario
                        window.location.href = "/Administrador/Index"; // Cambiar esto por la URL que desees
                    });
                }
            },
            error: function (xhr, status, error) {
                ocultarCarga();
                var response = JSON.parse(xhr.responseText);
                
                if (!response.success) {
                    Swal.fire({
                        icon: 'error',
                        title: 'Error',
                        text: response.message
                    });
                }
            }
        });
    });


    function cargarGeneros() {
        $.ajax({
            url: '/api/admin/generos',
            type: 'GET',
            success: function (response) {
                // Limpiar las opciones anteriores
                $('#generos').empty();
                $('#generos').append('<option value="">Seleccionar Género</option>'); // Opción por defecto

                // Añadir las opciones de géneros usando interpolación de cadenas
                response.forEach(function (genero) {
                    $('#generos').append(`<option value="${genero.id}">${genero.descripcion}</option>`);
                });
            },
            error: function (xhr, status, error) {
                alert('Error al cargar los géneros');
            }
        });
    }

    function cargarProveedores() {
        $.ajax({
            url: '/api/admin/proveedores',
            type: 'GET',
            success: function (response) {
                // Limpiar las opciones anteriores
                $('#proveedor').empty();
                $('#proveedor').append('<option value="">Seleccionar Proveedor</option>'); // Opción por defecto

                // Añadir las opciones de proveedores usando interpolación de cadenas
                response.forEach(function (proveedor) {
                    $('#proveedor').append(`<option value="${proveedor.id}">${proveedor.descripcion}</option>`);
                });
            },
            error: function (xhr, status, error) {
                alert('Error al cargar los proveedores');
            }
        });
    }
});

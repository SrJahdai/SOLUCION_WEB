
    $("#registro-submit").on("click", function (event) {
        event.preventDefault(); // Evitar que el formulario se envíe de la forma tradicional


        // Obtener los valores del formulario
        const nombre = $('#Nombre').val();
        const correo = $('#Correo').val();
        const contrasena = $('#Contrasena').val();
        const confirmarContrasena = $('#confirmarContrasena').val();
        const fechaNacimiento = $('#FechaNacimiento').val();


        // Validación de contraseñas
        if (contrasena !== confirmarContrasena) {
            $('#error-message').text('Las contraseñas no coinciden.');
            return;
        }

        // Enviar los datos del formulario a la API para registro
        $.ajax({
            url: '/api/autenticacion/registro',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify({
                Correo: correo,
                Contrasena: contrasena,
                Nombre: nombre,
                FechaNacimiento: fechaNacimiento
            }),
            success: function (response) {
                
                alert('Registro exitoso.');
                window.location.href = '/Login'; // Redirige a la página de login
            },
            error: function (xhr, status, error) {
                // Mostrar un mensaje de error si la solicitud falla
                const errorMessage = xhr.responseJSON ? xhr.responseJSON.message : 'Error en el registro.';
                $('#error-message').text(errorMessage);
            }
        });
    });

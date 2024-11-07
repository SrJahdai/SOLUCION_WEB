
    $("#registro-submit").on("click", function (event) {
        event.preventDefault(); 


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
                
                
                Swal.fire({
                    position: "top-end",
                    icon: "success",
                    title: "Cuenta registrada correctamente",
                    showConfirmButton: false,
                    timer: 1000
                });
                window.location.href = 'Login'; // Redirige a la página de login
            },
            error: function (xhr, status, error) {
                // Mostrar un mensaje de error si la solicitud falla
                const errorMessage = xhr.responseJSON ? xhr.responseJSON.message : 'Error en el registro.';
                $('#error-message').text(errorMessage);
            }
        });
    });

$('#formIngresar').on("submit",function (e) {
    e.preventDefault();

    // Limpieza de errores previos
    $('#error-message').text('');
    $('#correo-error').text('');
    $('#password-error').text('');

    const correo = $('#correo').val();
    const password = $('#password').val();
    const rememberMe = $('#rememberMe').is(':checked');

    // Validación simple de campos vacíos
    if (!correo || !password) {
        $('#error-message').text('Por favor, complete todos los campos.');
        return;
    }

    const loginData = {
        Correo: correo,
        Contrasena: password
    };

    // Realizar la solicitud de login usando AJAX
    $.ajax({
        url: '/api/autenticacion/login',
        method: 'POST',
        contentType: 'application/json',
        data: JSON.stringify(loginData),
        success: function (response) {
            // Si el login es exitoso, se puede redirigir o hacer algo con el ID del usuario
            alert('Bienvenido,  ' + response.Nombre); // Aquí obtenemos el ID del usuario
            // Redirige a la página de inicio, por ejemplo
        },
        error: function (xhr) {
            if (xhr.status === 401) {
                $('#error-message').text('Credenciales incorrectas. Intente de nuevo.');
            } else {
                $('#error-message').text('Ocurrió un error al iniciar sesión. Inténtelo más tarde.');
            }
        }
    });
});

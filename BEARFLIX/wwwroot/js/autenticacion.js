$(document).ready(function () {

    function mostrarCarga() {
        $('#loading-screen').css('visibility', 'visible');
    }

    function ocultarCarga() {
        $('#loading-screen').css('visibility', 'hidden');
    }
    

    $("#registerForm").on("submit",function (e) {
        e.preventDefault();

        mostrarCarga();

        var nombre = $("#nombre").val();
        var correo = $("#correo").val();
        var contrasena = $("#contrasena").val();
        var confirmarContrasena = $("#confirmarContrasena").val();
        var fechaNacimiento = $("#fechaNacimiento").val();

        
        $(".form-control").removeClass("is-invalid");
        $("#error-message").text("");

        var valid = true;

        // Validación de la longitud de la contraseña
        if (contrasena.length < 6) {
            valid = false;
            $("#contrasena").addClass("is-invalid");
            $("#error-message").text("La contraseña debe tener al menos 6 caracteres.");
        }

        // Verificar que la contraseña no contenga caracteres especiales no permitidos
        var regex = /[!^&*()_+={}\[\]|\\:;"'<>,.?/`~]/;
        if (regex.test(contrasena)) {
            valid = false;
            $("#contrasena").addClass("is-invalid");
            $("#error-message").text("La contraseña solo puede contener los caracteres especiales: @, #, $, %.");
        }

        // Validación de que las contraseñas coinciden
        if (contrasena !== confirmarContrasena) {
            valid = false;
            $("#confirmarContrasena").addClass("is-invalid");
            $("#error-message").text("Las contraseñas no coinciden.");
        }

        // Validación del correo electrónico (estructura de correo válida)
        var emailRegex = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/;
        if (!emailRegex.test(correo)) {
            valid = false;
            $("#correo").addClass("is-invalid");
            $("#error-message").text("El correo electrónico no tiene un formato válido.");
        }

        if (!valid) {
            ocultarCarga()
            return; 
        }

        var registerData = {
            Nombre: nombre,
            Correo: correo,
            Contrasena: contrasena,
            ConfirmarContrasena: confirmarContrasena,
            FechaNacimiento: fechaNacimiento
        };

        $.ajax({
            url: "/api/auth/registrar",
            type: "POST",
            contentType: "application/json",
            data: JSON.stringify(registerData),
            success: function (response) {
                ocultarCarga();
                alert(response); 
                
            },
            error: function (xhr, status, error) {
                ocultarCarga();
                var errorMessage = xhr.responseText; // El mensaje del error
                $("#error-message").text(errorMessage); // Mostrar el mensaje de error en el frontend
            }
        });

        

    });
});

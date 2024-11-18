$(document).ready(function () {

    function mostrarCarga() {
        $("#loading-screen").css("visibility", "visible");
    }

    function ocultarCarga() {
        $("#loading-screen").css("visibility", "hidden");
    }

    $("#loginForm").on("submit", function (e) {
        e.preventDefault();

        mostrarCarga();

        $(".form-control").removeClass("is-invalid");
        $("#error-message").text("");

        // Datos del formulario
        let correo = $("#correo").val();
        let contrasena = $("#password").val();
        let rememberMe = $("#rememberMe").prop("checked");

        let loginData = {
            Correo: correo,
            Contrasena: contrasena,
            Recuerdame: rememberMe
        };

        // Petición AJAX
        $.ajax({
            url: "/api/auth/login",
            type: "POST",
            contentType: "application/json",
            data: JSON.stringify(loginData),
            success: function (response) {
                ocultarCarga();
                Swal.fire({
                    position: 'center',
                    iconHtml: '<i class="bi bi-person-check"></i>',
                    title: 'Sesión iniciada',
                    text: 'Bienvenido',
                    iconColor: 'blue', 
                    toast: true, 
                    showConfirmButton: false, 
                    timer: 2000 
                });
                window.location.href = response.redirectUrl; 
            },
            error: function (xhr, status, error) {
                ocultarCarga();
                $("#correo").addClass("is-invalid");
                $("#password").addClass("is-invalid");
                $("#error-message").text("Las credenciales no coinciden, intente nuevamente.");
            }
        });
    });




    $("#registerForm").on("submit", function (e) {
        e.preventDefault();

        mostrarCarga();

        let nombre = $("#nombre").val();
        let correo = $("#correo").val();
        let contrasena = $("#contrasena").val();
        let confirmarContrasena = $("#confirmarContrasena").val();
        let fechaNacimiento = $("#fechaNacimiento").val();

        $(".form-control").removeClass("is-invalid");
        $("#error-message").text("");

        let valid = true;

        if (contrasena.length < 6) {
            valid = false;
            $("#contrasena").addClass("is-invalid");
            $("#error-message").text("La contraseña debe tener al menos 6 caracteres.");
        }

        var regex = /[!^&*()_+={}\[\]|\\:'´"<>,.?/`~]/;
        if (regex.test(contrasena)) {
            valid = false;
            $("#contrasena").addClass("is-invalid");
            $("#error-message").text("La contraseña solo puede contener los caracteres especiales: @, #, $, %.");
        }

        if (contrasena !== confirmarContrasena) {
            valid = false;
            $("#confirmarContrasena").addClass("is-invalid");
            $("#error-message").text("Las contraseñas no coinciden.");
        }

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

        let registerData = {
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
                window.location.href = 'Login';
                Swal.fire({
                    iconHtml: '<i class="bi bi-person-add"></i>',
                    iconColor: 'green',
                    title: "¡Éxito!",
                    text: "El usuario se registró correctamente. Inicie sesión.",
                    showConfirmButton: false, 
                    timer: 3000 
                 });
                
            },
            error: function (xhr, status, error) {
                ocultarCarga();
                var errorMessage = xhr.responseText;
                $("#error-message").text(errorMessage);
                Swal.fire({
                    position: "top-end",
                    icon: "error",
                    title: "Ups...",
                    text: "Algo salió mal",
                    footer: '<a href="/Inicio/Contactanos">Si el problema persite, contáctanos</a>'
                });
            }
        });
    });
});

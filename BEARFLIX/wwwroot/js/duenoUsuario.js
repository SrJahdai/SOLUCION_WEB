$(document).ready(function () {
    $('#add-user').click(function (event) {
        event.preventDefault();
        const email = $('#email').val();
        const password = $('#password').val();
        const username = $('#username').val();
        const role = $('#role').val();

        $.ajax({
            url: '@Url.Action("AgregarUsuario", "Dueno")',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify({
                Email: email,
                Password: password,
                Username: username,
                Role: role
            }),
            success: function (response) {
                alert(response.mensaje);
            },
            error: function (xhr, status, error) {
                alert(xhr.responseText);
            }
        });
    });

    $('#remove-user').click(function (event) {
        event.preventDefault();
        const email = $('#email').val();

        $.ajax({
            url: '@Url.Action("EliminarUsuario", "Dueno")',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify({
                Email: email
            }),
            success: function (response) {
                alert(response.mensaje);
            },
            error: function (xhr, status, error) {
                alert(xhr.responseText);
            }
        });
    });
});
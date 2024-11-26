$(document).ready(function () {
    // Botón de retroceso
    $("#back-button").click(function () {
      window.location.href = "anterior.html"; // Cambia por la ruta de la pantalla anterior
    });
  
    // Acción del botón "Dar Alta"
    $("#add-user").click(function (event) {
      event.preventDefault(); // Evita el envío del formulario
      const email = $("#email").val();
      const password = $("#password").val();
      const username = $("#username").val();
      const role = $("#role").val();
  
      if (email && password && username) {
        alert(`Usuario dado de alta:\nCorreo: ${email}\nUsuario: ${username}\nRol: ${role}`);
      } else {
        alert("Por favor, complete todos los campos.");
      }
    });
  
    // Acción del botón "Dar Baja"
    $("#remove-user").click(function (event) {
      event.preventDefault(); // Evita el envío del formulario
      const email = $("#email").val();
  
      if (email) {
        const confirmDelete = confirm(`¿Está seguro de que desea dar de baja al usuario con correo: ${email}?`);
        if (confirmDelete) {
          alert(`Usuario con correo ${email} dado de baja.`);
        }
      } else {
        alert("Por favor, ingrese el correo electrónico del usuario a dar de baja.");
      }
    });
  });
  
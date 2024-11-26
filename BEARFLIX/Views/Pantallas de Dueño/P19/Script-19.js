// Simulamos los datos obtenidos de la base de datos
const reporte = {
    fechaInicio: "01/11/2024",
    fechaFin: "30/11/2024",
    usuariosAlta: 150,
    usuariosBaja: 20,
    totalUsuarios: 5000,
};

// Insertamos los datos en la interfaz
document.getElementById("dateRange").textContent = `${reporte.fechaInicio} - ${reporte.fechaFin}`;
document.getElementById("usersAdded").textContent = `${reporte.usuariosAlta} users`;
document.getElementById("usersRemoved").textContent = `${reporte.usuariosBaja} users`;
document.getElementById("totalUsers").textContent = `${reporte.totalUsuarios} users`;

// Funcionalidad del botón "Atrás"
document.getElementById("backButton").addEventListener("click", () => {
    alert("Regresando...");
});

// Simulamos datos obtenidos de la base de datos
const reportes = [
    { id: 1, fechaInicio: "12/11/2024", fechaFin: "19/11/2024" },
    { id: 2, fechaInicio: "05/10/2024", fechaFin: "12/10/2024" },
    { id: 3, fechaInicio: "28/09/2024", fechaFin: "05/10/2024" },
];

// Seleccionamos el contenedor donde aparecerán los reportes
const reportList = document.getElementById("reportList");

// Generamos dinámicamente los elementos
reportes.forEach((reporte) => {
    // Creamos un contenedor de lista
    const item = document.createElement("div");
    item.classList.add("list-group-item");

    // Texto del reporte
    const texto = document.createElement("span");
    texto.textContent = `REPORTE DEL ${reporte.fechaInicio} - ${reporte.fechaFin}`;
    item.appendChild(texto);

    // Botón para ver reporte
    const botonVer = document.createElement("button");
    botonVer.classList.add("btn", "btn-red");
    botonVer.textContent = "VER REPORTE";
    botonVer.addEventListener("click", () => {
        alert(`Ver reporte ${reporte.id}`);
    });
    item.appendChild(botonVer);

    // Agregamos el ítem a la lista
    reportList.appendChild(item);
});

// Funcionalidad del botón "Atrás"
document.getElementById("backButton").addEventListener("click", () => {
    alert("Regresando...");
});

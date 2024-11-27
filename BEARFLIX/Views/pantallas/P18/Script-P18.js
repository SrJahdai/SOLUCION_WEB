// Simulamos datos obtenidos de la base de datos 
const transacciones = [
    { id: 27834084 },
    { id: 27834085 },
    { id: 27834086 },
    { id: 27834087 },
];

// Seleccionamos el contenedor donde aparecerán los bauchers
const paymentList = document.getElementById("paymentList");

// Generamos dinámicamente los elementos
transacciones.forEach((transaccion) => {
    // Creamos un contenedor de lista
    const item = document.createElement("div");
    item.classList.add("list-group-item");

    // Texto del baucher
    const texto = document.createElement("span");
    texto.textContent = `PAGO #${transaccion.id}`;
    item.appendChild(texto);

    // Botón para ver el baucher
    const botonVer = document.createElement("button");
    botonVer.classList.add("btn", "btn-red");
    botonVer.textContent = "VER";
    botonVer.addEventListener("click", () => {
        alert(`Ver pago con ID: ${transaccion.id}`);
    });
    item.appendChild(botonVer);

    // Agregamos el ítem a la lista
    paymentList.appendChild(item);
});

// Funcionalidad del botón "Atrás"
document.getElementById("backButton").addEventListener("click", () => {
    alert("Regresando...");
});

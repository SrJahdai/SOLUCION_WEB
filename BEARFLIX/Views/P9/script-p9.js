// Suponiendo que tienes un array de películas con propiedades "nombre" y "puntuacion"
const peliculas = [
    { nombre: "Película 1", puntuacion: 54 },
    { nombre: "Película 2", puntuacion: 60 },
    // ... más películas
];

// Función para ordenar las películas
function ordenar(orden) {
    peliculas.sort((a, b) => {
        if (orden === 'asc') {
            return a.puntuacion - b.puntuacion;
        } else {
            return b.puntuacion - a.puntuacion;
        }
    });

}

// Función para actualizar la tabla con los datos de las películas
function actualizarTabla() {
    const tbody = document.querySelector('tbody');
    tbody.innerHTML = '';

    peliculas.forEach(pelicula => {
        const row = document.createElement('tr');
        row.innerHTML = `
            <td>${pelicula.nombre}</td>
            <td class="text-center">${pelicula.puntuacion}%</td>
            <td class="text-center">
                <button class="btn btn-link" onclick="ordenar('asc')">▲</button>
                <button class="btn btn-link" onclick="ordenar('desc')">▼</button>
            </td>
        `;
        tbody.appendChild(row);
    });
}


// Función para actualizar la tabla con los datos filtrados y ordenados
function actualizarTabla(datosFiltrados) {
    const tbody = document.querySelector('tbody');
    tbody.innerHTML = '';

    datosFiltrados.forEach(pelicula => {
        const row = document.createElement('tr');
        row.innerHTML = `
            <td>${pelicula.nombre}</td>
            <td class="text-center">${pelicula.puntuacion}%</td>
        `;
        tbody.appendChild(row);
    });
}

// Función para filtrar los datos
function filtrar(filtro) {
    let datosFiltrados = peliculas;
    switch (filtro) {
        case 'Por Género':
            // Implementar la lógica para filtrar por género
            // Ejemplo:
            datosFiltrados = peliculas.filter(pelicula => pelicula.genero === 'Acción');
            break;
        case 'Por Proveedor':
            // Implementar la lógica para filtrar por proveedor
            // Ejemplo:
            datosFiltrados = peliculas.filter(pelicula => pelicula.proveedor === 'Netflix');
            break;
    }
    actualizarTabla(datosFiltrados);
}

// Función para ordenar los datos
function ordenar(orden) {
    let datosOrdenados = [...peliculas]; // Crear una copia del array original
    datosOrdenados.sort((a, b) => {
        if (orden === 'De A a Z') {
            return a.nombre.localeCompare(b.nombre);
        } else {
            return b.nombre.localeCompare(a.nombre);
        }
    });
    actualizarTabla(datosOrdenados);
}

// Manejadores de eventos para los menús desplegables
document.querySelectorAll('.dropdown-menu').forEach(dropdown => {
    dropdown.addEventListener('click', (event) => {
        const target = event.target;
        if (target.tagName === 'A') {
            const option = target.textContent.trim();
            switch (option) {
                case 'Por Género':
                case 'Por Proveedor':
                    filtrar(option);
                    break;
                case 'De A a Z':
                case 'De Z a A':
                    ordenar(option);
                    break;
            }
        }
    });
});

// Inicializar la tabla con todos los datos
actualizarTabla(peliculas);

// Ejemplo de cómo manejar la selección de una opción:
document.querySelectorAll('.dropdown-menu').forEach(dropdown => {
    dropdown.addEventListener('click', (event) => {
        const target = event.target;
        if (target.tagName === 'A') {
            const option = target.textContent.trim();
            switch (option) {
                // ... casos para cada opción
            }
        }
    });
});
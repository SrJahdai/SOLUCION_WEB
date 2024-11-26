$(document).ready(function () {
    // Simulación de datos provenientes de una base de datos
    const genres = [
      { id: 1, name: "Terror" },
      { id: 2, name: "Comedia" },
      { id: 3, name: "Acción" },
      { id: 4, name: "Romance" },
    ];
  
    // Función para renderizar la lista de géneros
    function renderGenres() {
      const $genreList = $("#genre-list");
      $genreList.empty(); // Limpiar el contenido antes de volver a generar
      genres.forEach((genre) => {
        const row = `
          <tr>
            <td>${genre.name}</td>
            <td class="text-center">
              <button class="action-btn edit-btn" data-id="${genre.id}" title="Editar">
                <i class="bi bi-pencil"></i>
              </button>
              <button class="action-btn delete-btn ms-2" data-id="${genre.id}" title="Borrar">
                <i class="bi bi-trash"></i>
              </button>
            </td>
          </tr>
        `;
        $genreList.append(row);
      });
  
      // Asociar eventos a los botones generados dinámicamente
      $(".edit-btn").click(function () {
        const id = $(this).data("id");
        alert(`Editar género con ID: ${id}`);
      });
  
      $(".delete-btn").click(function () {
        const id = $(this).data("id");
        const confirmDelete = confirm(`¿Estás seguro de que deseas borrar el género con ID: ${id}?`);
        if (confirmDelete) {
          alert(`Género con ID: ${id} eliminado.`);
        }
      });
    }
  
    // Renderizar la lista de géneros al cargar la página
    renderGenres();
  
  // Acción del botón de retroceso
  $("#back-button").click(function () {
    window.location.href = 'index-P10.html'; // Redirige a la página S-10.html
  });
});
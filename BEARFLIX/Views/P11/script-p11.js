$(document).ready(function () {
    // Simulación de datos provenientes de una base de datos
    const prov = [
      { id: 1, name: "Dreamworks" },
      { id: 2, name: "Disney" },
      { id: 3, name: "Marvel" },
      { id: 4, name: "Ghibli" },
    ];
  
    // Función para renderizar la lista de géneros
    function renderprov() {
      const $provList = $("#prov-list");
      $provList.empty(); // Limpiar el contenido antes de volver a generar
      prov.forEach((genre) => {
        const row = `
          <tr>
            <td>${prov.name}</td>
            <td class="text-center">
              <button class="action-btn edit-btn" data-id="${prov.id}" title="Editar">
                <i class="bi bi-pencil"></i>
              </button>
              <button class="action-btn delete-btn ms-2" data-id="${prov.id}" title="Borrar">
                <i class="bi bi-trash"></i>
              </button>
            </td>
          </tr>
        `;
        $provList.append(row);
      });
  
      // Asociar eventos a los botones generados dinámicamente
      $(".edit-btn").click(function () {
        const id = $(this).data("id");
        alert(`Editar proveedor con ID: ${id}`);
      });
  
      $(".delete-btn").click(function () {
        const id = $(this).data("id");
        const confirmDelete = confirm(`¿Estás seguro de que deseas borrar el proveedor con ID: ${id}?`);
        if (confirmDelete) {
          alert(`proveedor con ID: ${id} eliminado.`);
        }
      });
    }
  
    // Renderizar la lista de géneros al cargar la página
    renderprov();
  
  // Acción del botón de retroceso
  $("#back-button").click(function () {
    window.location.href = 'index-P10.html'; // Redirige a la página S-10.html
  });
});

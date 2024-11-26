// Aquí puedes agregar la lógica de JavaScript para los botones
    const btnDarAlta = document.getElementById('btnDarAlta');
    const btnVerProveedores = document.getElementById('btnVerProveedores');

    btnDarAlta.addEventListener('click', () => {
        const nombre = document.getElementById('nombreProveedor').value;
        const porcentaje = document.getElementById('porcentajeAPagar').value;
// Aquí agregarías la lógica para guardar los datos del proveedor
            console.log('Dar de alta a:', nombre, porcentaje);
        });

    btnVerProveedores.addEventListener('click', () => {
// Aquí agregarías la lógica para mostrar la lista de proveedores existentes
        onsole.log('Ver proveedores existentes');
        
});

const porcentajeInput = document.getElementById('porcentajeAPagar');

porcentajeInput.addEventListener('input', () => {
    if (porcentajeInput.value > 100) {
        porcentajeInput.value = 100;
    }
});
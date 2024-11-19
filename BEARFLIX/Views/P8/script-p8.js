const formulario = document.querySelector('form');

const imagenPrincipalInput = document.getElementById('imagenPrincipal');
const imagenPrincipalPreview = document.getElementById('imagenPrincipalPreview'); // Suponiendo que tienes un elemento <img> con este ID para mostrar la previsualización

imagenPrincipalInput.addEventListener('change', (event) => {
    const file = event.target.files[0];
    if (file) {
        const reader = new FileReader();
        reader.onload = (e) => {
            imagenPrincipalPreview.src = e.target.result;
        };
        reader.readAsDataURL(file);
    }
});

formulario.addEventListener('submit', (event) => {
    event.preventDefault();
    // Aquí puedes agregar la lógica de validación
    const titulo = document.getElementById('titulo').value;
    if (titulo === '') {
        alert('Por favor, ingresa un título');
        return;
    }
    // ... otras validaciones
    // Si todos los datos son válidos, puedes enviar el formulario
    formulario.submit();
});
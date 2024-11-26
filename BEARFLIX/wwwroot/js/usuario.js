function getCookie(name) {
    const value = `; ${document.cookie}`;
    const parts = value.split(`; ${name}=`);
    if (parts.length === 2) return parts.pop().split(';').shift();
    return null;
}

// Obtener los datos de las cookies y actualizar la UI
window.onload = function () {
    // Suponiendo que las cookies almacenan el nombre del usuario y su foto de perfil
    var nombreUsuario = getCookie('nombreUsuario'); // Cookie con el nombre del usuario
    var fotoPerfil = getCookie('fotoPerfil'); // Cookie con la ruta de la foto del perfil

    // Si las cookies existen, actualizamos los elementos
    if (nombreUsuario) {
        document.getElementById('nombreUsuario').textContent = nombreUsuario;
        document.getElementById('offcanvasNombreUsuario').textContent = nombreUsuario;
    }

    if (fotoPerfil) {
        document.getElementById('ftoPerfil').src = fotoPerfil;
        document.getElementById('fotoPerfil').src = fotoPerfil;
    }
};



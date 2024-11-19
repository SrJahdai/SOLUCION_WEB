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

$(document).ready(function () {
    var owl = $(".owl-carousel");

    owl.owlCarousel({
        items: 2, // Solo un elemento visible al centro
        loop: true,
        center: true,
        margin: 0, // Espaciado entre tarjetas
        stagePadding: 50, // Espaciado adicional para ver los laterales
        autoplay: true,
        autoplayTimeout: 5000,
        autoplayHoverPause: true, // Pausa autoplay al hacer hover
        nav: false,
        dots: true,
        mouseDrag: false, // Desactiva el desplazamiento con el mouse
        touchDrag: true,
        responsive: {
            0: { items: 1, stagePadding: 0 },
            600: { items: 1, stagePadding: 0 },
            1000: { items: 2, stagePadding: 100 }
        }
    });

    // Acciones para los botones de navegación personalizados
    $(".next-btn").click(function () {
        owl.trigger("next.owl.carousel");
    });

    $(".prev-btn").click(function () {
        owl.trigger("prev.owl.carousel");
    });

    // Comportamiento de los botones prev y next (disabled)
    owl.on("translated.owl.carousel", function (event) {
        // Verifica si los botones de navegación están habilitados o deshabilitados
        if ($(".owl-prev").hasClass("disabled")) {
            $(".prev-btn").addClass("disabled");
        } else {
            $(".prev-btn").removeClass("disabled");
        }

        if ($(".owl-next").hasClass("disabled")) {
            $(".next-btn").addClass("disabled");
        } else {
            $(".next-btn").removeClass("disabled");
        }

        // Obtener el índice del item centrado
        var centerIndex = owl.find(".owl-item.center").index(); // Obtiene el índice del elemento central
        owl.find(".owl-item").each(function (index) {
            var $h2 = $(this).find("aside .escaparate h2");
            var $p = $(this).find("aside .escaparate p");

            // Si el índice no corresponde al elemento central
            if (index !== centerIndex) {
                $h2.addClass("hidden"); // Agrega la clase 'hidden' a los h2 dentro de los elementos no centrados
                $p.addClass("hidden");  // Agrega la clase 'hidden' a los p dentro de los elementos no centrados
            } else {
                $h2.removeClass("hidden"); // Elimina la clase 'hidden' de los h2 dentro del elemento central
                $p.removeClass("hidden");  // Elimina la clase 'hidden' de los p dentro del elemento central
            }
        });
    });

    const sliders = document.querySelectorAll('.slider-container');

    sliders.forEach(slider => {
        const sliderTrack = slider.querySelector('.slider-pelicula');
        const sliderBtns = slider.querySelectorAll('.slider-btn');
        const items = slider.querySelectorAll('.peliculaShow');
        const itemWidth = items[0].offsetWidth + 10;
        let trackPosition = 0;

        // Función para actualizar la visibilidad de los botones de navegación
        function updateButtons() {
            const maxPosition = sliderTrack.scrollWidth - slider.offsetWidth;
            slider.querySelector('.slider-btn-left').classList.toggle('visible', trackPosition < 0);
            slider.querySelector('.slider-btn-right').classList.toggle('visible', trackPosition > -maxPosition);
        }

        // Función para mover el track a la izquierda o derecha
        function moveTrack(direction) {
            const maxPosition = sliderTrack.scrollWidth - slider.offsetWidth; // Máxima posición posible
            const minPosition = 0; // Mínima posición (sin desplazamiento negativo)

            if (direction === 'right' && trackPosition > -maxPosition) {
                trackPosition -= itemWidth; // Mueve a la derecha
            } else if (direction === 'left' && trackPosition < minPosition) {
                trackPosition += itemWidth; // Mueve a la izquierda
            }

            // Aplica el movimiento
            sliderTrack.style.transform = `translateX(${trackPosition}px)`;

            updateButtons(); // Actualiza la visibilidad de los botones
        }


        slider.querySelector('.slider-btn-left').addEventListener('click', function () {
            moveTrack('left');
        });

        slider.querySelector('.slider-btn-right').addEventListener('click', function () {
            moveTrack('right');
        });

        // Funciones de interacción táctil
        let startX = 0;
        let currentX = 0;

        // Al iniciar el toque
        slider.addEventListener('touchstart', function (e) {
            startX = e.touches[0].clientX;
        });

        // Al mover el dedo
        slider.addEventListener('touchmove', function (e) {
            currentX = e.touches[0].clientX;
        });

        // Al finalizar el toque
        slider.addEventListener('touchend', function () {
            if (startX - currentX > 50) {
                moveTrack('right'); // Desliza a la derecha
            } else if (currentX - startX > 50) {
                moveTrack('left'); // Desliza a la izquierda
            }
        });

        // Actualiza la visibilidad de los botones al cargar
        updateButtons();
    });

});

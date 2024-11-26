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

    // Cargar datos de películas desde la API
    $.ajax({
        url: '/api/escaparate/escaparateindex',
        method: 'GET',
        dataType: 'json',
        success: function (data) {
            // Llenar slider de estrenos
            const slider = $('#slider .owl-carousel');
            data.estrenos.forEach(pelicula => {
                slider.append(`
                    <aside class="escaparate id-${pelicula.id}">

                    <img src="${pelicula.portada}" alt="${p.titulo}" style="width: 100%; height: 100%;">
                        <div class="row">
                            <col-2>
                                <h2>${pelicula.titulo}</h2>
                            </col-2>
                            <div class="col-8">
                                <p>
                                    ${pelicula.descripcion}
                                </p>
                            </div>

                        </div>
                    </aside>
                `);
            });

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
                        $h2.addClass("hidden");
                        $p.addClass("hidden");
                    } else {
                        $h2.removeClass("hidden");
                        $p.removeClass("hidden");
                    }
                });
            });



            // Llenar sección de películas recientes
            const recientesContainer = $('#recientePelicula');
            data.recientes.forEach(p => {
                recientesContainer.append(`
                        <div class="peliculaShow ${p.id}">
                         <img src="${p.portada}" alt="${p.titulo}" style="width: 100%; height: 100%;">
                         </div>
                    `);
                            });
                            $('#recientePelicula').append(`
                    <div class="mostrarMas bi bi-plus-circle" id="recientesMas"></div>
                `);

            // Llenar películas por género
            const generosContainer = $('#generos-container');
            data.peliculasPorGenero.forEach(genero => {
                const generoSection = $(`
                        <div class="slider-container ">

                            <h2 class="slider-cabecera ">${genero.genero}</h2>
                            <div class="slider-pelicula">
                             ${genero.peliculas.map(p => `
                                   <div class="peliculaShow">
                                   <img src="${p.portada}" alt="${p.titulo}" style="width: 100%; height: 100%;">
                                   </div>
                            `).join('')}

                                <div id="boton-${genero.genero}" class="mostrarMas bi bi-plus-circle"></div>

                            </div>
                            <button class="slider-btn slider-btn-left"><i class="bi bi-caret-left-fill fs-1"></i></button>
                            <button class="slider-btn slider-btn-right"><i class="bi bi-caret-right-fill fs-1"></i></button>
                        </div>


                `);
                generosContainer.append(generoSection);





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
                        const minPosition = 0;

                        if (direction === 'right' && trackPosition > -maxPosition) {
                            trackPosition -= itemWidth; // Mueve a la derecha
                        } else if (direction === 'left' && trackPosition < minPosition) {
                            trackPosition += itemWidth; // Mueve a la izquierda
                        }
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
        },
        error: function (xhr, status, error) {
            console.error('Error al cargar los datos:', error);
            alert('Hubo un problema al cargar las películas. Intenta de nuevo más tarde.');
        }
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
            const minPosition = 0; 

            if (direction === 'right' && trackPosition > -maxPosition) {
                trackPosition -= itemWidth; // Mueve a la derecha
            } else if (direction === 'left' && trackPosition < minPosition) {
                trackPosition += itemWidth; // Mueve a la izquierda
            }
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

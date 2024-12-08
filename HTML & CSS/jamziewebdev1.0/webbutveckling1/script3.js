
    // Laddar in biblioteket
    jQuery(document).ready(function($) {
        // Hämtar höjden på headern
        var headerHeight = $('header').outerHeight();
        // Hämta navigationsbaren och dess offset från toppen av dokumentet
        var nav = $('nav');
        var navOffset = nav.offset().top;

        // 'Lyssnar' efter scrollhändelser i fönstret
        $(window).scroll(function() {
            // Kontrollerar om användaren har scrollat längre än höjden på sidhuvudet
            if ($(window).scrollTop() > headerHeight) {
                // Om ja, lägg till klassen 'sticky' till navigationsbaren
                nav.addClass('sticky');
            } else {
                // Om användaren har scrollat tillbaka upp till över headern, ta bort klassen 'sticky'
                nav.removeClass('sticky');
            }
        });
    });

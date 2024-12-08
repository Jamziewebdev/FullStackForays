$(document).ready(function() {
    // Funktion som uppdaterar bakgrundsfärgen baserat på fönstrets bredd
    function updateBackgroundColor() {
        // Skapar variabeln 'windowWidth' och tilldelar den värdet av fönstrets bredd
        var windowWidth = $(window).width();

        // Ändra fönstrets färg när skärmen är mindre än 768px, samt färgen på sektioner
        if (windowWidth < 768) {
            $('#wrapper').css('background-color', '#072D50');
            $('article header').css('background-color', '#637585')
            $('section').css('background-color', '#637585')
            $('aside section').css('background-color', '#637585')
        } else {
            $('#wrapper').css('background-color', '#c3c8ce');
            $('article header').css('background-color', '#405970')
            $('section').css('background-color', '#405970')
            $('aside section').css('background-color', '#405970')
        }
    }

    // Kallar på funktionen att uppdatera bakgrundsfärgen
    updateBackgroundColor();

    // Eventlyssnare för fönster-förändring
    $(window).resize(function() {
        // Kallar på funktionen vid fönsterförändringar
        updateBackgroundColor();
    });

    // Notis om tillfällig förändring
    console.log("Denna jQuery-kod är temporär, läs mer om den i script5.js");
});

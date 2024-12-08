// hämtar knappen
let mybutton = document.getElementById("myBtn")
// Visa knappen när användaren skrollar mer än 20px från toppen
window.onscroll = function() {scrollFunction()};

function scrollFunction() {
    // Kolla om användaren har skrollat mer än 20px från toppen av sidan
    if (document.body.scrollTop > 20 || document.documentElement.scrollTop > 20) {
        // Om så är fallet visas knappen med en animation på att den blir synlig och klickbar
        mybutton.style.display = "block";
    } else {
        // Annars tar vi bort knappen för att göra det lättare att navigera i webbsidan
        mybutton.style.display = "none";
    } 
}

//När användaren klickar på knappen, skrolla till toppen av dokumentet
function topFunction() {
    // Skrolla sidans body-element till toppen
    document.body.scrollTop = 0;
    // Skrolla dokumentets html-element till toppen (för äldre webbläsare)
    document.documentElement.scrollTop = 0;
}









// Vänta på att hela dokumentet ska laddas innan koden körs
document.addEventListener('DOMContentLoaded', function () {
    // Hämta referensen till elementet med klassen 'menyikon'
    const menuIcon = document.querySelector('.menyikon');
    const navUl = document.querySelector('nav ul');

 // Hämta referensen till det första <ul>-elementet inuti <nav>-elementet
    menuIcon.addEventListener('click', function () {
        // Ändra visningsstilen för <ul>-elementet beroende på dess nuvarande tillstånd
        navUl.style.display = (navUl.style.display === 'block' ? '' : 'block');
    });

});
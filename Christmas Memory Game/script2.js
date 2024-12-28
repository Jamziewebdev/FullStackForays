document.addEventListener("DOMContentLoaded", function() {
    const behindContainer = document.querySelector(".snowflakes.behind");
    const inFrontContainer = document.querySelector(".snowflakes.in-front");
    const snowflakeCount = 1000; 

    const snowflakeTypes = ["❅", "•", "·"]; 

    // Skapar snöflingor genom att skapa div-element
    for (let i = 0; i < snowflakeCount; i++) {
        const snowflake = document.createElement("div");
        snowflake.classList.add("snowflake");
        snowflake.textContent = snowflakeTypes[Math.floor(Math.random() * snowflakeTypes.length)];
        
        const size = Math.random() * 2 + 0.5 + "em"; 
        snowflake.style.fontSize = size;
        
        const opacity = Math.random() * 0.5 + 0.5;
        snowflake.style.opacity = opacity;
        
        const horizontalPosition = Math.random() * 100 + "%";
        snowflake.style.left = horizontalPosition;

        const verticalPosition = Math.random() * 100 - 100 + "vh"; 
        snowflake.style.top = verticalPosition;

        const duration = Math.random() * 10 + 5 + "s";
        const delay = Math.random() * -10 + "s"; 
        snowflake.style.animationDuration = duration;
        snowflake.style.animationDelay = delay;

        const horizontalTranslation = Math.random() * 40 - 20 + "vw"; 
        snowflake.style.setProperty('--horizontal-translation', horizontalTranslation);

        const skewX = Math.random() * 20 - 10 + "deg"; 
        const skewY = Math.random() * 20 - 10 + "deg"; 
        snowflake.style.transform = `skew(${skewX}, ${skewY})`;

        if (Math.random() < 0.5) {
            behindContainer.appendChild(snowflake);
        } else {
            inFrontContainer.appendChild(snowflake);
        }
    }
});
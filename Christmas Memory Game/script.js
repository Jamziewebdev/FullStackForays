const startBtn = document.getElementById("startBtn");
const flipsCountElement = document.querySelector(".flips span b");
const timeSelect = document.getElementById("time-select");
const timerDisplay = document.getElementById("timer-display");

let firstCard, secondCard;
let isFlipping = false;
let matchedPairs = 0;
let countdown;

const images = [
    'img1.png',
    'img2.png',
    'img3.png',
    'img4.png',
    'img5.png',
    'img6.png'
];

function startGame() {
    resetGame();
    shuffleCards();
    startTimer();
}

function resetGame() {
    for (let i = 1; i <= 12; i++) {
        const card = document.getElementById(`card${i}`);
        card.classList.remove("flip");
        card.addEventListener("click", flipCard);
        toggleView(card, true);
    }
    matchedPairs = 0;
    updateFlipsCount();
    startBtn.textContent = "Starta";
}

function shuffleCards() {
    const shuffledImages = [...images, ...images].sort(() => Math.random() - 0.5);
    for (let i = 1; i <= 12; i++) {
        const card = document.getElementById(`card${i}`);
        const backViewImg = card.querySelector(".back-view img");
        backViewImg.src = `img/${shuffledImages[i - 1]}`;
        toggleView(card, true);
    }
}

function startTimer() {
    let selectedTime = parseInt(timeSelect.value, 10);
    timeSelect.style.display = "none";
    timerDisplay.style.display = "inline";
    timerDisplay.textContent = `${selectedTime} sekunder kvar`;

    clearInterval(countdown);
    countdown = setInterval(() => {
        selectedTime--;
        timerDisplay.textContent = `${selectedTime} sekunder kvar`;
        if (selectedTime <= 0) {
            endGame("Tiden är ute! Försök igen.");
        }
    }, 1000);
}

function flipCard() {
    if (isFlipping || this === firstCard) return;

    this.classList.add("flip");
    toggleView(this, false);

    if (!firstCard) {
        firstCard = this;
        return;
    }

    secondCard = this;
    isFlipping = true;
    checkMatch();
}

function toggleView(card, showFront) {
    const frontView = card.querySelector(".front-view");
    const backView = card.querySelector(".back-view");
    if (showFront) {
        frontView.style.display = "flex";
        backView.style.display = "none";
    } else {
        frontView.style.display = "none";
        backView.style.display = "flex";
    }
}

function checkMatch() {
    const firstImage = firstCard.querySelector(".back-view img").src;
    const secondImage = secondCard.querySelector(".back-view img").src;

    if (firstImage === secondImage) {
        matchedPairs++;
        updateFlipsCount();
        disableCards();
    } else {
        unflipCards();
    }
}

// Gör matchade kort oklickbara
function disableCards() {
    firstCard.removeEventListener("click", flipCard);
    secondCard.removeEventListener("click", flipCard);
    resetFlipState();

    if (matchedPairs === 6) {
        endGame("Grattis! Du har matchat alla par!");
    }
}

// Vänder tillbaka kort som inte matchar
function unflipCards() {
    setTimeout(() => {
        firstCard.classList.remove("flip");
        secondCard.classList.remove("flip");
        toggleView(firstCard, true);
        toggleView(secondCard, true);
        resetFlipState();
    }, 1000);
}

function resetFlipState() {
    [firstCard, secondCard] = [null, null];
    isFlipping = false;
}

function updateFlipsCount() {
    flipsCountElement.textContent = matchedPairs;
}

function endGame(message) {
    clearInterval(countdown);
    alert(message);
    startBtn.textContent = "Starta om";
    for (let i = 1; i <= 12; i++) {
        const card = document.getElementById(`card${i}`);
        card.removeEventListener("click", flipCard);
    }
    timeSelect.style.display = "inline";
    timerDisplay.style.display = "none";
}

startBtn.addEventListener("click", startGame);
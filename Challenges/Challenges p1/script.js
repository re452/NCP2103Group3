document.addEventListener("DOMContentLoaded", () => {
    const templateCards = document.querySelectorAll(".template-card");
    const mainPanel = document.getElementById("mainPanel");
    const emptyState = document.getElementById("emptyState");
    const createBtn = document.getElementById("createBtn");
    const customBtn = document.getElementById("customChallengeBtn");

    let challengeContainer = null;
    const MAX_CHALLENGES = 8;
    let savedChallenges = JSON.parse(localStorage.getItem("pawgressChallenges") || "[]");

    // 🐾 RENDER A CHALLENGE
    function renderChallenge(challenge) {
        if (!challengeContainer) {
            challengeContainer = document.createElement("div");
            challengeContainer.classList.add("challenge-container");
            mainPanel.insertBefore(challengeContainer, createBtn);
        }

        const challengeCard = document.createElement("div");
        challengeCard.classList.add("challenge-item", challenge.difficulty);

        const difficultyLabel =
            challenge.difficulty === "custom"
                ? "CUSTOM"
                : challenge.difficulty?.toUpperCase() || "EASY";

        const exercisesText = Array.isArray(challenge.exercises)
            ? challenge.exercises.join(", ")
            : challenge.exercises || "Your personalized tasks!";

        challengeCard.innerHTML = `
            <h4>${challenge.title}</h4>
            <span class="difficulty-label">${difficultyLabel}</span>
            <div class="exercise-tooltip">${exercisesText}</div>
        `;

        const removeBtn = document.createElement("button");
        removeBtn.innerHTML = "&times;";
        removeBtn.classList.add("remove-btn");
        challengeCard.appendChild(removeBtn);

        removeBtn.addEventListener("click", e => {
            e.stopPropagation();
            challengeCard.remove();
            savedChallenges = savedChallenges.filter(c => c.title !== challenge.title);
            localStorage.setItem("pawgressChallenges", JSON.stringify(savedChallenges));

            if (challengeContainer.querySelectorAll(".challenge-item").length === 0) {
                emptyState.style.display = "block";
                createBtn.classList.remove("bottom-right");
                challengeContainer.remove();
                challengeContainer = null;
            }
        });

        emptyState.style.display = "none";
        createBtn.classList.add("bottom-right");
        challengeContainer.appendChild(challengeCard);
    }

    // 🐾 CUTE MESSAGE
    function showQuoteMessage(message) {
        if (document.querySelector(".quote-message")) return;
        const quote = document.createElement("div");
        quote.classList.add("quote-message");
        quote.textContent = message;
        document.body.appendChild(quote);

        setTimeout(() => {
            quote.classList.add("fade-out");
            setTimeout(() => quote.remove(), 500);
        }, 2500);
    }

    // 🐾 RESTORE SAVED CHALLENGES
    savedChallenges.forEach(ch => renderChallenge(ch));

    // 🐾 HANDLE PREMADE CHALLENGE CLICK
    templateCards.forEach(card => {
        card.addEventListener("click", () => {
            if (challengeContainer && challengeContainer.querySelectorAll(".challenge-item").length >= MAX_CHALLENGES) {
                showQuoteMessage("Too many challenges can tire your paws! 🐾 Take it easy and rest a bit!");
                return;
            }

            const title = card.querySelector("h3")?.innerText || "Untitled Challenge";
            const difficulty = card.classList.contains("easy")
                ? "easy"
                : card.classList.contains("medium")
                ? "medium"
                : "hard";
            const exercises = card.querySelector("p")?.innerText || "";

            const newChallenge = { title, difficulty, exercises };
            savedChallenges.push(newChallenge);
            localStorage.setItem("pawgressChallenges", JSON.stringify(savedChallenges));
            renderChallenge(newChallenge);
        });
    });

    // 🐾 CUTE MODAL CREATION
    function showCustomModal() {
        const modal = document.createElement("div");
        modal.classList.add("custom-modal");
        modal.innerHTML = `
            <div class="custom-modal-content">
                <h3>Name Your Challenge 🐾</h3>
                <input type="text" id="challengeNameInput" placeholder="Enter challenge name..." />
                <div class="modal-buttons">
                    <button id="confirmCustomBtn">Confirm</button>
                    <button id="cancelCustomBtn">Cancel</button>
                </div>
            </div>
        `;
        document.body.appendChild(modal);

        const input = modal.querySelector("#challengeNameInput");
        const confirm = modal.querySelector("#confirmCustomBtn");
        const cancel = modal.querySelector("#cancelCustomBtn");

        input.focus();

        confirm.addEventListener("click", () => {
            const name = input.value.trim();
            if (!name) return;

            const selectedTasks = JSON.parse(localStorage.getItem("pawgressSelectedTasks") || "[]");
            const exercises = selectedTasks.length ? selectedTasks : ["No tasks selected"];

            const newCustom = {
                title: name,
                difficulty: "custom",
                exercises
            };

            savedChallenges.push(newCustom);
            localStorage.setItem("pawgressChallenges", JSON.stringify(savedChallenges));
            renderChallenge(newCustom);
            modal.remove();
        });

        cancel.addEventListener("click", () => modal.remove());
    }

    // 🐾 HANDLE CUSTOM BUTTON CLICK
    if (customBtn) {
        customBtn.addEventListener("click", () => {
            if (challengeContainer && challengeContainer.querySelectorAll(".challenge-item").length >= MAX_CHALLENGES) {
                showQuoteMessage("Too many challenges can tire your paws! 🐾 Take it easy and rest a bit!");
                return;
            }
            showCustomModal();
        });
    }
});

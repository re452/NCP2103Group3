window.addEventListener("load", function() {
  setTimeout(() => {
    const winkEye = document.getElementById("winkEye");
    winkEye.style.animation = "wink 3s ease-in-out";
  }, 2500);
});

document.getElementById("enterBtn").addEventListener("click", function() {
  const nameInput = document.getElementById("petName").value.trim();
  const petGreeting = document.getElementById("petGreeting");

  if (nameInput !== "") {
    petGreeting.textContent = Woof! Hi, I'm ${nameInput}, your new companion! 🐶;
    petGreeting.classList.remove("hidden");
  } else {
    petGreeting.textContent = "Please enter a name first!";
    petGreeting.classList.remove("hidden");
  }
});
document.addEventListener("DOMContentLoaded", () => {
  let animelsContainers;
  let categoryButtons = document.querySelectorAll("button.btn.btn-warning.m-2");
  function getAnimels() {
    animelsContainers = document.querySelectorAll("div.container.animel");
  }
  categoryButtons.forEach((a) =>
    a.addEventListener("click", (event) => {
      filter(a.innerText);
    })
  );

  function filter(category) {
    getAnimels();
    animelsContainers.forEach((a) => {
      if (category === "All categories") {
        a.classList.remove("d-none");
      } else if (!a.classList.contains(category)) {
        a.classList.add("d-none");
      } else {
        a.classList.remove("d-none");
      }
    });
  }
});

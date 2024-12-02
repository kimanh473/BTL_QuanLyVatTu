// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
document.addEventListener("DOMContentLoaded", function () {
    const toggleButton = document.getElementById("toggle-theme");
    const htmlElement = document.documentElement;
    const isDarkMode = localStorage.getItem("dark-mode") === "true";
    if (isDarkMode) {
        htmlElement.setAttribute("data-bs-theme", "dark");
        toggleButton.textContent = "Tắt chế độ tối";
    }

    toggleButton.addEventListener("click", function () {
        if (htmlElement.hasAttribute("data-bs-theme")) {
            htmlElement.removeAttribute("data-bs-theme");
            toggleButton.textContent = "Chế độ tối";
            localStorage.setItem("dark-mode", "false");
        } else {
            htmlElement.setAttribute("data-bs-theme", "dark");
            toggleButton.textContent = "Tắt chế độ tối";
            localStorage.setItem("dark-mode", "true");
        }
    });
});




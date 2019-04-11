// Cambio de tema Oscuro | Claro
function toggleStyleSheet() {
    let style = document.getElementById("theme");
    let styleSheet = style.getAttribute("href");

    if (styleSheet == "/Content/dark-theme.css") style.setAttribute("href", "/Content/light-theme.css");
    else style.setAttribute("href", "/Content/dark-theme.css");
}

function notYect() {
    alert("Aun no programado t.t");
}
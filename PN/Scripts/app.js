// Event Liseners and Subcriptions on Document Ready
$(document).ready(function () {
    // Preview img before upload
    $("#image-file").change(function () {
        readURL(this);
    });
});

// Cambio de tema Oscuro | Claro
function toggleStyleSheet() {
    let style = document.getElementById("theme");
    let styleSheet = style.getAttribute("href");

    if (styleSheet == "/Content/dark-theme.css") style.setAttribute("href", "/Content/light-theme.css");
    else style.setAttribute("href", "/Content/dark-theme.css");
}

// alerta funcion no programada
function notYect() {
    alert("Aun no programado t.t");
}

// cambia los efectos del boton de suscripción
function subcription(control) {
    if (control.getAttribute("class") == "btn btn-default navbar-btn") {
        control.setAttribute("class", "btn btn-info navbar-btn")
    } else {
        control.setAttribute("class", "btn btn-default navbar-btn")
    }
}

// guarda los cookies de terminos y condiciones
function aceptCookieConditions() {
    $("#cookieConsent").remove();
    $.cookie("cookieString", "acepted", { expires: 18250, path: '/' });
}

// Preview img before upload
function readURL(input) {

    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            $('#img-to-upload').attr('src', e.target.result);
        }

        reader.readAsDataURL(input.files[0]);
    }
}
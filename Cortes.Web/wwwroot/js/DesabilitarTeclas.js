$(document).ready(function () {
    $(window).keydown(function (event) {
        if (event.keyCode == 116) {
            event.preventDefault();
            return false;
        }
    });
});
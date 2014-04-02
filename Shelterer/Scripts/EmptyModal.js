$(document).ready(function () {
    var button = $(".btn-remote");
    var fun = button.click;
    button.click(function () {
        $(button.attr("data-target")).empty(".modal-content");
        fun();
    });
});
// Write your JavaScript code.

$(function () {
    var url = window.location.href;

    $(".navbar-collapse.collapse a").each(function () {
        if (url == (this.href)) {
            $(this).closest("li").addClass("active");
        }
    });
});    
// Write your JavaScript code.

$(function() {
    var url = window.location.href;

    $("[data-hide]").on("click", function () {
        $("." + $(this).attr("data-hide")).hide();
        /*
         * via https://stackoverflow.com/a/13550556/2014064
        */
    });

    $(".navbar-collapse.collapse a").each(function() {
        if (url == (this.href)) {
            $(this).closest("li").addClass("active");
        }
    });  
});   

function CheckTestStatus(testNumber) {

    if (testNumber != null) {
        $.post("/Home/CheckTestStatus",
            {
                testNumber: testNumber
            },
            function(result) {
                if (result === "Ended") {
                    $("#alert-text").text("Niestety, spóźniłeś się...");
                    $("#tile-open-alert").show();
                }
            }).fail(
            function(xhr) {
                $("#alert-text").text("Ups! Coś poszło nie tak...");
                $("#tile-open-alert").show();
            }
        );
    } else {
        $("#alert-text").text("Poczekaj na otwarcie okienka!");
        $("#tile-open-alert").show();
    }  
}
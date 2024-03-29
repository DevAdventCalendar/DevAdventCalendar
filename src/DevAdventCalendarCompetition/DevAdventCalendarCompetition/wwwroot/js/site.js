// Write your JavaScript code.

$(function () {
    var url = window.location.href;

    if (document.location.pathname === "/Results/Index") {
        GetResults(1);
    }

    $("[data-hide]").on("click", function () {
        $("." + $(this).attr("data-hide")).hide();
        /*
         * via https://stackoverflow.com/a/13550556/2014064
        */
    });

    $(".navbar-collapse.collapse a").each(function () {
        if (url == (this.href)) {
            $(this).closest("li").addClass("active");
        }
    });

    var count = 0;
    $("#addAnswer").on("click", function () {
        var ans = $("#answer").clone().appendTo("#answers");
        ans.find("input").val("");
        if (count === 0) {
            $("#removeAnswer").removeClass(function (index, currentClass) {
                return "hidden";
            });
        }
        count++;
    });

    $("#removeAnswer").on("click", function () {
        $("#answers #answer").last().remove();
        count--;
        if (count === 0) {
            $("#removeAnswer").addClass(function (index, currentClass) {
                return "hidden";
            });
        }
    });
    $('[data-toggle="tooltip"]').tooltip();
    /*
     * via https://getbootstrap.com/docs/4.1/components/tooltips
    */

    $('[data-toggle="tab"]').on("click", function(event) {
      GetResults(event.target.getAttribute("data-week"),
          event.target.getAttribute("data-page"));
    });
});

function GetResults(weekNumber, pageIndex) {
    $.get("/Results/RenderResults",
        { pageIndex: pageIndex, weekNumber: weekNumber }, function (result) {
            $("#resultsPanel").html(result);
        });
}

function CheckTestStatus(testNumber) {
    if (testNumber != null) {
        $.get("/Home/CheckTestStatus",
            {
                testNumber: testNumber
            },
            function (result) {
                if (result === "Ended") {
                    $("#alert-text").text("Niestety, spóźniłeś się...");
                    $("#tile-open-alert").show();
                }
            });
    } else {
        $("#alert-text").text("Nie możesz wejść! Cierpliwości...");
        $("#tile-open-alert").show();
    }
}

function GetMysteriousName() {
    $.get("/mysterious_name",
        function(result) {
            console.log(result);
        });
}

function refreshAt(hours, minutes, seconds) {
    var now = new Date();
    var then = new Date();

    if (now.getHours() > hours ||
        (now.getHours() == hours && now.getMinutes() > minutes) ||
        now.getHours() == hours && now.getMinutes() == minutes && now.getSeconds() >= seconds) {
        then.setDate(now.getDate() + 1);
    }
    then.setHours(hours);
    then.setMinutes(minutes);
    then.setSeconds(seconds);

    var timeout = (then.getTime() - now.getTime());
    setTimeout(function () { window.location.reload(true); }, timeout);
}
refreshAt(13, 00, 0);

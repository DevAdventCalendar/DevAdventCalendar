var aspect = "3:4",
    aspect_width = parseInt(aspect.split(":")[0]),
    aspect_height = parseInt(aspect.split(":")[1]),
    container = $(".puzzle-container"),
    imgContainer = container.find("figure"),
    img = imgContainer.find("img"),
    path = img.attr("src"),
    piece = $("<div/>"),
    piece_width = Math.floor(img.width() / aspect_width),
    piece_height = Math.floor(img.height() / aspect_height),
    idCounter = 0,
    positions = [],
    empty = {
        top: 0,
        left: 0,
        bottom: piece_height,
        right: piece_width
    },
    moveCounter = 0,
    started = false,
    startTime,
    puzzleUrl = "/puzzletest";

var pieceNormalPositions = { properties: [
    { id: 0, position: { top: 0, left: 0 }},
    { id: 1, position: { top: 0, left: piece_width }},
    { id: 2, position: { top: 0, left: piece_width * 2 }},
    { id: 3, position: { top: piece_height, left: 0}},
    { id: 4, position: { top: piece_height, left: piece_width }},
    { id: 5, position: { top: piece_height, left: piece_width * 2 }},
    { id: 6, position: { top: piece_height * 2, left: 0 }},
    { id: 7, position: { top: piece_height * 2, left: piece_width}},
    { id: 8, position: { top: piece_height * 2, left: piece_width * 2}},
    { id: 9, position: { top: piece_height * 3, left: 0}},
    { id: 10, position: { top: piece_height * 3, left: piece_width}},
    { id: 11, position: { top: piece_height * 3, left: piece_width * 2}},
]};

$(function () {
    for (var x = 0, y = aspect_height; x < y; x++) {
        for (var a = 0, b = aspect_width; a < b; a++) {
            var top = piece_height * x,
                left = piece_width * a;
            var clonedTile = piece.clone();
            clonedTile.attr("id", idCounter++)      
            .css({
                width: piece_width,
                height: piece_height,
                position: "absolute",
                top: top,
                left: left,
                backgroundImage: ["url(", path, ")"].join(""),
                backgroundPosition: ["-", piece_width * a, "px ", "-", piece_height * x, "px"].join("")
            }).appendTo(imgContainer);
            $(clonedTile).on('click', { id: idCounter }, tileClickEvent);
            positions.push({ top: top, left: left });
        }
    }

    img.remove();
    container.find("#0").css({ display: "none" });

    $("#start").on("click", function (e) {
        $.ajax({
            type: "post",
            url: puzzleUrl + "/StartGame",
            success: function(data) {
                if(data == 'Test started!') {
                    startTime = new Date();
                    moveCounter = 0;
                    $("#moveCounter").text("Liczba kroków: " + moveCounter);
                    var pieces = imgContainer.children();
                    
                    function shuffle(array) {
                        var i = array.length;
                        if (i === 0) {
                            return false;
                        }

                        while (--i) {
                            var j = Math.floor(Math.random() * (i + 1)),
                            tempi = array[i],
                            tempj = array[j];
                            array[i] = tempj;
                            array[j] = tempi;
                        }
                    }

                    shuffle(pieces);
                    $.each(pieces, function (i) {
                        pieces.eq(i).css(positions[i]);
                    });
                    pieces.appendTo(imgContainer);
                    started = true;
                    $("#start").attr("disabled", true);
                    $("#reset").attr("disabled", false);
                }
            }
        });        
    });

    $("#reset").on("click", function (e) {    
        window.location.reload();
    });

    $(window).on("beforeunload", function() {
        if(started) {
            $.ajax({
                type: "post",
                data: { moveCount: moveCounter },
                url: puzzleUrl + "/ResetGame",
                success: function(data) {
                    location.reload();
                }
            });
        }
    });
});

function tileClickEvent(event) {
    $.ajax({
        type: "get",
        url: puzzleUrl + "/CheckGameStarted",
        success: function(data) {
            if(data === true)
                started = true;
        }
    });

    if(!started) {
        alert("Hej, hej...co tak szybko? By zacząć układać puzzle, musisz najpierw wcisnąć przycisk Start!");
        return;
    }
    else
        moveTile(event);
}

function moveTile(event) {
    var emptyTile = $("#0")[0];
    var emptyTileTop = parseInt(emptyTile.style.top, 10);
    var emptyTileLeft = parseInt(emptyTile.style.left, 10);
    var emptyTileWidth = parseInt(emptyTile.style.width, 10);
    var emptyTileHeight = parseInt(emptyTile.style.height, 10);
    var clickedTile = event.target;
    var clickedTileStyles = event.target.style;
    var clickedTileTop = parseInt(event.target.style.top, 10);
    var clickedTileLeft = parseInt(event.target.style.left, 10);
    var clickedTileWidth = parseInt(event.target.style.width, 10);
    var clickedTileHeight = parseInt(event.target.style.height, 10);  

    if((clickedTileTop == emptyTileTop + emptyTileHeight || 
        clickedTileTop + clickedTileHeight == emptyTileTop) && clickedTileLeft == emptyTileLeft) {
        $(emptyTile).css({
            top: clickedTileTop,
        });

        $(clickedTile).css({
            top : emptyTileTop,
        });
    }
    else if((clickedTileLeft + clickedTileWidth == emptyTileLeft ||
        clickedTileLeft == emptyTileLeft + emptyTileWidth) && clickedTileTop == emptyTileTop) {
        $(emptyTile).css({
            left: clickedTileLeft,
        });

        $(clickedTile).css({
            left : emptyTileLeft,
        });
    }
    else {
        return;
    }

    if(checkGameEndCondition()) {
        var endTime = new Date();
        var gameDuration = Math.ceil((endTime - startTime) / 1000);

        $.ajax({
            type: "post",
            url: puzzleUrl + "/UpdateGameResult",
            data: { moveCount: moveCounter, gameDuration: gameDuration, testEnd: endTime.toISOString().replace("T", " ").replace("Z", "") },
            success: function() {
                alert("Gratulacje! Puzzle ułożyłeś w " + moveCounter + " próbach, a zajęło Ci to " + gameDuration + " sekund.");
            },
            error: function(xhr, status, err) {
                alert("Ups! Wystąpił jakiś błąd.");
            }
        });        
    }
}

function checkGameEndCondition() {
    var result = false;

    var props = pieceNormalPositions.properties;

    for(var i = 0; i < props.length; i++) {
        var elementToCheck = $("#" + props[i].id)[0];

        var topAsInt = parseInt(elementToCheck.style.top);
        var leftAsInt = parseInt(elementToCheck.style.left);

        if(topAsInt == props[i].position.top && 
            leftAsInt == props[i].position.left) {
            result = true;
        }
        else {
            result = false;
            break;;
        }
    };
    
    moveCounter++;
    $("#moveCounter").text("Liczba kroków: " + moveCounter);

    return result;
}
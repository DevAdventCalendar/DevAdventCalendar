(function(){
"use strict";

    var $window = $(window),
        windowWidth = $window.width(),
        windowHeight =  $window.height();

    /* mobile menu */
    var menuToggler = $('.menu_toggler'),
    offsetMenu = $('.offset_menu'),
    menuQuit = $('.cross');
    menuToggler.on('click',function(e){
        e.preventDefault();
      offsetMenu.addClass('visible');
    });
    menuQuit.on('click',function(){
      offsetMenu.removeClass('visible');
    });

    // SNOW FALL INIT
    $('.hero_area,.single_blog_section').snowfall({
      round: true,
      flakeCount : 300,
      maxSpeed : 5,
      maxSize : 5
    });


    /* COUNTDOWN INIT */
    $('.countdown').countdown('2019/12/01', function(event) {
    var $this = $(this).html(event.strftime(''
      + '<li>%D <span>dni</span></li>  '
      + '<li>%H <span>minut</span></li>  '
      + '<li>%M <span>godzin</span></li>  '
      + '<li>%S <span>sekund</span></li> '));

        if (event.strftime('%D%H%M%S') === '00000000') {
            $('#countdown_active').hide(0);
            $('#countdown_inactive').show(0);
        }
    });

    // Replace all SVG images with inline SVG
    $('.svg').each(function(){
        var $img = $(this),
            imgID = $img.attr('id'),
            imgClass = $img.attr('class'),
            imgURL = $img.attr('src');

        $.get(imgURL, function(data) {
            // Get the SVG tag, ignore the rest
            var $svg = $(data).find('svg');

            // Add replaced image's ID to the new SVG
            if(typeof imgID !== 'undefined') {
                $svg = $svg.attr('id', imgID);
            }
            // Add replaced image's classes to the new SVG
            if(typeof imgClass !== 'undefined') {
                $svg = $svg.attr('class', imgClass);
            }

            // Remove any invalid XML tags as per http://validator.w3.org
            $svg = $svg.removeAttr('xmlns:a');

            // Replace image with new SVG
            $img.replaceWith($svg);

        }, 'xml');

    });

    // DONATE DONATION ITEM TAG GENERATION
    $('.item_donation_wrapper .donate_btn').on('click',function(){
        var tagHolder = document.querySelectorAll('.slected_items')[0],
            donationItems = document.forms['donation_items'],
            hiddenField = document.forms['info_form'].selected_items,
            selectedItemsArry =[];
        tagHolder.innerHTML ='';

        for (var i=0; i < donationItems.length; i++){
            if(donationItems[i].checked){
                tagHolder.innerHTML += "<span>" + donationItems[i].value +"</span>";
                selectedItemsArry.push(donationItems[i].value);
            }
        }

        // set the value in hidden field
        hiddenField.setAttribute('value',selectedItemsArry);
    });

    /*========= all sliders js =========*/

    /* Header area Message slider */
    var messageSlider =$('.message_slider');
    messageSlider.owlCarousel({
        loop:true,
        margin: 30,
        nav: false,
        autoplay: true,
        dots: false,
        items:1,
        mouseDrag: false,
        animateIn: "fadeInDown",
        animateOut: "fadeOutDown"
    });

    /* add class on viewport */
    var animatingElement = $('.animate');
    animatingElement.waypoint(function(direction){
      $(this).addClass("position_zero");
    },{
      offset: '90%'
    });

    /* reveal animation on viewport */
    var $revealClass = $('.reveal');
    $revealClass.css({
        'animation-name': 'none',
        'visibility': 'hidden'
    });

    $revealClass.waypoint(function(direction) {
        var animationName = $(this).attr('data-reveal-anim'),
            animDelay = $(this).attr('data-anim-delay'),
            animDuration = $(this).attr('data-anim-duration');

        $(this).css({
            'animation-name': animationName,
            'data-anim-duration': animDuration,
            '-webkit-animation-delay': animDelay,
            '-moz-animation-delay': animDelay,
            'animation-delay': animDelay,
            'visibility': 'visible'
        });
    },{
        offset: '80%'
    });


    // load evetn content init
    $window.load(function(){
        $('.preloader').fadeOut(500);
        $('.preloader-bg').delay('500').fadeOut(1000);
    });


    // scroll evetn contens init
    $window.scroll(function(){
        var distanceFromTop = $(document).scrollTop();
        if( distanceFromTop > 57){
            menuToggler.addClass('changeColor');
        }
        else{
            menuToggler.removeClass('changeColor');
        }
    });


    /* word count limit */

    $('#wish_message').on('keyup', function (){
        var getText = $(this).val(),
            finalText = "",
            removeExtraSpaces = getText.replace(/\s+/g, ' '),
            separateWordsInArray = removeExtraSpaces.split(' '),
            numberOfWords = separateWordsInArray.length,
            wordLimit = 80,
            i=0,
            wordCount = $('.wordleft');
        wordCount.html(wordLimit - numberOfWords);

        if( numberOfWords > wordLimit ){
            for(i=0; i< wordLimit; i++){
                finalText = finalText+" "+ separateWordsInArray[i]+" ";
                $(this).val(finalText);
            }
        }
        else {return getText;}
    });

    $(function () {
        var url = window.location.href;

        $("[data-hide]").on("click", function () {
            $("." + $(this).attr("data-hide")).hide();
            /*
             * via https://stackoverflow.com/a/13550556/2014064
            */
        });
    });

    function validateEmail(email) {
        var re = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
        return re.test(String(email).toLowerCase());
    }

    $('#mc-embedded-subscribe-form').submit(function(e) {
        $(this).find('.mce_inline_error').remove();

        var $email = $(this).find('input[name="email"]');
        var $name = $(this).find('input[name="name"]');
        var $gdpr = $(this).find('input[name="gdpr"]');
        var $gdprWrapper = $gdpr.closest('.gdpr-wrapper');

        var email = $email.val();
        var name = $name.val();

        var isEmailValid = email && validateEmail(email);
        var isNameValid = name;
        var isGdprChecked = $gdpr.is(':checked');

        if(!isEmailValid) {
            $email.after('<div class="mce_inline_error">Nieprawidłowy email</div>');
        }
        if(!isNameValid) {
            $name.after('<div class="mce_inline_error">To pole jest wymagane</div>');
        }
        if(!isGdprChecked) {
            $gdprWrapper.after('<div class="mce_inline_error">Zaznaczenie zgody jest wymagane</div>');
        }

        if(isEmailValid && isNameValid && isGdprChecked) {
            // submit
        } else {
            e.preventDefault();
        }
    });

})(jQuery);

function CheckTestStatus(testNumber) {
    if (testNumber != null) {
        $.post("/Home/CheckTestStatus",
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
        $("#alert-text").text("Nie możesz wejść!");
        $("#tile-open-alert").show();
    }
}
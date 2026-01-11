var scrolled = false;
var width;
var height;
var demiWidth;
var staticLeft;
var staticTop;
var animated;
var oldScroll;
var $body;
var $container;
var fondCourant = "rien";
var $staticTopBottom;
var $staticLeftRight;

$(document).ready(function () {

   
    if (window.matchMedia("(min-width: 1024px)").matches) {
        StyleCalcule();

        $(window).scroll(function () {
            scrolled = true;
        });

        setInterval(function () {
            if (scrolled) {
                scrolled = false;
                AnimationAvecScroll();

            }
        }, 100);

        
        
    } else {
        StyleCalculeMobile();

        $(window).scroll(function () {
            scrolled = true;
        });

        setInterval(function () {
            if (scrolled) {
                scrolled = false;
                AnimationAvecScrollMobile();

            }
        }, 100);
        
    }


    $(window).resize(function () {
        if (window.matchMedia("(min-width: 1024px)").matches) {
            StyleCalcule();
        }
        else {
            StyleCalculeMobile();
        }
        
    });
    



});

function StyleCalculerAll() {
    

    //$(".iconeAvecTexte .icone").css("width", width / 6.2 + "px")
    //            .css("height", width / 6.2 + "px")
    //            .css("left",- width / 70 + "px")
    //            .css("top",- width / 40 + "px");


    //$("#statistique .icone").mouseover(function () {
    //    $(this).css("width", width / 5.5 + "px")
    //            .css("height", width / 5.5 + "px")
    //            .css("left", -width / 40 + "px")
    //            .css("top", -width / 25 + "px");

    //});

    //$("#statistique .icone").mouseout(function () {
        
    //    $(this).css("width", width / 6.2 + "px")
    //            .css("height", width / 6.2 + "px")
    //            .css("left", -width / 70 + "px")
    //            .css("top", -width / 40 + "px");

    //});


    
    //$(".iconeAvecTexte .graphique").css("height", width / 5 + "px");

    //$(".iconeAvecTexte .icone i").css("margin-top", width / 30 + "px");
     
    //$(".iconeAvecTexte .texte").css("height", width / 9 + "px")
    //                           .css("padding-left", width / 7 + "px");

    //$(".normal").css("padding", width / 70 + "px " + width / 40 + "px ");

    //$(".cercle").css("width", width / 6.2 + "px")
    //            .css("height", width / 6.2 + "px")
    //            .css("left",- width / 70 + "px")
    //            .css("top",- width / 40 + "px");
    
    //$(".cercle i").css("margin-top", width / 30 + "px");
    
    

    

}


function StyleCalculeMobile() {
    $("#page").hide();
    $("#pageMobile").show();

    width = window.outerWidth;
    height = window.outerHeight;

    $("#pageMobile .fondMobile").css("width", width + "px")
        .css("min-height", height + "px");


    
    $("#pageMobile .logo").css("width", width + "px")
        .css("min-height", height + "px");
    
    $(".normal").css("min-height", $(window).height() + "px");
    animated = false;

    animated = false;
    oldScroll = 0;
    $body = $("html");
    $container = $('#container');
    $staticTopBottom = $(".static.topBottom");
    $staticLeftRight = $(".static.leftRight");

    $(window).trigger('scroll');

    StyleCalculerAll();
}

function StyleCalcule() {

    $("#page").show();
    $("#pageMobile").hide();


    width = window.outerWidth;



    if (width < 700) {
        width = 700;
    }

    demiWidth = width * 0.5;

    height = width * $("#parallax .fond").length;

    
    $("#container").css("width", width + "px")
        .css("height", $(window).height() + "px");

    $("#parallax .fond").css("width", width + "px")
        .css("height", $(window).height() + "px");



    $(".normal").css("min-height", $(window).height() + "px");

    $("#page").css("height", height + "px");
    $("#parallax").css("width", height + "px");

    $("#parallax .contenu").css("width", width - 100 + "px")
        .css("height", $(window).height() - 100 + "px");

    $("#parallax .logo").css("width", width + "px")
        .css("height", $(window).height() - 100 + "px");

    $("#texteLogo").css("height", $(window).height() - 100 + "px");

    $("#parallax .fond:last-of-type").css("margin-left", "-12px");
    $("#parallax .fond:last-of-type").css("width", $("#page").width() + "px");
    $("#parallax .fond:last-of-type .contenu").css("width", $("#page").width() - 100 + "px");

    staticLeft = $("#camion").offset().left - 100;


    staticTop = $(".static.topBottom").closest("div").offset().left;

    animated = false;
    oldScroll = 0;
    $body = $("html");
    $container = $('#container');
    $staticTopBottom = $(".static.topBottom");
    $staticLeftRight = $(".static.leftRight");

    $(window).trigger('scroll');

    StyleCalculerAll();
}

function AnimationAvecScrollMobile() {

    var scroll = $(window).scrollTop();

    //$("#test").text("scroll : " + scroll + "    " + "height : " + (height + parseInt($("#statistique").height()) + parseInt($("#phase").height()) + parseInt($("#introduction").height()) + parseInt($("#ethique").height())));

    if (scroll < (height + 100) && fondCourant != "rien") {
        $body.css("background-image", "");
        fondCourant = "rien";
    }

    if (scroll > (parseInt($("#statistique").height()) + parseInt($("#introduction").height())) && scroll < (height * 2 + parseInt($("#statistique").height()) + parseInt($("#ethique").height()) + 200) && fondCourant != "handshake") {
       
        $body.css("background-image", "url(/Content/Images/Handshake.jpg)");
        fondCourant = "handshake";
    }

    if (scroll > (height / 2 - 100 + parseInt($("#statistique").height()) + parseInt($("#phase").height()) + parseInt($("#introduction").height()) + parseInt($("#ethique").height())) && fondCourant != "contact") {
        $body.css("background-image", "url(/Content/Images/Contact-us.jpg)");
        fondCourant = "road";
    }

    if (scroll > (height + parseInt($("#statistique").height()) + parseInt($("#introduction").height()))) {
        $("#lock,#file").addClass("active");

       

        setTimeout(function () {
            $("#lock").addClass("fa-flip-horizontal");
            $("#lock i").removeClass("fa-unlock");
            $("#lock i").addClass("fa-lock");

        }, 1000);

    };

    if (scroll < (parseInt($("#statistique").height()) + parseInt($("#introduction").height()))) {

        $(".cercle, .cercle i").css("transition", "none");
        $("#lock,#file").removeClass("active");
      
        $("#lock").removeClass("fa-flip-horizontal");
        $("#lock i").addClass("fa-unlock");
        $("#lock i").removeClass("fa-lock");
        setTimeout(function () {
            $(".cercle, .cercle i").css("transition", "all 1s linear");
        }, 50);


    };

}

function AnimationAvecScroll() {
    var scroll = $(window).scrollTop();

    $container.scrollLeft(scroll);

    // $("#test").html(scroll + "," + (height + parseInt($("#statistique").height())));

    //$container.stop(true,true).animate({scrollLeft: scroll},100);


    if (scroll < width * 1 && fondCourant != "rien") {
        $body.css("background-image", "");
        fondCourant = "rien";
    }

    if (scroll > width * 1 && scroll < width * 3 && fondCourant != "road") {
        $body.css("background-image", "url(/Content/Images/Road.jpg)");
        fondCourant = "road";
    }

    if (scroll > width * 4 && scroll < (height + parseInt($("#statistique").height()) + parseInt($("#ethique").height()) + 200) && fondCourant != "handshake") {
        $body.css("background-image", "url(/Content/Images/Handshake.jpg)");
        fondCourant = "handshake";
    }

    if (scroll > (height + parseInt($("#statistique").height()) + parseInt($("#ethique").height()) + 200) && fondCourant != "contact") {
        $body.css("background-image", "url(/Content/Images/Contact-us.jpg)");
        fondCourant = "road";
    }

    if (scroll > width * 4) {

        $("#parallax .fond").hide();
    }
    else {
        $("#parallax .fond").show();
    }

    if (scroll > (height + parseInt($("#statistique").height()) - 100)) {
        $("#lock,#file").addClass("active");
       
        setTimeout(function () {
            $("#lock").addClass("fa-flip-horizontal");
            $("#lock i").removeClass("fa-unlock");
            $("#lock i").addClass("fa-lock");

        }, 1000);

    };

    if (scroll < height) {

        $(".cercle, .cercle i").css("transition", "none");
        $("#lock,#file").removeClass("active");
        
        $("#lock").removeClass("fa-flip-horizontal");
        $("#lock i").addClass("fa-unlock");
        $("#lock i").removeClass("fa-lock");
        setTimeout(function () {
            $(".cercle, .cercle i").css("transition", "all 1s linear");
        }, 50);
        

    };

    if (scroll + 200 > staticLeft && scroll < staticLeft + width) {
        $staticLeftRight.css("top", "60%")
                .css("left", "100px")
                .css("position", "fixed");
    } else if (scroll > staticLeft + width) {
        $staticLeftRight.css("top", "60%")
                .css("position", "absolute")
                .css("left", width + "px");

    } else if (scroll + 200 < staticLeft) {
        $staticLeftRight.css("top", "60%")
                .css("left", "-300px")
                .css("position", "absolute");

    }

    if (scroll > oldScroll) {
        if (scroll + demiWidth > staticTop && scroll + demiWidth < staticTop + width && !animated) {

            animated = true;
            var nbGoutte;

            if (width < 700) {
                nbGoutte = randomIntFromInterval(10, 20);
            }
            else {
                nbGoutte = randomIntFromInterval(25, 100);
            }

            var html = '';

            for (i = 0; i < nbGoutte; i++) {
                var em = randomIntFromInterval(2, 10);
                var left = randomIntFromInterval(10, 75);

                html += '<div class="static topBottom" style="top:-200px;left:' + left + '%;">';
                html += '<i class="fa fa-tint" style="font-size:' + em + 'em;color:black"></i>';
                html += '</div>';
            }

            $("#drop").append(html);


            $(".static.topBottom").each(function () {

                var $that = $(this);
                var randomTop = randomIntFromInterval(200, 600);
                $that.css("top", -randomTop + "px");

                var top = (parseInt($(window).height()) + randomTop - 150);
                var randomAnimate = randomIntFromInterval(750, 1500);

                $that.animate({
                    top: "+=" + top
                }, randomAnimate, function () {

                    $that.remove();


                });
            });


        }

        if ($(".leftRight #droite").length == 0) {
            var html = "";
            html += '<i class="fa fa-cloud" style="margin-right:5px;color:whitesmoke"></i>';
            html += '<i class="fa fa-cloud" style="margin-right: 10px; font-size: 1.5em; color: whitesmoke"></i>';
            html += '<i id="droite" class="fa fa-truck" style="transform: scaleX(-1);color:gray;font-size:10em"></i>';

            $(".leftRight").empty();
            $(".leftRight").append(html);
        }



    } else {
        if (scroll + demiWidth < staticTop && animated) {
            animated = false;

        }

        if ($(".leftRight #gauche").length == 0) {
            var html = "";
            html += '<i class="fa fa-truck" style="color:gray;font-size:10em"></i>';

            html += '<i class="fa fa-cloud" style="margin-left: 10px; font-size: 1.5em; color: whitesmoke"></i>';
            html += '<i id="gauche" class="fa fa-cloud" style="margin-left:5px;color:whitesmoke"></i>';

            $(".leftRight").empty();
            $(".leftRight").append(html);
        }



    }

    oldScroll = scroll;

}

function randomIntFromInterval(min, max) {
    return Math.floor(Math.random() * (max - min + 1) + min);
}


// left: 37, up: 38, right: 39, down: 40,
// spacebar: 32, pageup: 33, pagedown: 34, end: 35, home: 36
var keys = { 37: 1, 38: 1, 39: 1, 40: 1 };

function preventDefault(e) {
    e = e || window.event;
    if (e.preventDefault)
        e.preventDefault();
    e.returnValue = false;
}

function preventDefaultForScrollKeys(e) {
    if (keys[e.keyCode]) {
        preventDefault(e);
        return false;
    }
}

function disableScroll() {
    if (window.addEventListener) // older FF
        window.addEventListener('DOMMouseScroll', preventDefault, false);
    window.onwheel = preventDefault; // modern standard
    window.onmousewheel = document.onmousewheel = preventDefault; // older browsers, IE
    window.ontouchmove = preventDefault; // mobile
    document.onkeydown = preventDefaultForScrollKeys;
    $('body').bind('touchmove', function (e) { e.preventDefault() });
}

function enableScroll() {
    if (window.removeEventListener)
        window.removeEventListener('DOMMouseScroll', preventDefault, false);
    window.onmousewheel = document.onmousewheel = null;
    window.onwheel = null;
    window.ontouchmove = null;
    document.onkeydown = null;
    $('body').unbind('touchmove');
}
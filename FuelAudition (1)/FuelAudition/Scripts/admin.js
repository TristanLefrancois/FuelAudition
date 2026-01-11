

function InitialiserRecherche() {

    $("#critereRecherche").submit(function (e) {
        
       
        e.preventDefault();
        ViderMessageFonctionnel();
        $('#resultatRecherche').toggleClass("cache");
        $.ajax({
            type: 'POST',
            url: urlResultatRecherche,
            data: $("#critereRecherche").serialize() + "&PageCourante=1" + "&Trie=" + $("#Trie").val() + "&SensTrie=" + $("#SensTrie").val(),

            success: function (data) {
                $('#resultatRecherche').empty();
                $('#resultatRecherche').append(data);
                $('#resultatRecherche').toggleClass("cache");
                InitialiserResultatRecherche();
            },
            error: function (request, status, error) {

                ErreurRetourAjax(request, status, error);
            }
        });
    });


}


function InitialiserResultatRecherche() {
    $("i.modifier").click(function () {
        ViderMessageFonctionnel();
        window.location = urlModifier + "/" + $(this).closest("tr").attr("data-id");
    });


    $("[data-trie]").click(function () {
        ViderMessageFonctionnel();
        var trie = $(this).attr("data-trie");
        var sensTrie = "asc";

        if ($("#Trie").val() == trie) {
            if ($("#SensTrie").val() == "asc") {
                sensTrie = "desc";
            }
            else {
                sensTrie = "asc";
            }

        }


        $('#resultatRecherche').toggleClass("cache");
        $.ajax({
            type: 'POST',
            url: urlResultatRecherche,
            data: $("#critereRecherche").serialize() + "&PageCourante=1" + "&Trie=" + trie + "&SensTrie=" + sensTrie,

            success: function (data) {
                $('#resultatRecherche').empty();
                $('#resultatRecherche').append(data);
                $('#resultatRecherche').toggleClass("cache");
                InitialiserResultatRecherche();
            },
            error: function (request, status, error) {

                ErreurRetourAjax(request, status, error);
            }
        });
    });

    $("[data-trie=" + $("#Trie").val() + "] i").removeClass("fa-sort").addClass("fa-sort-" + $("#SensTrie").val());
    $("[data-trie=" + $("#Trie").val() + "]").addClass("active");

    $("[data-page]").click(function () {
        ViderMessageFonctionnel();

        var page = $(this).attr("data-page");
        if (page > 0) {
            $('#resultatRecherche').toggleClass("cache");
            $.ajax({
                type: 'POST',
                url: urlResultatRecherche,
                data: $("#critereRecherche").serialize() + "&PageCourante=" + page + "&Trie=" + $("#Trie").val() + "&SensTrie=" + $("#SensTrie").val(),

                success: function (data) {
                    $('#resultatRecherche').empty();
                    $('#resultatRecherche').append(data);
                    $('#resultatRecherche').toggleClass("cache");
                    InitialiserResultatRecherche();
                },
                error: function (request, status, error) {

                    ErreurRetourAjax(request, status, error);
                }
            });
        }


    });


}


function InitialiserDecision() {
    

    $("#Approuver").click(function () {
        $('#formDecision').attr('action', urlApprouver);

        $("#formDecision").submit();
    });

    $("#Refuser").click(function () {
        $('#formDecision').attr('action', urlRefuser);

        $("#formDecision").submit();
    });
    
}
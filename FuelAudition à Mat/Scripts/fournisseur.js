var $divASupprimer;


function InitialiserFournisseur() {

    $("#fournisseur").submit(function (e) {

        if (culture == "fr-ca") {
            $("#Marge").val($("#Marge").val().replace(".", ","));
        }
        else {
            $("#Marge").val($("#Marge").val().replace(",", "."));
        }

    });

    $('.datetimepicker').datetimepicker({
        locale: culture

    });

    $("#ProvinceId").on('change', function () {
        
        var provinceId = this.value;
        $.ajax({
            type: 'POST',
            url: urlObtenirVilles,
            data: { provinceId : provinceId },

            success: function (data) {
                $('#SectionVilles').empty();
                $('#SectionVilles').append(data);
                
            },
            error: function (request, status, error) {

                ErreurRetourAjax(request, status, error);
            }
        });

    });

}

function InitialiserRecherche() {

    //$("#critereRecherche").submit(function (e) {
    //    e.preventDefault();
    //    ViderMessageFonctionnel();
    //    $('#resultatRecherche').toggleClass("cache");
    //    $.ajax({
    //        type: 'POST',
    //        url: urlResultatRecherche,
    //        data: $("#critereRecherche").serialize(),

    //        success: function (data) {
    //            $('#resultatRecherche').empty();
    //            $('#resultatRecherche').append(data);
    //            $('#resultatRecherche').toggleClass("cache");
    //            InitialiserResultatRecherche();
    //        },
    //        error: function (request, status, error) {

    //            ErreurRetourAjax(request, status, error);
    //        }
    //    });
    //});


}


function InitialiserResultatRecherche() {
    $("i.modifier").click(function () {
        ViderMessageFonctionnel();
        window.location = urlModifier + "/" + $(this).closest("tr").attr("data-id");
    });

    $("i.supprimer").click(function () {
        ViderMessageFonctionnel();

        $("#ConfirmerSuppression .modal-body").empty();
        $("#ConfirmerSuppression .modal-body").append('<div>' + messageSuppression.replace("{0}", $(this).closest("tr").attr("data-titre")) + '</div>');

        var id = $(this).closest("tr").attr("data-id");

        $("#ConfirmerSuppression .btn-primary").unbind("click").click(function () {
            Supprimer(id);
        });
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

function Supprimer(id) {
    $.ajax({
        type: 'POST',
        url: urlSupprimer,
        data: { id: id },

        success: function (data) {

            window.location = data;

        },
        error: function (request, status, error) {

            ErreurRetourAjax(request, status, error);
        }
    });
}


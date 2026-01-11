
$(document).ready(function () {
    $("#fr").click(function () {
        $("#culture").val("fr-ca");
        $("#formCulture").submit();
    });

    $("#en").click(function () {
        $("#culture").val("en-us");
        $("#formCulture").submit();
    });

    $('input[type=checkbox]').iCheck({
        checkboxClass: 'icheckbox_flat-red',
        radioClass: 'iradio_flat-gred'
    });

    CSSCalculer();
   
    $("[type=text][data-val-required], [type=password][data-val-required], select[data-val-required]").closest(".form-group").find("label").after("<span class='obligatoire'>*</span>");


});

function CSSCalculer() {
    $("#iconeAccueil").height($("#iconeAccueil").width());
}

function ErreurRetourAjax(request, status, error) {

    $("#erreurAjax").val(request.responseText);
    
}

function ViderMessageFonctionnel () {
    $(".callout").remove();
}

function RoundFixed(value, exp) {
    if (typeof exp === 'undefined' || +exp === 0)
        return Math.round(value);

    value = +value;
    exp = +exp;

    if (isNaN(value) || !(typeof exp === 'number' && exp % 1 === 0))
        return NaN;

    // Shift
    value = value.toString().split('e');
    value = Math.round(+(value[0] + 'e' + (value[1] ? (+value[1] + exp) : exp)));

    // Shift back
    value = value.toString().split('e');
    return +(value[0] + 'e' + (value[1] ? (+value[1] - exp) : -exp));
}
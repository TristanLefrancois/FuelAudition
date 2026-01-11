
function InitialiserCamion() {

    
    BindSupprimer();

    $("#Ajouter").click(function (e) {
        e.preventDefault();
        
        if ($("#tblClientCamion tr[data-id=" + $("#CamionId").val() + "]").length == 0)
        {
            $("#tblClientCamion tbody").append(HtmlLigneTableau($("#CamionId").val(), $("#CamionId option:selected").text()));
            BindSupprimer();
        }
 
    });


    $("#Camion").submit(function (e) {
       
        var camionsId = [];

        $("#tblClientCamion tbody tr").each(function () {
            camionsId.push($(this).attr("data-id"));
        });

        $("#CamionsId").val(camionsId.join(","));

    });
}


function HtmlLigneTableau(id, nom) {

    var html = '';

    html += '<tr data-id="' + id + '">';
    html += '<td data-col="Nom">';
    html += nom;
    html += '</td>';
    html += '<td class="text-center">';
    html += '<i title="Supprimer" class="fa fa-trash-o fa-lg supprimer"></i>';
    html += '</td>';
    html += '</tr>';
    

    return html;
}


function BindSupprimer() {
    $("i.supprimer").unbind("click").click(function () {
        
        $(this).closest("tr").remove();
    });
}
                                    
                                
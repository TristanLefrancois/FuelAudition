

var dataGraphique;
var old;
var texteVolume = [
    { volume: "5000", texte: "0 - 4999 L" },
    { volume: "10000", texte: "5000 - 9999 L" },
    { volume: "20000", texte: "10000 - 19999 L" },
    { volume: "30000", texte: "20000 - 29999 L" },
    { volume: "50000", texte: "30000 - 49999 L" },
    { volume: "75000", texte: "50000 - 74999 L" },
    { volume: "100000", texte: "75000 L et plus" }
  
];


function InitialiserStatistique() {

    $("#fill").on('ifChanged', function () {
        MettreAJourGraphiquePoint();
    });

    $.ajax({
        type: 'POST',
        url: urlObtenirGraphique,
        datatype: "json",

        success: function (data) {
            dataGraphique = data;

            MettreAJourGraphiquePoint();

        },
        error: function (request, status, error) {

            ErreurRetourAjax(request, status, error);
        }
    });
}

function MettreAJourGraphiquePoint() {
    $("#sommaire").empty();


    $("#chargement").hide();

    var datasets = [];

    
    var fill = $("#fill").iCheck('update')[0].checked;

    var colors = ["rgba(220,220,0,0.4)",
                  "rgba(211, 42, 47, 0.4)",
                  "rgba(150,150,150,0.4)",
                  "rgba(0,150,0,0.4)",
                  "rgba(0, 0, 255, 0.4)",
                  "rgba(0, 0, 0, 0.4)",
                  "rgba(0,200,200,0.4)",
                  "rgba(200,0,200,0.4)",
                  "rgba(50,150,150,0.4)",
                  "rgba(255,0,0,0.4)",
                  "rgba(0,0,125,0.4)",
                  "rgba(0,255,0,0.4)"];

    var points = [];
    $.each(dataGraphique, function (index, item) {
        

        var that = item;
        var datasetIndex = index;  var html = '';
        html += '<div class="col-md-4 col-xs-4 form-group" >';
        html += '<div class="legendeGraphique" style="background-color:' + colors[index] + ';border-color:' + colors[index] + '"></div>';
        html += '<div style="font-weight:bold">' + item.Activite + ", " + item.Province + '</div>';
        html += '<div>' + texteMoyenne + ' : ' + item.Moyenne + '</div>';
        html += '<div>' + texteMediane + ' : ' + item.Mediane;
        html += '<span class="fa-stack infobulle" data-toggle="tooltip" data-placement="right" data-html="true" title="' + texteMedianeInfoBulle + '">';
        html += '<i class="fa fa-circle fa-stack-1x" ></i>';
        html += '<i class="fa fa-question fa-stack-1x"></i>';
        html += '</span></div>';
        html += '<div>' + texteNbClient + ' : ' + item.NbClient;
        html += '<span class="fa-stack infobulle" data-toggle="tooltip" data-placement="right" data-html="true" title="' + texteNbClientInfoBulle + '">';
        html += '<i class="fa fa-circle fa-stack-1x" ></i>';
        html += '<i class="fa fa-question fa-stack-1x"></i>';
        html += '</span></div>';
                                            
        html += '<div style="font-weight:bold">' + texteVotreMarge + '</div>';
        $.each(item.Fournisseurs, function (index, item) {
            html += '<div>' + item.Nom + ' : ' + RoundFixed(item.Marge, 4) + '</div>';
            
            $.each(that.Point, function (i, p) {
              
                if (item.Marge == p.x && item.Volume == p.y) {
                    points.push({ ds: datasetIndex, point: i, x: p.x, marge: item.Marge, fournisseur: item.Nom });
                    return false;
                }
            });
        });

        html += '</div>';


        $("#sommaire").append(html);

        datasets.push({
            label: item.Activite + ", " + item.Province,
            data: item.Point,
            backgroundColor: colors[index],
            pointRadius: 6,
            pointHitRadius: 10,
            pointHoverRadius: 6,
            borderColor: colors[index],
            fill: fill
        });
    });



    var ctx = $("#chart");

    if (old != undefined) {
        old.destroy();

    }

    old = new Chart(ctx, {
        type: 'line',
        data: {
            datasets: datasets,

        },
        options: {
            scales: {
                xAxes: [{
                    type: 'linear',
                    position: 'bottom'
                }]
            },
            showLines: false,
            tooltips: {
                custom: function (tooltip) {

                    // tooltip will be false if tooltip is not visible or should be hidden
                    if (!tooltip) {
                        return;
                    }

                    if (tooltip.body != undefined) {
                        debugger;
                        var marge = tooltip.title[0];
                        var tab = tooltip.body[0].lines[0].split(':');

                        var titre = tab[0];
                        var volume = tab[1];
                       
                        var texte = $.grep(texteVolume, function (item, i) {
                            debugger;
                            return item.volume == volume.trim();
                        })[0].texte;

                        tab = titre.split(',');
                        var activite = tab[0].trim();
                        var province = tab[1].trim();

                        var ds = $.grep(dataGraphique, function (item, i) {
                           
                            return item.Activite == activite && item.Province == province;
                        });
                        
                       
                        var utilMarge = $.grep(points, function (item, i) {
                            return item.x == marge;
                        });
                       
                        if (utilMarge.length > 0) {
                            tooltip.title[0] = titre;
                            tooltip.title[1] = "";
                            tooltip.title[2] = texteVotreMarge;
                            tooltip.title[3] = utilMarge[0].fournisseur + " : " + utilMarge[0].marge;
                            tooltip.title[4] = "";
                            tooltip.body[0].lines[0] = ressourceVolume + " : " + texte;
                            tooltip.body[0].lines[1] = texteMarge + " : " + marge;

                            tooltip.height = 115;

                            tooltip.y = tooltip.y - 30;
                        }
                        else {
                            tooltip.title[0] = titre;
                            tooltip.title[1] = "";
               
                            tooltip.body[0].lines[0] = ressourceVolume + " : " + texte;
                            tooltip.body[0].lines[1] = texteMarge + " : " + marge;

                            tooltip.height = 75;
                            tooltip.y = tooltip.y - 15;
                        }
                        
                        
                    }

                 
                }
            }
        }
    });


    $.each(points, function (index, item) {


        var point = old.getDatasetMeta(item.ds).data[item.point];
        point.custom = point.custom || {};
        point.custom.backgroundColor = "rgba(0,0,0,0.8)";
        point.custom.borderWidth = 5;
        point.custom.radius = 7;
        point.custom.borderColor = "rgba(0,0,0,0.8)";

    });

    old.update();


    $("#legendeNbClient").css("top", $("#chart").outerHeight() / 2 + "px")
    $("#legendeNbClient").show();
    $("#consigne").show();
    $("#legendeMarge").show();
}


//function MettreAJourGraphique() {
//    $("#sommaire").empty();


//    $("#chargement").hide();

//    var datasets = [];


//    var fill = $("#fill").iCheck('update')[0].checked;

//    var colors = ["rgba(220,220,0,0.4)",
//                  "rgba(211, 42, 47, 0.4)",
//                  "rgba(150,150,150,0.4)",
//                  "rgba(0,150,0,0.4)",
//                  "rgba(0, 0, 255, 0.4)",
//                  "rgba(0, 0, 0, 0.4)",
//                  "rgba(0,200,200,0.4)",
//                  "rgba(200,0,200,0.4)",
//                  "rgba(50,150,150,0.4)",
//                  "rgba(255,0,0,0.4)",
//                  "rgba(0,0,125,0.4)",
//                  "rgba(0,255,0,0.4)"];

//    var points = [];
//    $.each(dataGraphique, function (index, item) {

//        var that = item;
//        var datasetIndex = index;  var html = '';
//        html += '<div class="col-md-4 col-xs-4 form-group" >';
//        html += '<div class="legendeGraphique" style="background-color:' + colors[index] + ';border-color:' + colors[index] + '"></div>';
//        html += '<div style="font-weight:bold">' + item.Activite + ", " + item.Volume + ", " + item.Province + '</div>';
//        html += '<div>Groupé par tranche de : ' + item.Step + '</div>';
//        html += '<div>Moyenne : ' + item.Moyenne + '</div>';
//        html += '<div>Médiane : ' + item.Mediane;
//        html += '<span class="fa-stack infobulle" data-toggle="tooltip" data-placement="right" data-html="true" title="La médiane représente la valeur de la marge qui coupe votre groupe en deux, c\'est à dire qu\à ce nombre vous avez autant de données plus élevées que moins élevées. Elle sera différente de la moyenne mathématique du groupe.">';
//        html += '<i class="fa fa-circle fa-stack-1x" ></i>';
//        html += '<i class="fa fa-question fa-stack-1x"></i>';
//        html += '</span></div>';
//        html += '<div>Nb. clients comparés : ' + item.NbClient;
//        html += '<span class="fa-stack infobulle" data-toggle="tooltip" data-placement="right" data-html="true" title="Le nombre total de clients comparés est le nombre de clients de la même classe que vous. Ils utilisent le même type de carburant dans les mêmes quantités mensuelles et opèrent dans le même type d\'activité que votre inscription ""fournisseur"". Il est également de la même province.">';
//        html += '<i class="fa fa-circle fa-stack-1x" ></i>';
//        html += '<i class="fa fa-question fa-stack-1x"></i>';
//        html += '</span></div>';
                                            
//        html += '<div style="font-weight:bold">Votre Marge</div>';
//        $.each(item.Fournisseurs, function (index, item) {
//            html += '<div>' + item.Nom + ' : ' + RoundFixed(item.Marge, 4) + '</div>';

//            $.each(that.Point, function (i, p) {

//                if (item.Marge < p.x) {
//                    points.push({ ds: datasetIndex, point: i, x: p.x, marge: item.Marge, fournisseur: item.Nom });
//                    return false;
//                }
//            });
//        });

//        html += '</div>';


//        $("#sommaire").append(html);

//        datasets.push({
//            label: item.Activite + ", " + item.Volume + ", " + item.Province,
//            data: item.Point,
//            backgroundColor: colors[index],
//            pointRadius: 6,
//            pointHitRadius: 10,
//            pointHoverRadius: 6,
//            borderColor: colors[index],
//            fill: fill
//        });
//    });



//    var ctx = $("#chart");

//    if (old != undefined) {
//        old.destroy();

//    }

//    old = new Chart(ctx, {
//        type: 'line',
//        data: {
//            datasets: datasets,

//        },
//        options: {
//            scales: {
//                xAxes: [{
//                    type: 'linear',
//                    position: 'bottom'
//                }]
//            },
//            tooltips: {
//                custom: function (tooltip) {

//                    // tooltip will be false if tooltip is not visible or should be hidden
//                    if (!tooltip) {
//                        return;
//                    }

//                    if (tooltip.body != undefined) {
//                        debugger;
//                        var marge = tooltip.title[0];
//                        var tab = tooltip.body[0].lines[0].split(':');

//                        var titre = tab[0];
//                        var nbClient = tab[1];

//                        tab = titre.split(',');
//                        var activite = tab[0].trim();
//                        var volume = tab[1].trim();

//                        var ds = $.grep(dataGraphique, function (item, i) {
//                            return item.Activite == activite && item.Volume == volume;
//                        });

//                        var point;
//                        $.each(ds[0].Point, function (i, item) {
                            
//                            if (item.x == marge) {
//                                if (i == 0) {
//                                    point = 0 + " et " + marge;
//                                }
//                                else {
//                                    point = ds[0].Point[i - 1].x + " et " + marge;
//                                }
//                                return false;
//                            }

//                        });
                       
//                        var utilMarge = $.grep(points, function (item, i) {
//                            return item.x == marge;
//                        });

//                        if (utilMarge.length > 0) {
//                            tooltip.title[0] = titre;
//                            tooltip.title[1] = "";
//                            tooltip.title[2] = "Votre marge";
//                            tooltip.title[3] = utilMarge[0].fournisseur + " : " + utilMarge[0].marge;
//                            tooltip.title[4] = "";
//                            tooltip.body[0].lines[0] = "Nb. clients : " + nbClient;
//                            tooltip.body[0].lines[1] = "Marge : entre " + point;

//                            tooltip.height = 115;

//                            tooltip.y = tooltip.y - 30;
//                        }
//                        else {
//                            tooltip.title[0] = titre;
//                            tooltip.title[1] = "";
               
//                            tooltip.body[0].lines[0] = "Nb. clients : " + nbClient;
//                            tooltip.body[0].lines[1] = "Marge : entre " + point;

//                            tooltip.height = 75;
//                            tooltip.y = tooltip.y - 15;
//                        }
                        
                        
//                    }

                 
//                }
//            }
//        }
//    });


//    $.each(points, function (index, item) {


//        var point = old.getDatasetMeta(item.ds).data[item.point];
//        point.custom = point.custom || {};
//        point.custom.backgroundColor = "rgba(0,0,0,0.8)";
//        point.custom.borderWidth = 5;
//        point.custom.radius = 7;
//        point.custom.borderColor = "rgba(0,0,0,0.8)";

//    });

//    old.update();


//    $("#legendeNbClient").css("top", $("#chart").outerHeight() / 2 + "px")
//    $("#legendeNbClient").show();
//    $("#consigne").show();
//    $("#legendeMarge").show();
//}
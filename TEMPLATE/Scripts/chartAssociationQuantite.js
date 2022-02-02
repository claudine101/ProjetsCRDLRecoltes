
$(document).ready(
function () {
    $.getJSON("../Association/GetData", function (data) {
        var Names = []
        var Serie = []
        var Qts = []
        var data1 = [];
        var serie1 = "";
        for (var i = 0; i < data.length; i++) {
            Names.push(data[i].name);
            Qts.push(data[i].count);
            var serie1 = new Array(data[i].name, data[i].count, "key:"+data[i].key);
            data1.push(serie1);
 
        }
        DreawChart(data1);

    });
});


function DreawChart(series) {
    Highcharts.chart('containerAssociationQuantite', {
        chart: {
            type: 'columnpyramid'
        },
        title: {
            text: 'RAPPORT DES QUANTITES QUI EXISTE  DANS LES ASSOCIATIONS'
        },
        colorByPoint: true,
        plotOptions: {
            series: {
                cursor: 'pointer',
                point: {
                    events: {
                        click: function () {
                            //$("#myModal").modal();
                            alert("ID=" + this.Key + "NOM=" + this.name + "Nombre=" + this.y)
                            //$("#mytable").DataTable(
                            //    {

                            //    "processing": true,
                            //    "serverSide": true,
                            //    "bDestroy": true,
                            //    "oreder": [],
                            //    "ajax": {
                            //        url: "Home/GetId",
                            //        type: "POST",
                            //        data: {
                            //            key: this.key
                            //        }
                            //    },
                            //    lengthMenu: [[10, 50, 100, row_count], [10, 50, 100, "All"]],
                            //    pageLength: 10,
                            //    "columnDefs": [{
                            //        "targets": [],
                            //        "orderable": false
                            //    }],

                            //    dom: 'Bfrtlip',
                            //    buttons: [
                            //    'excel', 'print', 'pdf'
                            //    ],
                            //    language: {
                            //        "sProcessing": "Traitement en cours...",
                            //        "sSearch": "Rechercher&nbsp;:",
                            //        "sLengthMenu": "Afficher _MENU_ &eacute;l&eacute;ments",
                            //        "sInfo": "Affichage de l'&eacute;l&eacute;ment _START_ &agrave; _END_ sur _TOTAL_ &eacute;l&eacute;ments",
                            //        "sInfoEmpty": "Affichage de l'&eacute;l&eacute;ment 0 &agrave; 0 sur 0 &eacute;l&eacute;ment",
                            //        "sInfoFiltered": "(filtr&eacute; de _MAX_ &eacute;l&eacute;ments au total)",
                            //        "sInfoPostFix": "",
                            //        "sLoadingRecords": "Chargement en cours...",
                            //        "sZeroRecords": "Aucun &eacute;l&eacute;ment &agrave; afficher",
                            //        "sEmptyTable": "Aucune donn&eacute;e disponible dans le tableau",
                            //        "oPaginate": {
                            //            "sFirst": "Premier",
                            //            "sPrevious": "Pr&eacute;c&eacute;dent",
                            //            "sNext": "Suivant",
                            //            "sLast": "Dernier"
                            //        },
                            //        "oAria": {
                            //            "sSortAscending": ": activer pour trier la colonne par ordre croissant",
                            //            "sSortDescending": ": activer pour trier la colonne par ordre d&eacute;croissant"
                            //        }
                            //    }

                            //});

                        }
                    }
                }
            }
        },

        xAxis: {
            crosshair: true,
            labels: {
                style: {
                    fontSize: '12px'
                }
            },
            type: 'category'
        },
        yAxis: {

            title:
            {
                text: 'QUANTITE'
            },

        },
        tooltip: {
            valueSuffix: ' KG'
        },
        series: [{
            name: 'Quantite',
            colorByPoint: true,
            data: series,
            showInLegend: false
        }]
    });
}

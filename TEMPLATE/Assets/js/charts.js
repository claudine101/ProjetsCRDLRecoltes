
$(document).ready(
function () {
    $.getJSON("association/GetData", function (data) {
        var Names = []
        var Seriea = []
        var Serie = []
        var Qts = []
        var Qts1 = ["{name: 'Fire',y: 12.57,}"]
        //var t=langDict = new Dictionary();
        //t.Add(data[i].count, '"' + data[i].name + '"');

        var data1 = [];
        var dat1 = [];
        var seri;
        var serie1 = "";

        alert(serie1);
        for (var i = 0; i < data.length; i++) {
            Names.push(data[i].name);
            Qts.push(data[i].count);

            //seri ="{name:'" + data[i].name + "', y:" + data[i].count + ",key:'" + data[i].id + "'}";

            var serie1 = new Array(data[i].name, data[i].count, data[i].key);
            data1.push(serie1);
            dat1.push(seri);

            //Serie.push("{ name: '" + data[i].name + "',y: " + data[i].count + "}");
            //Seriea.push(" {name: '" + str_replace("'", "\'", $key['nom_produit']) + '' + "',y: " + data[i].count + ",key: ID },");
            //Serie.push("{name: '" + Replace("'", "\'", data[i].name) + "',y: " + data[i].count + "},");

        }
        alert(data1)
        //dat1.push(seri);
        DreawChart(data1);

    });
});
function DreawChart(series) {

    $('#container').highcharts({
        chart: {
            plotBackgroundColor: null,
            plotBorderWidth: 1, //null,
            plotShadow: false
        },
        title: {
            text: 'fruit  market shares at a specific month, 2014'
        },
        tooltip: {
            pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
        },
        plotOptions: {
            pie: {
                allowPointSelect: true,
                cursor: 'pointer',
                dataLabels: {
                    enabled: true,
                    format: '<b>{point.name}</b>: {point.percentage:.1f} %',
                    style: {
                        color: (Highcharts.theme && Highcharts.theme.contrastTextColor) || 'black'
                    }
                }
            }
        },
        series: [{
            type: 'pie',
            name: 'Fruit share',
            data: series
        }]
    });
}
function DreawChart(series) {
    Highcharts.chart('container1', {
        chart: {
            type: 'columnpyramid'
        },
        title: {
            text: 'The 5 highest pyramids in the World'
        },
        colorByPoint: true,
        plotOptions: {
            series: {
                cursor: 'pointer',
                point: {
                    events: {
                        click: function () {
                            //$("#myModal").modal();
                            alert("ID=" + this.key + "NOM=" + this.name + "Nombre=" + this.y)
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
                text: 'AMAHERA'
            },

        },
        tooltip: {
            valueSuffix: ' FBU'
        },
        series: [{
            name: 'Amahera',
            colorByPoint: true,
            data: series,
            showInLegend: false
        }]
    });
}
Highcharts.chart('container2',
{
    chart:
    {
        type: 'columnpyramid'
    },
    title:
    {
        text: 'The 5 highest pyramids in the World'
    },

    colors: ['#C79D6D', '#B5927B', '#CE9B84', '#B7A58C', '#C7A58C'],
    plotOptions: {
        series: {
            cursor: 'pointer',
            point: {
                events: {
                    click: function () {
                        //$("#myModal").modal();
                        alert("ID=" + this.key + "NOM=" + this.name + "Nombre=" + this.y)
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
    xAxis:
    {
        crosshair: true,
        labels:
        {
            style:
            {
                fontSize: '14px'
            }
        },
        type: 'category'
    },
    yAxis: {
        min: 0,
        title: {
            text: 'Height (m)'
        }
    },
    tooltip: {
        valueSuffix: ' m'
    },
    series: [{
        name: 'Height',
        colorByPoint: true,
        data: [
           { name: 'claudine', y: 1000, key: 1 }, { name: 'NDAYISABA', y: 1000, key: 1 }

        ],
        showInLegend: false
    }]
});
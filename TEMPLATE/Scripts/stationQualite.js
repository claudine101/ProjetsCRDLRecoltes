$(document).ready(
function () {
    $.getJSON("../RecolteStation/GetDataQualite", function (data) {
        var NamesClient = []
        var SerieClient = []
        var Nombre = []
        var dataClient = [];
        for (var i = 0; i < data.length; i++) {
            NamesClient.push(data[i].name);
            Nombre.push(data[i].count);
            var SerieClient = new Array(data[i].name, data[i].count, "key:" + data[i].key);
            dataClient.push(SerieClient);
        }
        DreawChart(dataClient);

    });
});

function DreawChart(dataClient) {
    Highcharts.chart('containerQualite', {
        chart: {
            type: 'column'
        },
        title: {
            text: 'RAPPORT  DE CLIENT QUI AMENE LES CAFES DANS LES STATIONS DE LAVAGE '
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
                text: 'Quantite'
            },

        },
        tooltip: {
            valueSuffix: 'KG'
        },
        series: [{
            name: 'Quantite',
            colorByPoint: true,
            data: dataClient,
            showInLegend: false
        }]
    });
}

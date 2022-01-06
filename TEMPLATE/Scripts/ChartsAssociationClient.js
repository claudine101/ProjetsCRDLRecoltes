$(document).ready(
function () {
    $.getJSON("../Association/GetDataClient", function (data) {
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

    $('#containerAssociationClients').highcharts({
        chart: {
            plotBackgroundColor: null,
            plotBorderWidth: 1, //null,
            plotShadow: false
        },
        title: {
            text: 'RAPPORT  DE CLIENT QUI EXISTE LES ASSOCIATIONS'
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
            name: 'Membre',
            data: dataClient
        }]
    });
}

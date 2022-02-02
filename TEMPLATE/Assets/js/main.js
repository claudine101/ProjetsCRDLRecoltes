$(function () 
{
    $('#ID_province').on('change', function () {
        var id = $(this).val();
        $.get('../association/getCommune', { id: id }, function (data) {
            $('#ID_commune').empty();
            $('#ID_commune').append("<option value=''>selectionnez commune</option>")
            $.each(data, function (index, row) {
                $('#ID_commune').append("<option value='" + row.ID_commune + "'>" + row.NOM_commune + "</option>")
            });
        });
    });
});

//Getting zone
$(function () {
    $('#ID_commune').on('change', function () {
        var ID = $(this).val();
        $.get('/association/getZone', { ID: ID }, function (data) {
            $('#ID_zone').empty();
            $('#ID_zone').append("<option value=''>selectionnez zone</option>")
            $.each(data, function (index, row) {
                $('#ID_zone').append("<option value='" + row.ID_zone + "'>" + row.NOM_zone + "</option>")
            });
        });
    });
});
//Getting colline
$(function () {
    $('#ID_zone').on('change', function () {
        var ID = $(this).val();
        $.get('/association/getColline', { ID: ID }, function (data) {
            $('#ID_colline').empty();
            $('#ID_colline').append("<option value=''>selectionnez colline</option>")
            $.each(data, function (index, row) {
                $('#ID_colline').append("<option value='" + row.ID_colline + "'>" + row.NOM_colline + "</option>")
            });
        });
    });
});
//POUR PRIX DE QUALITE
$(function () {
    $('#ID_qualite').on('change', function () {
        var ID_qualite = $(this).val();
        $.get('/Recolte/getPrix', { ID_qualite: ID_qualite }, function (data) {
            $('.Prix').val(data.PrixUnitaire);
        });
    });
});

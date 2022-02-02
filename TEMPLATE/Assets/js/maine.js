
//POUR CLIENT DE L'ASSOCIATION
$(function () {
    $('#ID_association').on('change', function () {
        var ID = $(this).val();
        $.get('/RecolteStation/getClient', { ID: ID }, function (data) {
            $('#ID_client').empty();
            $('#ID_client').append("<option value=''>selectionnez le client</option>")
            $.each(data, function (index, row) {
                $('#ID_client').append("<option value='" + row.ID_client+ "'>" + row.NOM_client + "</option>")
            });
        });
    });
});
    //AFFICHAGE PAR PROVINCE
    $(function () 
    {
        $('#ID_provinc').on('change', function () {
            var id = $(this).val();
            $.get('/Association/getDonne', { id: id }, function (data) {
                $('#mytable').empty();
                $('#mytable').append(
                       "<tr>"
                            + "<th>NO</th>"
                            + " <th></th>"
                            + " <th>NOM </th>"
                            + " <th></th>"
                            + "<th>TEL</th>"
                            + " <th></th>" 
                            + " <th>COLLINE</th>"
                   + "</tr>")
                var No = 0;
                $.each(data, function (index, row) {
                    No = No + 1;
                    $('#mytable').append(
                        "<tr>"
                             + "<td>" + No + "<td>"
                             + "<td>"+ row.assocition+"<td>"
                             + "<td>" + row.tel + "<td>"
                             + "<td>" + row.colline + "<td>"                    
                    + "</tr>")

                });
            
            });
        });
    });


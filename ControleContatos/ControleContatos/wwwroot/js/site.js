$(document).ready(function () {
    getDataTable("#table-listUsers");
    getDataTable("#table-listCandidatos");

    $('.btn-total-candidatos').click(function () {
        var userId = $(this).attr('user-id');

        $.ajax({
            type: 'GET',
            url: '/User/ListarCandidatosPorUserId/' + userId,
            success: function (result) {

                $("#ListaCandidatosUsers").html(result);
                $('#modalCandidatosUsers').modal('show');
                getDataTable("#table-listModal");
            }
        });
    });

    $('.btn-delete-candidatos').click(function () {
        $('#modalCandidatoDelete').modal('show');
    });

    $('.btn-delete-users').click(function () {
        $('#modalUserDelete').modal('show');
    });
});



function getDataTable(id) {
    $(id).DataTable({
        "ordering": true,
        "paging": true,
        "searching": true,
        "oLanguage": {
            "sEmptyTable": "Nenhum Registro encontrado na tabela",
            "sInfo": "Mostrar _START_ até _END_ de _TOTAL_ Registros",
            "sInfoEmpty": "Mostrar 0 até 0 de 0 Registros",
            "sInfoFiltered": "(Filtrar de _MAX_ total Registros)",
            "sInfoPostFix": "",
            "sInfoThousands": ".",
            "sLengthMenu": "Mostrar _MENU_ Registros por Pagina",
            "sLoadingRecords": "Carregando...",
            "sProcessing": "Processando...",
            "sZeroRecords": "Nenhum registro encontrado",
            "sSearch": "Pesquisar",
            "oPaginate": {
                "sNext": "Proximo",
                "sPrevious": "Anterior",
                "sFirst": "Primeiro",
                "sLast": "Ultimo"
            },
            "oAria": {
                "sSortAscending": ": Ordenar colunas de forma ascendente",
                "sSortDescending": ": Ordenar colunas de forma descendente"
            }
        }
    });
}

$('.close-alert').click(function () {
    $('.alert').hide('hide');
})
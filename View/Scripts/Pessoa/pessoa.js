$(function () {
    $idAlterar = -1;

    $tabelaPessoa = $('#pessoa-tabela').DataTable({
        ajax: 'http://localhost:51800/Pessoa/obtertodos',
        serverSide: true,
        columns: [
            { 'data': 'Id' },
            { 'data': 'Nome' },
            { 'data': 'CPF' },
            {
                render: function (data, type, row) {
                    return '<button class="btn btn-primary botao-editar" data-id="' + row.Id + '">Editar</button>\<button class="btn btn-danger botao-apagar" data-id="' + row.Id + '">Apagar</button>'

                }

            }

        ]
    });

    $('#pessoa-botao-salvar').on('click', function () {
        $nome = $('#pessoa-campo-nome').val();
        $cpf = $('#pessoa-campo-cpf').val();

        if ($idAlterar == -1) {
            inserir($nome, $cpf);
        } else {
            alterar($nome, $cpf);
        }
    });

    function alterar($nome, $cpf) {
        $.ajax({
            url: "http://localhost:51800/pessoa/update",
            method: "post",
            data: {
                id: $idAlterar,
                nome: $nome,
                cpf: $cpf
            },
            success: function (data) {
                $("#modal-pessoa").modal("hide");
                $idAlterar = -1;
                $tabelaPessoa.ajax.reload();    
            },
            error: function (err) {
                alert("Não foi possível alterar");
            }
        })
    }

    function inserir($nome, $cpf) {
        $.ajax({
            url: 'http://localhost:51800/pessoa/inserir',
            method: 'post',
            data: {
                nome: $nome,
                cpf: $cpf
            },
            success: function (data) {
                $('#modal-pessoa').modal('hide');
                $tabelaPessoa.ajax.reload();
            },
            error: function (err) {

            }
        });
    }

    $('.table').on('click', '.botao-apagar', function () {
        $idApagar = $(this).data('id');

        $.ajax({
            url: 'http://localhost:51800/pessoa/apagar?id=' + $idApagar,
            method: 'get',
            success: function (data) {
                $tabelaPessoa.ajax.reload();
            },

            error: function (err) {
                alert('Não foi possível apagar');
            }

        });
    });

    $('.table').on('click', '.botao-editar', function () {
        $idAlterar = $(this).data('id');

        $.ajax({
            url: 'http://localhost:51800/pessoa/obterpeloid?id=' + $idAlterar,
            method: 'get',

            success: function (data) {
                $('#pessoa-campo-nome').val(data.Nome);
                $('#pessoa-campo-cpf').val(data.CPF);
                $('#modal-pessoa').modal('show');
            },
            error: function (err) {
                alert('não foi possível carregar');
            }
        });
    });
});

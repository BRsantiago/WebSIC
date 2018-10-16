function Datatables(idTable) {
    var tableRendered = null;

    tableRendered = $('#' + idTable).DataTable({
        responsive: true,
        paging: true,
        select: true,
        select: {
            style: 'multi',
            selector: 'td:first-child'
        },
        "language": {
            "url": "//cdn.datatables.net/plug-ins/1.10.16/i18n/Portuguese-Brasil.json",
            "sEmptyTable": "Nenhum registro encontrado.",
            select: {
                rows: {
                    _: "%d linhas selecionadas.",
                    0: "Clique na linha para selecionar.",
                    1: "1 linha selecionada."
                }
            }
        }
    });

    return tableRendered;
}


function onShowModalCreateRepresentante(id) {
    $.ajax({
        type: 'GET',
        url: '/Representante/Create/',
        data: { idEmpresa: id },
        cache: false,
        dataType: 'html',
        success: function (data) {
            $('#modal').html(data);
            $('#modal').appendTo("body").modal('show');
        },
        error: function (xhr, ajaxOptions, thrownError) {
            //TrataErroAjax(xhr);
            alert("erro");
        }
    });
};

function onShowModalEdit(id) {

    var params = { id: id };

    $.ajax({
        type: 'GET',
        url: '@Url.Action("Edit", "Representante")',
        data: params,
        cache: false,
        dataType: 'html',
        success: function (data) {
            $('#modal').html(data);
            $('#modal').modal('show');
        },
        error: function (xhr, ajaxOptions, thrownError) {
            //TrataErroAjax(xhr);
            alert("erro");
        }
    });
};


function onShowModalDelete(id) {

    var params = { id: id };

    $.ajax({
        type: 'GET',
        url: '@Url.Action("Delete", "Representante")',
        data: params,
        cache: false,
        dataType: 'html',
        success: function (data) {
            $('#modal').html(data);
            $('#modal').modal('show');
        },
        error: function (xhr, ajaxOptions, thrownError) {
            //TrataErroAjax(xhr);
            alert("erro");
        }
    });
};

function SalvarNovoRepresentante() {
    
    var form = $("form");
    $.ajax({
        url: form.attr("action"),
        method: form.attr("method"),  // post
        data: form.serialize(),
        success: function (result) {
            if (result.success) {
                swal({
                    title: "Good job!",
                    text: "Representante cadastrado com sucesso!",
                    icon: "success",
                    button: "OK!"
                })
                    .then((value) => {
                        $('#modal').modal('hide');
                        window.location = '/Empresa/Edit/' + $('#IdEmpresa').val();
                    });
            } else {
                swal({
                    title: "Atenção!",
                    text: result.message,
                    icon: "warning",
                    button: "OK!"
                })
                    .then((value) => {
                        $('#modal').modal('hide');
                        window.location = '/Empresa/Edit/' + $('#IdEmpresa').val();
                    });
            }
        },
        error: function () {
            swal({
                title: "Erro de sistema!",
                text: "Por favor entre em contato com o setor de TI!",
                icon: "error"
            });
        }
    });
}

function ObterRepresentandePorCPF() {

    var CPF = $('#CPF').val();

    var parametros = {
        CPF: CPF
    }


    $.ajax({
        url: '/Pessoa/ObterPessoaPorCPF',
        data: JSON.stringify(parametros),
        contentType: "application/json",
        type: 'POST',
        success: function (result) {
            if (result.success) {
                swal({
                    title: "Atenção!",
                    text: result.message,
                    icon: "success"
                });

                $('#IdPessoa').val(result.data.IdPessoa);
                $('#Nome').val(result.data.Nome);
                $('#Apelido').val(result.data.Apelido);
                $('#DataNascimento').val(result.data.DataNascimento);
                $('#NomePai').val(result.data.NomePai);
                $('#NomeMae').val(result.data.NomeMae);
                $('#Endereco').val(result.data.Endereco);
                $('#Numero').val(result.data.Numero);
                $('#Complemento').val(result.data.Complemento);
                $('#Bairro').val(result.data.Bairro);
                $('#Cidade').val(result.data.Cidade);
                $('#UF').val(result.data.UF);
                $('#CEP').val(result.data.CEP);
                $('#TelefoneEmergencia').val(result.data.TelefoneEmergencia);
                $('#TelefoneResidencial').val(result.data.TelefoneResidencial);
                $('#TelefoneCelular').val(result.data.TelefoneCelular);
                $('#RNE').val(result.data.RNE);
                $('#CPF').val(result.data.CPF);
                $('#RG').val(result.data.RG);
                $('#OrgaoExpeditor').val(result.data.OrgaoExpeditor);
                $('#UFOrgaoExpeditor').val(result.data.UFOrgaoExpeditor);
                $('#Genero').val(result.data.Genero);
                $('#Observacao').val(result.data.Observacao);
                $('#FlgCVE').val(result.data.FlgCVE);
                $('#Email').val(result.data.Email);
                $('#CNH').val(result.data.CNH);
                $('#CategoriaCNH').val(result.data.CategoriaCNH);
                $('#DataValidadeCNH').val(result.data.DataValidadeCNH);

            } else {
                swal({
                    title: "Atenção!",
                    text: result.message,
                    icon: "error"
                });
            }

        },
        erro: function (result) {
            swal({
                title: "Erro de sistema!",
                text: "Por favor entre em contato com o TI!",
                icon: "error"
            });
        }
    });
}
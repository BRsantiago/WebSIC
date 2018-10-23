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


function onShowModalCreate(id, tipo) {
    $.ajax({
        type: 'GET',
        url: '/' + tipo + '/Create/',
        data: { id: id },
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

function onShowModalEdit(id, tipo) {

    var params = { id: id };

    $.ajax({
        type: 'GET',
        url: '/' + tipo + '/Edit/',
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

function onShowModalDelete(id, tipo) {

    var params = { id: id };

    $.ajax({
        type: 'GET',
        url: '/' + tipo + '/Delete/',
        data: params,
        cache: false,
        dataType: 'html',
        success: function (data) {
            $('#modal').html(data);
            $('#modal').modal('show');
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert("erro");
        }
    });
};


function onShowModalDeleteRepresentante(idPessoa, idEmpresa, tipo) {

    var params = {
        idPessoa: idPessoa,
        idEmpresa: idEmpresa
    };

    $.ajax({
        type: 'GET',
        url: '/' + tipo + '/Delete/',
        data: params,
        cache: false,
        dataType: 'html',
        success: function (data) {
            $('#modal').html(data);
            $('#modal').modal('show');
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert("erro");
        }
    });
};




function Salvar(form) {

    baseURL = window.location;

    var form = $(form);

    $.ajax({
        url: form.attr("action"),
        method: form.attr("method"),  // post
        data: form.serialize(),
        success: function (result) {
            if (result.success) {
                swal({
                    title: "Good job!",
                    text: result.message,
                    icon: "success",
                    button: "OK!"
                })
                    .then((value) => {
                        $('#modal').modal('hide');
                        window.location = baseURL;
                    });
            } else {
                swal({
                    title: "Atenção!",
                    text: result.message,
                    icon: "warning",
                    button: "OK!"
                })
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


var loadFile = function (event) {
    var output = document.getElementById('output');
    output.src = URL.createObjectURL(event.target.files[0]);
};

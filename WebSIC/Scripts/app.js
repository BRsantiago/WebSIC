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
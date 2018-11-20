var baseURL = null;

$(function () {
    Datatables("table");

    baseURL = window.location;

    //$(".details").click(function () {
    //    var id = $(this).attr("data-id");
    //    //alert(id);
    //    $("#modal").load(baseURL + "/Details?id=" + id, function () {
    //        $("#modal").modal();
    //    })
    //});
    //$(".edit").click(function () {
    //    var id = $(this).attr("data-id");
    //    //alert(id);
    //    $("#modal").load(baseURL + "/Edit?id=" + id, function () {
    //        $("#modal").modal();
    //    })
    //});
    //$(".delete").click(function () {
    //    var id = $(this).attr("data-id");
    //    //alert(id);
    //    $("#modal").load(baseURL + "/Delete?id=" + id, function () {
    //        $("#modal").modal();
    //    })
    //});

    $('#table').on('click', '.details', function () {
        var id = $(this).attr("data-id");
        $("#modal").load(baseURL + "/Details?id=" + id, function () {
            $("#modal").modal();
        })
    });
    $('#table').on('click', '.edit', function () {
        var id = $(this).attr("data-id");
        $("#modal").load(baseURL + "/Edit?id=" + id, function () {
            $("#modal").modal();
        })
    });
    $('#table').on('click', '.delete', function () {
        var id = $(this).attr("data-id");
        $("#modal").load(baseURL + "/Delete?id=" + id, function () {
            $("#modal").modal();
        })
    });
    $(".create").click(function () {
        $("#modal").load(baseURL + "/Create", function () {
            $("#modal").modal();
        })
    });
})

function SubmitForm() {
    var form = $("form");
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
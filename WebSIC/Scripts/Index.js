var baseURL = null;

$(function () {
    Datatables("table");

    baseURL = window.location;

    $(".details").click(function () {
        var id = $(this).attr("data-id");
        //alert(id);
        $("#modal").load(baseURL + "/Details?id=" + id, function () {
            $("#modal").modal();
        })
    });
    $(".edit").click(function () {
        var id = $(this).attr("data-id");
        //alert(id);
        $("#modal").load(baseURL + "/Edit?id=" + id, function () {
            $("#modal").modal();
        })
    });
    $(".delete").click(function () {
        var id = $(this).attr("data-id");
        //alert(id);
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
        success: function () {
            swal({
                title: "Good job!",
                text: "Action perform successfully!",
                icon: "success",
                button: "OK!"
            })
            .then((value) => {
                $('#modal').modal('hide');
                window.location = baseURL;
            });
            
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
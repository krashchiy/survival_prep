$(function () {
    console.log("loaded");
});

function dec_quantity(e, id) {
    var num = parseInt($("#quant_" + id).text(), 10);
    if (num > 1) {
        $("#quant_" + id).text(num - 1);
    }
}

function inc_quantity(e, id) {
    var num = parseInt($("#quant_" + id).text(), 10);
    $("#quant_" + id).text(num + 1);
}

function buy(e, id) {
    var quantity = parseInt($("#quant_" + id).text());
    Swal.fire({
        type: 'warning',
        title: 'Confirmation',
        text: 'Are you sure you want to purchase ' + quantity + " " + $("#name_" + id).text() + '(s) for $' + quantity * parseInt($("#cost_" + id).text()) + '?',
        showCancelButton: true,
        focusConfirm: true,
        confirmButtonText: 'Confirm',
        cancelButtonText: 'Cancel'
    }).then((result) => {
        if (result.value) {
            $.ajax({
                url: "/Shop/BuyItem",
                method: "POST",
                data: {
                    id: id,
                    quantity: quantity
                }
            }).done(function (result) {
                $("#money").text("Money: " + result.money);
                Swal.fire({
                    type: 'success',
                    title: 'Success!',
                    text: 'Succesfully Bought'
                })
            }).fail(function (jqXHR, textStatus, errorThrown) {
                console.log("failed: ");
                console.log(jqXHR);
                console.log(textStatus);
                console.log(errorThrown);
                Swal.fire({
                    type: 'error',
                    title: 'Purchase failed',
                    text: 'You do not have enough money to purchase this item'
                });
            });
        }
    });
}

function toggle_modal(e, id) {
    $.ajax({
        url: "/Shop/ShowDetails",
        method: "POST",
        data: {
            id: id
        }
    }).done(function (result) {
        $("#modalBody").text("Item name: " + result.name + "\n" + "Cost: " + result.cost + "\n" + "Score: " + result.score + "\n" + "Currently owned: " + result.curNum);
    }).fail(function (jqXHR, textStatus, errorThrown) {
        $("#modalBody").text("Error");
    }).always(function () {
        $("#detailModal").modal("show");
    });
}
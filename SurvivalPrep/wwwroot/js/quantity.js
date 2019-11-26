$(function () {
    console.log("loaded");
});

function dec_quantity(e, id) {
    var num = parseInt($("#quant_" + id).text(), 10);
    if (num > 1) {
        $("#quant_" + id).text(num - 1);
    }

    console.log("click");
}

function inc_quantity(e, id) {
    var num = parseInt($("#quant_" + id).text(), 10);
    $("#quant_" + id).text(num + 1);
}
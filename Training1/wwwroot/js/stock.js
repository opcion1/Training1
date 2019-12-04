
$(document)
    .on("blur", "#pricePorUnity,#quantity", function () {
        $(this).val($(this).val().replace(",", "."));
        if ($.isNumeric($("#pricePorUnity").val()) && $.isNumeric($("#quantity").val())) {
            CalculateTotalPrice($("#pricePorUnity").val(), $("#quantity").val());
        }
    });
function CalculateTotalPrice(pricePorUnity, quantity) {
    $("#totalPrice").val(pricePorUnity * quantity);
}
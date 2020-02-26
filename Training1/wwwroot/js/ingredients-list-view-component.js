

$('li.dropdown-item').on('click', function () { 
    $('#input-product').val($(this).text());
});

$('#input-product').on('keyup', function () {
    var search = $(this).val().toLowerCase().trim();
    $('li.dropdown-item')
        .removeClass('li-invisible')
        .filter(function (i, li) {
            return $(li).text().toLowerCase().indexOf(search) < 0;
        })
        .addClass('li-invisible');
    $(this).dropdown('update');

});

/*Binding event of IngredientsList view component*/
$(document).ready(function () {
    $(document).on('click', 'li.dropdown-item', onDropDownSelect);
    $(document).on('keyup', '.input-product', inputProductKeyUp);
    $(document).on("click", "#icon-create", createIngredient);
    $(document).on("click", ".btn-edit-row", function () { editIngredient($(this));});
    $(document).on("click", ".btn-delete-row", function () { deleteIngredient($(this)); });
    $(document).on("click", ".btn-cancel-edit", cancelEdit);
    $(document).on("submit", ".form-ingredient", function (event) {
        event.preventDefault();
        submitIngredient($(this));
    });
});


/*functions*/
function onDropDownSelect() {
    $('#table-ingredients .input-product').val($(this).text());
    $('#table-ingredients .hidden-product').val($(this).attr('id'));
}

function inputProductKeyUp() {
    var search = $(this).val().toLowerCase().trim();
    $('li.dropdown-item')
        .removeClass('li-invisible')
        .filter(function (i, li) {
            return $(li).text().toLowerCase().indexOf(search) < 0;
        })
        .addClass('li-invisible');
    $(this).dropdown('update');

}

function deleteIngredient(btnDelete) {
    if (confirm('Do you really want to delete this ingredient?')) {
        var id = btnDelete.attr('id').replace('btn_delete_', '');
        $.ajax({
            url: '/Ingredients/Delete/' + id,
            type: 'post'
        }).done(function () {
            $('#tr_ingredient_' + id).remove();
        });
    }
}

function createIngredient() {
    removeEditRow();
    var newIngredient = $('.sample-edit-row').clone();
    var form = newIngredient.find('#form-ingredient');
    form.attr('id', 'create-incredient');
    form.attr('action','/Ingredients/Create');

    $('#table-ingredients tbody').prepend(newIngredient);
}

function editIngredient(btnEdit) {
    removeEditRow();
    var id = btnEdit.attr('id').replace('btn_edit_', '');
    //get the ingredient from id
    $.ajax({
        url: '/Ingredients/Details/' + id,
        type: 'get'
    })
        .done(function (data) {
            displayEditRowIngredient(data);
        })
}

function cancelEdit() {
    var isNewIngredient = !$.isNumeric($('#table-ingredients').find('.hidden-ingredient').val());

    if (isNewIngredient) {
        removeEditRow();
    }
    else {
        var idIngredient = $('#table-ingredients').find('.sample-edit-row').attr('id').replace('tr_edit_', '');
        $.ajax({
            url: '/Ingredients/Details/' + idIngredient,
            type: 'get'
        }).done(function (data) {
            displayRowIngredient(data, false);
        });
    }
}

function displayEditRowIngredient(data) {
    var editIngredient = $('.sample-edit-row').clone();
    editIngredient.attr('id', 'tr_edit_' + data.ingredientId);

    var form = editIngredient.find('#form-ingredient');
    fillForm(form, data);

    $('#tr_ingredient_' + data.ingredientId).replaceWith(editIngredient);
}

function fillForm(form, data) {
    form.attr('id', 'edit-incredient');
    form.attr('action', '/Ingredients/Edit');
    form.find('.hidden-ingredient').val(data.ingredientId);
    form.find('.hidden-product').val(data.product.id);
    form.find('.input-product').val(data.product.name);
    form.find('.input-qty').val(data.quantity);
    form.find('.select-unityType').val(data.unityType);
}

function submitIngredient($form) {
    var isNewIngredient = !$.isNumeric($('#table-ingredients .hidden-ingredient').val());
    var json = $form.serialize();

    $.ajax({
        url: $form.attr('action'),
        type: $form.attr('method'),
        data: json
    }).done(function (data) {
        displayRowIngredient(data, isNewIngredient);
    });
}

function displayRowIngredient(data, isNewIngredient) {
    debugger
    var ingredient = $('.sample-row').clone();
    ingredient.removeClass('sample-row');
    ingredient.attr('id', 'tr_ingredient_' + data.ingredientId);
    ingredient.find('product-name').replaceWith(data.product.name);
    ingredient.find('product-qty').replaceWith(data.quantity);
    ingredient.find('product-unityType').replaceWith(getUnityTypeString(data.unityType));
    ingredient.find('edit-ingredient').replaceWith("<a class='btn-edit-row' id='btn_edit_" + data.ingredientId + "' title='edit ingredient'><i class='btn fas fa-pen'></i></a>");
    
    ingredient.find('delete-ingredient').replaceWith("<a class='btn-delete-row' id='btn_delete_" + data.ingredientId + "' title='delete ingredient'><i class='btn fas fa-times'></i></a>");

    if (isNewIngredient) {
        removeEditRow();
        ingredient.appendTo('#table-ingredients tbody');
    }
    else {
        $('#tr_edit_' + data.ingredientId).replaceWith(ingredient);
    }
}

function removeEditRow() {
    $('#table-ingredients .sample-edit-row').remove();
}

function getUnityTypeString(unityType) {
    switch (unityType) {
        case 0:
            return "Grammes";
        case 1:
            return "Kilogrammes";
        case 2:
            return "Liter";
        case 3:
            return "Unity";
    }
}
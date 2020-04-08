
$(document).ready(function () {
    $('.food-name').autocomplete({
        delay: 300,
        minLength: 3,
        source: function (request, response) {
            $.ajax({
                url: '/Foods/Search',
                type: 'GET',
                dataType: 'json',
                data: { searchText: request.term }
                ,
                success: function (data) {
                    response($.map(data, function (item) {
                        return { label: item.name, value: item.name, id: item.foodId, desc: item.description, commentary: item.commentary };
                    }));
                }
            });
        },
        select: function (event, ui) {
            showFood(ui);
        }
    });

    $('#Food_Name').focus(function () {
        $('.input-container').addClass('focused');
    });
    $('#Food_Name').focusout(function () {
        $('.input-container').removeClass('focused');
    });
    $('i.cancel-icon').click(function () {
        cancelFood();
    });
});

function Food(id, name, desc, commentary) {
    this.foodId = id;
    this.name = name;
    this.description = desc;
    this.commentary = commentary;
}

function showFood(ui) {
    debugger
    var food = new Food(ui.item.id, ui.item.label, ui.item.desc, ui.item.commentary);
    displayFood(food);
    $('i.cancel-icon').show();
    $('#Food_Name').addClass('icon');
    $('#Food_Name').addClass('cancel-form-control-focus');
}

function cancelFood() {
    var emptyFood = new Food(-1, '', '', '');
    displayFood(emptyFood);
    $('i.cancel-icon').hide();
    $('#Food_Name').removeClass('icon');
    $('#Food_Name').removeClass('cancel-form-control-focus');
}

function displayFood(food) {
    $('#Food_FoodId').val(food.foodId);
    $('#Food_Name').val(food.name);
    $('#Food_Description').val(food.description);
    $('#Food_Commentary').val(food.commentary);
}
$.validator.methods.number = function (value, element) {
    return this.optional(element) || /^-?(?:\d+|\d{1,3}(?:[\s\.,]\d{3})+)(?:[\.,]\d+)?$/.test(value);
}

$.validator.addMethod('greaterthanstartdate',
    function (value, element, params) {
        debugger
        // Get start and end dates
        var startDate = new Date($('input#StartDate').val());
        var endDate = new Date(value);
        return (endDate > startDate);
    });

$.validator.unobtrusive.adapters.add('greaterthanstartdate',
    function (options) {
        var element = $(options.form).find('input#EndDate')[0];
        options.rules['greaterthanstartdate'] = [element];
        options.messages['greaterthanstartdate'] = options.message;
    });
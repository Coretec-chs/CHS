$.validator.setDefaults({
    highlight: function (element) {
        $(element).closest('.form-group').addClass('has-error');
    },
    unhighlight: function (element) {
        $(element).closest('.form-group').removeClass('has-error');
    },
    highlight: function (element) {
        $(element).closest('.form-group-sm').addClass('has-error');
    },
    unhighlight: function (element) {
        $(element).closest('.form-group-sm').removeClass('has-error');
    },
    errorElement: 'span',
    errorClass: 'help-block',
    errorPlacement: function (error, element) {
        if (element.parent('.input-group').length) {
            error.insertAfter(element.parent());
        } else if (element.parent('.radio-inline').length) {
            error.insertAfter(element.parent().parent());
        } else {
            error.insertAfter(element);
        }
    }
});
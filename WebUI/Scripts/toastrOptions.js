/**
 * Some default options for Toastr
 */
toastr.options = {
    "closeButton": true,
    "debug": false,
    "newestOnTop": false,
    "progressBar": true,
    "positionClass": "toast-top-center",
    "preventDuplicates": false,
    "onclick": null,
    "showDuration": "300",
    "hideDuration": "1000",
    "timeOut": "10000",
    "extendedTimeOut": "1000",
    "showEasing": "swing",
    "hideEasing": "linear",
    "showMethod": "fadeIn",
    "hideMethod": "fadeOut"
};

var toastrAlert = function (alertMessage) {
    if (alertMessage != null && alertMessage != '') {
        var msgarray = alertMessage.split(',');
        if (msgarray[2] == "error") {
            toastr["error"](msgarray[1]);
        } else if (msgarray[2] == "success") {
            toastr["success"](msgarray[1]);
        } else  {
            toastr["info"](msgarray[1]);
        }
    }
};
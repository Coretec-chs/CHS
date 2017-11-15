// Content opening/closing
$(document).ready(function () {
    $('.content-toggle').on('click', function () {
        $(this).toggleClass('open');
        $(this).parents('.widget:first').find('.widget-content').slideToggle();
    });
    Grid.rebind();
    Select2.rebind();
    ImageUpload.rebind();
});

(function () {
    $(document).on('keyup', '#SearchInput', function () {
        var searches = this.value.toLowerCase().split(' ');
        var menus = $('.sidebar li');

        for (var i = 0; i < menus.length; i++) {
            var menu = $(menus[i]);
            var isMatch = true;

            var menuWords = menu.text().toLowerCase().split(' ');
            for (var j = 0; j < searches.length; j++) {
                var hasMatch = false;
                for (var k = 0; k < menuWords.length; k++) {
                    if (menuWords[k].indexOf(searches[j]) >= 0) {
                        hasMatch = true;
                    }
                }

                if (!hasMatch) {
                    isMatch = false;
                }
            }

            if (isMatch) {
                menu.show(500);
            } else {
                menu.hide(500);
            }
        }
    });
}());

//Progress
$(function () {
    $.ajaxSetup({ cache: false });

    $('*[data-load]').click(function () {
        $.blockUI({ message: '<h5><img src="/Content/ajax-loader.gif" /> Loading...</h5>' });
    });

});

$(function () {
    $.ajaxSetup({ cache: false });

    $('*[data-submit]').click(function () {
        //var isvalid = true;
        //var elements = document.querySelectorAll("form")
        //for (var i = 0, element; element = elements[i++];) {
        //    element.validate();
        //    if (!element.valid())
        //        isvalid = false;
        //}

        var form = $(this).parents('form:first');
        form.validate()
        if (form.valid()) {
            $.blockUI({ message: '<h5><img src="/Content/ajax-loader.gif" /> Loading...</h5>' });
        } else {
            $.unblockUI();
        }
    });
});



$(function () {
    $('.datepicker').datetimepicker({
        //defaultDate: new Date(),
        format: 'DD MMM YYYY',
        showTodayButton: true,
        showClear: true,
        showClose: true,
        ignoreReadonly: true
    });

    $('.monthpicker').datetimepicker({
        //defaultDate: new Date(),
        format: 'MMM YYYY',
        showClear: true,
        showClose: true,
        ignoreReadonly: true,
        useCurrent: false,
        viewMode: 'months',
    });



    //$('.datagrid').DataTable({
    //    select: {
    //        style: 'single'
    //    }
    //});

});


var Grid = new (function () {
    this.rebind = function () {
        $('.datagrid').DataTable({
            responsive: true,
            retrieve: true,
            select: {
                style: 'single'
            },
            "columnDefs": [{
                "targets": 'no-sort',
                "orderable": false
            }],
            "columnDefs": [{
                "targets": 'hide-col',
                "visible": false
            }],
            "order": []
        });

        $('.datagrid-button').DataTable({
            responsive: true,
            retrieve: true,
            select: {
                style: 'single'
            },
            "columnDefs": [{
                "targets": 'no-sort',
                "orderable": false
            }],
            "columnDefs": [{
                "targets": 'hide-col',
                "visible": false
            }],
            "order": [],
            dom: 'Bfrtip',
            buttons: [
                'csv', 'excel'
            ]
        });
        $('.datagrid-nosort').DataTable({
            responsive: true,
            retrieve: true,
            select: {
                style: 'single'
            },
            "columnDefs": [{
                "targets": 'no-sort',
                "orderable": false
            }],
            "columnDefs": [{
                "targets": 'hide-col',
                "visible": false
            }],
            "order": []
        });

        $('.gridfilter tfoot th').each(function () {
            var title = $('.gridfilter thead th').eq($(this).index()).text();
            if (title != "") {
                $(this).html('<input type="text" placeholder="Search ' + title + '" style="width:98%" />');
            }
        });

        // DataTable
        var table = $('.gridfilter').DataTable({
            responsive: true,
            retrieve: true,
            select: {
                style: 'single'
            },
            "columnDefs": [{
                "targets": 'no-sort',
                "orderable": false

            }],
            "lengthMenu": [[5, 10, 20], [5, 10, 20]]
        });

        $('.tb-line').dataTable({
            responsive: true,
            retrieve: true,
            select: {
                style: 'single'
            },
            "dom": 't',
            "paging": false,
            "ordering": true,
            "info": false,
            "searching": false,
            "columnDefs": [{
                "targets": 'no-sort',
                "orderable": false
            }],
            "columnDefs": [{
                "targets": 'cl-right',
                "class": 'text-right'
            }]
        });

        // Apply the filter
        $(".gridfilter tfoot input").on('keyup change', function () {
            table
                .column($(this).parent().index() + ':visible')
                .search(this.value)
                .draw();
        });
    };
})();

//var Dropify = new (function () {
//    this.rebind = function () {
//        $('*[data-plugin = "dropify"]').dropify();
//    };
//})();

var Select2 = new (function () {
    this.rebind = function () {
        $('*[data-plugin = "select2"]').select2();
    };
})();

var ImageUpload = new (function () {
    this.rebind = function () {
        $('*[data-plugin = "uploadimage"]').uploadimage();
    };
})();

var DateControl = new (function () {
    this.rebind = function () {
        $('.datepicker').datetimepicker({
            //defaultDate: new Date(),
            format: 'DD MMM YYYY',
            showTodayButton: true,
            showClear: true,
            showClose: true,
            ignoreReadonly: true
        });

        $('.monthpicker').datetimepicker({
            //defaultDate: new Date(),
            format: 'MMM YYYY',
            showClear: true,
            showClose: true,
            ignoreReadonly: true,
            useCurrent: false,
            viewMode: 'months',
        });
    };
})();

var swalConfirm = new (function () {
    this.Delete = function (url, recid, parentid, replaceTaget) {
        swal({
            title: "Are you sure?",
            text: "You will not be able to recover this record!",
            type: "warning",
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes',
            cancelButtonText: 'No',
            confirmButtonClass: 'btn btn-danger',
            cancelButtonClass: 'btn btn-info',
            buttonsStyling: false
        },
                function () {
                    var msg = "";
                    $.ajax({
                        url: url,
                        data: { id: recid, parentId: parentid },
                        beforeSend: function () { $.blockUI({ message: '<h5><img src="/Content/ajax-loader.gif" /> Loading...</h5>' }); },
                        complete: function () { $.unblockUI(); },
                        success: function (data) {
                            if (data.success === true) {
                                $(replaceTaget).replaceWith(data.html);
                                Grid.rebind();
                            }
                            toastrAlert(data.msg);
                        }
                    });
                });
    }
    this.Cancel = function (url, recid) {
        swal({
            title: "Are you sure?",
            text: "You will not be able to undo cancelling!",
            type: "warning",
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes',
            cancelButtonText: 'No',
            confirmButtonClass: 'btn btn-danger',
            cancelButtonClass: 'btn btn-info',
            showLoaderOnConfirm: true,
            closeOnConfirm: false
        },
        function () {
            var msg = "";
            $.ajax({
                url: url,
                type: "POST",
                data: { id: recid },
                success: function (data) {
                    window.location.href = data.redirectTo;
                }
            });
        });
    }
})();












$('.rebind').on('hidden.bs.modal', function (e) {
    var parent = $(this).data("parent");
    var url = $(this).data("url");

    $(this).removeData();

    $.ajax({
        type: "GET",
        url: url,
        success: function (data) {
            // data is ur summary
            $(parent).html(data);
            Grid.rebind();
        }

    });
})

$('.loading').on('shown.bs.modal', function (e) {
    $.unblockUI();
})

$('a[data-toggle="tab"]').on('hide.bs.tab', function (e) {

    var parent = $(e.relatedTarget).data("parent");
    var url = $(e.relatedTarget).data("url");

    $.ajax({
        type: "GET",
        url: url,
        beforeSend: function () {
            $.blockUI({ message: '<h5><img src="/Content/ajax-loader.gif" /> Loading...</h5>' });
        },
        complete: function () {
            $.unblockUI();
        },
        dataType: 'html',
        success: function (data) {
            // data is ur summary
            $(parent).html(data);
            Grid.rebind();
            Select2.rebind();
            ImageUpload.rebind();
        }
    });
})

$('#smartwizard').on("showStep", function (e, anchorObject, stepNumber, stepDirection, stepPosition) {    
    var seltab = $(anchorObject[0]);
    var parent = seltab.data('parent') && seltab.data('parent').length > 0 ? seltab.data('parent') : '';
    var url = seltab.data('url') && seltab.data('url').length > 0 ? seltab.data('url') : '';
    if (url != '') {
        $.ajax({
            type: "GET",
            url: url,
            beforeSend: function () {
                $.blockUI({ message: '<h5><img src="/Content/ajax-loader.gif" /> Loading...</h5>' });
            },
            complete: function () {
                $.unblockUI();
            },
            dataType: 'html',
            success: function (data) {
                // data is ur summary
                $(parent).html(data);                
                Grid.rebind();
                Select2.rebind();
                ImageUpload.rebind();
            }
        });
    }
    if (stepNumber == 6) {
        $('.btn-finish').removeClass('disabled');
    } else {
        $('.btn-finish').addClass('disabled');
    }
    return true;
})
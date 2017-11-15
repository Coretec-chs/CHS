; (function (root, factory) {
    if (typeof define === 'function' && define.amd) {
        define(['jquery'], factory);
    } else if (typeof exports === 'object') {
        module.exports = factory(require('jquery'));
    } else {
        root.UploadImage = factory(root.jQuery);
    }
}(this, function ($) {
    var pluginName = "uploadimage";

    /**
     * UploadImage plugin
     *
     * @param {Object} element
     * @param {Array} options
     */
    function UploadImage(element, options) {

        this.element = element;
        this.input = $(this.element);
        this.imageCtl = this.input.find("embed");
        this.fileCtl = this.input.children('figcaption').find(".upload-img");
        this.viewCtl = this.input.children('figcaption').find(".view-img");
        this.clearCtl = this.input.children('figcaption').find(".clear-img");

        this.clearElement = this.clearElement.bind(this);
        //this.onChange = this.onChange.bind(this.fileCtl);
        this.readFile = this.readFile.bind(this);
        this.viewFile = this.viewFile.bind(this);

        this.createElements();
    }

    UploadImage.prototype.onChange = function () {
        //this.resetPreview();
        this.readFile();
    };

    UploadImage.prototype.createElements = function () {
        this.isInit = true;
        this.clearCtl.on('click', this.clearElement);
        this.fileCtl.on('change', this.readFile);
        this.viewCtl.on('click', this.viewFile);

    };

    UploadImage.prototype.clearElement = function () {
        this.imageCtl.attr("src", "#");
        this.fileCtl.val('');
        this.viewCtl.attr('data-filepath', '')
    };

    UploadImage.prototype.readFile = function (input) {
        if (input.target.files && input.target.files[0]) {
            var reader = new FileReader();
            var _this = this;
            reader.onload = function (e) {
                _this.imageCtl.attr('src', e.target.result);
                _this.viewCtl.attr('data-filepath', e.target.result)
            }
            reader.readAsDataURL(input.target.files[0]);
        }
    };

    UploadImage.prototype.viewFile = function () {
        var src = this.viewCtl.attr("data-filepath");
        if (src != '' && src != null) {
            window.open(src, '_blank');
        }
    };

    /**
 * Destroy UploadImage
 */
    UploadImage.prototype.destroy = function () {
        this.input.siblings().remove();
        this.isInit = false;
    };

    /**
     * Init UploadImage
     */
    UploadImage.prototype.init = function () {
        this.createElements();
    };

    /**
     * Test if element is init
     */
    UploadImage.prototype.isDropified = function () {
        return this.isInit;
    };

    $.fn[pluginName] = function (options) {
        this.each(function () {
            if (!$.data(this, pluginName)) {
                $.data(this, pluginName, new UploadImage(this, options));
            }
        });

        return this;
    };


    return UploadImage;
}));

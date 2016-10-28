; (function ($) {
    var BusinessObj = {
        Init: function () {
            var _this = this;

            _this.InitEvent();
            alert("Init");

            return _this;
        },
        InitEvent: function () {
            var _this = this;

            return _this;
        }
    };

    BusinessObj.Init();
})(jQuery);
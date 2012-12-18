define(['jquery',
        'singlePage/app',
        'jquery.loadingIndicator'], function ($, app) {

    var bindLoadingIndicator = function () {

        var $main = $('#main');

        app.$events.bind('loadView', function() {

            if (!$main.data('loadingIndicator')) {
                $main.loadingIndicator();
            }

            $main.data('loadingIndicator').show();
        });

        app.$events.bind('viewLoaded', function () {
            $main.data('loadingIndicator').hide();
        });
    };
    
    $(bindLoadingIndicator);
});
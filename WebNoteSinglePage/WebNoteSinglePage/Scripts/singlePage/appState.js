define(['singlePage/app',
        'jquery',
        'jquery.history',
        'singlePage/urlParser',
        'singlePage/bindLoadingIndicator',
        'singlePage/bindRefreshView'], function (app, $, historyJs, urlParser) {

    var init = function() {

        $(function() {
            handleChangedState();
        });

        $(window).bind("statechange", function(e) {
            handleChangedState();
        });
    };

    // newParam is optional
    var changeState = function (newViewId, newParam) {

        var newUrl = urlParser.buildQuerystring(newViewId, newParam);
        historyJs.pushState(null, window.document.title, newUrl);
        app.loadView(newViewId, newParam);
    };

    var handleChangedState = function () {

        var state = historyJs.getState();
        var fragments = urlParser.parseQuerystring(state.url);
        app.loadView(fragments.viewId, fragments.param);
    };

    var reload = function () {
        // temporary workaround, since handleChangedState(); is buggy here! :-(
        window.location.reload();
    };

    return {
        init: init,
        changeState: changeState,
        reload: reload
    };
});
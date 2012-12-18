define(function () {

    // joins viewId and param to an url querystring
    var buildQuerystring = function (viewId, param) {

        var queryString = "?viewId=" + viewId;

        if (param) {
            queryString += "&param=" + param;
        }

        return queryString;
    };

    // splits ?viewId=111param&param=xx into its fragments
    var parseQuerystring = function (url) {

        var viewId = getValueByName(url, 'viewId');
        var param = getValueByName(url, 'param');

        return {
            viewId: viewId || 'index',
            param: param || undefined
        };
    };
    
    var getValueByName = function (fragment, name) {
        var results = new RegExp('[\\?&]' + name + '=([^&#]*)').exec(fragment);
        if (!results) {
            return undefined;
        }

        return results[1] || undefined;
    };

    return {
        buildQuerystring: buildQuerystring,
        parseQuerystring: parseQuerystring
    };
});
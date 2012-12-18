define(['jquery',
        'knockout.mapping'], function ($, mapping) {

    var loadModel = function (activeParam, callback) {

        $.ajax('api/notes/' + activeParam).done(function (xhr) {

            var model = mapping.fromJS(xhr);
            callback.call(model);
        });
    };

    return { loadModel: loadModel };
});
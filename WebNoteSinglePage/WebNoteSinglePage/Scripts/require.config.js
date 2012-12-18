requirejs.config({
    // enforceDefine gives us 404 load detection in IE
    // --> this requires an 'exports' for ALL shims so that they can be checked for successfull loading
    //     (if they do not call define on their own)
    enforceDefine: true,
    baseUrl: "Scripts",
    paths: {
        'jquery': 'jquery-1.8.3',
        'knockout': 'knockout-2.2.0',
        'knockout.mapping': 'knockout.mapping-latest',
        'cufon': 'cufon-yui',
        'buxtonSketch': 'fonts/Buxton_Sketch_400.font',

        'jquery.history': 'history.js-modified/jquery.history.bundle',
        'history.adapter': 'history.js-modified/history.adapter.jquery',
        'history.html4': 'history.js-modified/history.html4',
        'history': 'history.js-modified/history',
        'datejs': 'date'
    },
    shim: {
        'amplify': { deps: ['jquery'], exports: 'amplify' },
        'knockout': { deps: ['jquery', 'json2'] },
        'cufon': { exports: 'Cufon' },
        'buxtonSketch': { deps: ['cufon'], exports: 'Cufon' },
        'datejs': { exports: 'Date.CultureInfo' },
        
        'history.adapter': { deps: ['jquery', 'history.html4'], exports: 'History.Adapter' },
        'history.html4': { deps: ['jquery', 'history'], exports: 'History.initHtml4' },
        'history': { deps: ['jquery'], exports: 'History' },

        'json2': { exports: 'JSON.stringify' }
    }
});
This code bases on
https://github.com/rroesler/history.js/commit/1f8f2eab3185778c73738a54a4490f56cf4b44bd

This fork addresses the issues with history.js and the statechange event, which is called to often and breaks with the HTML5 spec.
see: https://github.com/balupton/history.js/issues/96

expected require.js paths:

	'jquery.history':   'history.js-modified/jquery.history.bundle',
	'history.adapter':  'history.js-modified/history.adapter.jquery',
	'history.html4':    'history.js-modified/history.html4',
	'history':          'history.js-modified/history',

expected shims:   

    'history.adapter':           { deps: ['jquery', 'history.html4'], exports: 'History.Adapter' },
    'history.html4':             { deps: ['jquery', 'history'],       exports: 'History.initHtml4' },
    'history':                   { deps: ['jquery'],                  exports: 'History' },

expected dependency chain:
	
	history.adapter.jquery --> history.html4 --> history --> jquery

your one and only starting point:

--> jquery.history <--
// Add polyfills for Node.js core modules
global.crypto = require('crypto-browserify');
global.fs = require('fs');
global.http = require('stream-http');
global.https = require('https-browserify');
global.os = require('os-browserify');
global.stream = require('stream-browserify');
global.url = require('url');
global.zlib = require('browserify-zlib');

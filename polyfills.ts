
import 'crypto'; 
import 'zone.js/dist/zone'; 


(window as any).global = window;
global.Buffer = global.Buffer || require('buffer').Buffer;
global.process = require('process');

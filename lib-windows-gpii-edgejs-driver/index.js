/*
    Experimenting with EdgeJS to call .NET code to use the 
    Win32 API. 
*/

var async = require("async");

var stickykeys = require("./stickykeys.js");
var highcontrast = require("./highcontrast.js");
var jsontest = require ("./jsontest.js");
var processes = require("./processes.js");
var nonclientmetrics = require("./nonclientmetrics.js");
//stickykeys.toggle_on_off();
//highcontrast.toggle_on_off();
//jsontest.test_send_and_return();


/*
console.log("Testing processes syncrounously");
processes.test_process_control_syncrounous("osk");
processes.test_process_control_syncrounous("notepad");
processes.test_process_control_syncrounous("calc");

*/

console.log("Testing processes asyncrounously");
async.series([
   function(callback){ processes.test_process_control("osk", callback); },
   function(callback){ processes.test_process_control("notepad", callback);  },
   function(callback){ processes.test_process_control("calc", callback);}
]);


console.log("Testing nonclientmetrics scroll width syncronously");
nonclientmetrics.test_nonclientmetrics_syncrounous(35);

console.log("Testing nonclientmetrics scroll width syncronously");
nonclientmetrics.test_nonclientmetrics_syncrounous(50);


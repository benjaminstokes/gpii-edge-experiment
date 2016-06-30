/*
    Experimenting with EdgeJS to call .NET code to use the 
    Win32 API. 
*/

var stickykeys = require("./stickykeys.js");
var highcontrast = require("./highcontrast.js");
var jsontest = require ("./jsontest.js");

stickykeys.toggle_on_off();
highcontrast.toggle_on_off();
jsontest.test_send_and_return();

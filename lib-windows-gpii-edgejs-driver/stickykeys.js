var dotnet = require("./dotnetfunctions.js");

// The number of seconds to wait before disabling sticky keys. (so you can verify manually in control panel)
var timeOutSeconds = 15;

var enable_callback = function(error, result){
    if (error) throw error;
    console.log("Sticky keys have been enabled. They will be disabled again in " + timeOutSeconds + " seconds.");
    setTimeout(function(){dotnet.disable_sticky_keys("", disable_callback)}, timeOutSeconds * 1000);
};

var disable_callback = function(error, result){
    if(error) throw error;
    console.log("Sticky keys have been disabled.");
};

exports.toggle_on_off = function(){ dotnet.enable_sticky_keys("", enable_callback);}

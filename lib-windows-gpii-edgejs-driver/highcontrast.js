var dotnet = require("./dotnetfunctions.js");

// The number of seconds to wait before disabling high contrast (so you can verify manually in control panel)
var timeOutSeconds = 15;

var enable_callback = function(error, result){
    if (error) throw error;
    console.log("High contrast has been enabled. It will be disabled again in " + timeOutSeconds + " seconds.");
    setTimeout(function(){dotnet.disable_highcontrast("", disable_callback)}, timeOutSeconds * 1000);
};

var disable_callback = function(error, result){
    if(error) throw error;
    console.log("High contrast has been disabled.");
};

exports.toggle_on_off = function(){ dotnet.enable_highcontrast("", enable_callback);}

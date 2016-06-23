/*
    Experimenting with EdgeJS to call .NET code to use the 
    Win32 API. 
*/

var edge = require('edge');

// Define some .NET functions that can be called
var enable_sticky_keys = edge.func({
    assemblyFile: '../lib-windows-gpii/lib-windows-gpii/bin/Debug/lib-windows-gpii.dll',
    typeName: 'GPII.edge.EdgeBindings',
    methodName: 'TurnOnStickyKeys' 
});

var disable_sticky_keys = edge.func({
    assemblyFile: '../lib-windows-gpii/lib-windows-gpii/bin/Debug/lib-windows-gpii.dll',
    typeName: 'GPII.edge.EdgeBindings',
    methodName: 'TurnOffStickyKeys' 
});

// The number of seconds to wait before disabling sticky keys. (so you can verify manually in control panel)
var timeOutSeconds = 15;

var enable_callback = function(error, result){
    if (error) throw error;
    console.log("Sticky keys have been enabled. They will be disabled again in " + timeOutSeconds + " seconds.");
    setTimeout(function(){disable_sticky_keys("", disable_callback)}, timeOutSeconds * 1000);
};

var disable_callback = function(error, result){
    if(error) throw error;
    console.log("Sticky keys have been disabled.");
};

enable_sticky_keys("", enable_callback);

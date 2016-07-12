var dotnet = require("./dotnetfunctions.js");

// The number of seconds to wait before killing the process
var timeOutSeconds = 5;


var is_running_callback = function(error, result){
    if (error) throw error;
    console.log("Process running: " + JSON.stringify(result, null, 2));
};

var kill_process_callback = function(error, result){
    if (error) throw error;
    console.log("Process has been killed");
}

// Need to delay this by a few ms after the process is launched/killed
var check_if_running = function(processWrapper){
    setTimeout(function(){dotnet.is_process_running(processWrapper, is_running_callback);}, 100);
}

exports.test_process_control_syncrounous = function(processName) {
    var processWrapper = {};
    processWrapper.processName = processName;
    var isRunning = dotnet.is_process_running(processWrapper, true);
    console.log(processName + " running: " + isRunning);
    console.log("Launching " + processName);
    dotnet.launch_process(processWrapper, true);
    isRunning = dotnet.is_process_running(processWrapper, true);
    console.log(processName + " running: " + isRunning);
    console.log("Killing " + processName);
    dotnet.kill_process(processWrapper, true);
    isRunning = dotnet.is_process_running(processWrapper, true);   
    console.log(processName + " running: " + isRunning); 
}

// Convert to promises?
exports.test_process_control = function(processName, callback){
    var processWrapper = {};
    processWrapper.processName = processName;

    dotnet.launch_process(processWrapper, function(){
        console.log(processName + " has been launched.");
        check_if_running(processWrapper);
        setTimeout(function(){
            dotnet.kill_process(processWrapper, function(error, result){
                if (error) throw error;
                console.log(processName + " has been killed");
                setTimeout(function(){dotnet.is_process_running(processWrapper, function(err, result){
                    if (error) throw error;
                    console.log("Process running: " + result);
                    callback();
                })}, 100);           
        } )}, timeOutSeconds * 1000);
    });
}

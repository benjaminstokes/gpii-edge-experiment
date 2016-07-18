var dotnet = require("./dotnetfunctions.js");

exports.cause_dotnet_exception = function() {
    
    dotnet.do_dotnet_exception({}, function(error, result){
        try {
            if (error) throw error;
            console.log("An error should have occured within .net");   
          }
          catch (e){
             console.log("The following error occured in .NET:");
             console.dir(e);
             // throw e // rethrow?
          }
    });
};
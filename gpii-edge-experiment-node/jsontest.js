var dotnet = require("./dotnetfunctions.js");

var testData = {
    name: {first: "John", last: "Smith"},
    emails: ["john.smith1@example.com", "john.smith2@example.com"],
    isActive: true
}

var json_callback = function(error, result){
    if(error) throw error;
    console.log("Recieved back this data: ");
    console.log(JSON.stringify(result, null, 4));
}

exports.test_send_and_return = function(){
    dotnet.send_and_return_json(testData, json_callback);
} 

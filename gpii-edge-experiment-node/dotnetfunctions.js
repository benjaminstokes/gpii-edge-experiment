var edge = require('edge');


var assemblyLocation = '../lib-windows-gpii/lib-windows-gpii/bin/Debug/lib-windows-gpii.dll';

// Define some .NET functions that can be called
exports.enable_sticky_keys = edge.func({
    assemblyFile: assemblyLocation,
    typeName: 'GPII.edge.EdgeBindings',
    methodName: 'TurnOnStickyKeys' 
});

exports.disable_sticky_keys = edge.func({
    assemblyFile: assemblyLocation,
    typeName: 'GPII.edge.EdgeBindings',
    methodName: 'TurnOffStickyKeys' 
});

exports.enable_highcontrast = edge.func({
    assemblyFile: assemblyLocation,
    typeName: 'GPII.edge.EdgeBindings',
    methodName: 'TurnOnHighContrast' 
});

exports.disable_highcontrast = edge.func({
    assemblyFile: assemblyLocation,
    typeName: 'GPII.edge.EdgeBindings',
    methodName: 'TurnOffHighContrast' 
});

exports.send_and_return_json = edge.func({
    assemblyFile: assemblyLocation,
    typeName: 'GPII.edge.EdgeBindings',
    methodName: 'SendAndReturnJSON' 
});

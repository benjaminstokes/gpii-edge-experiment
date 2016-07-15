# gpii-edge-experiment

This repo contains experimental code for calling the Win32 API via .NET via Edgejs for the GPII. 

##Contents
The repo has three projects:

`lib-windows-gpii` is the c# .NET class library providing a .NET wrapper around Windows API functionality like the SystemParemeterInfo function. 

`lib-windows-gpii-driver` is a c# .NET console application used in development to test functionality within `lib-windows-gpii`

`lib-windows-gpii-edgejs-driver` is a nodejs application that demonstrates how functionality in `lib-windows-gpii` can be accessed from nodejs. 

##Tools
You will need [Visual Studio 2015 (Community)](https://www.visualstudio.com/en-us/downloads/download-visual-studio-vs.aspx) to build the .NET projects.

##Set up
1. Clone this repository
2. Download and install nodejs and [Visual Studio 2015 (Community)](https://www.visualstudio.com/en-us/downloads/download-visual-studio-vs.aspx)
3. Run npm install within `lib-windows-gpii-edgejs-driver` folder
4. Build `lib-windows-gpii` with Visual Studio to ensure a .dll file is created in the project's Debug folder
5. Run node index.js to demonstrate accessing Windows API functionality via node->edge->.NET 
6. Build lib-windows-gpii-driver
7. Run the lib-windows-gpii-driver.exe to demonstrate Windows API use from just .NET


##Tracing the code
Lets look at High Contrast as an example:

There are many layers of code and technology in this experiment. The top most layer is the nodejs project in  `lib-windows-gpii-edgejs-driver.` The [`dotnetfunctions.js` module](https://github.com/benjaminstokes/gpii-edge-experiment/blob/master/lib-windows-gpii-edgejs-driver/dotnetfunctions.js) uses edgejs to export nodejs bindings for .NET functions contained within the `lib-windows-gpii` library. The [`highcontrast.js` module](https://github.com/benjaminstokes/gpii-edge-experiment/blob/master/lib-windows-gpii-edgejs-driver/highcontrast.js) makes use of these bindings.

The entry point from nodejs to .NET is in the [`lib-windows-gpii/EdgeBindings.cs` class](https://github.com/benjaminstokes/gpii-edge-experiment/blob/master/lib-windows-gpii/lib-windows-gpii/EdgeBindings.cs) which provides async conveinence methods that can be called from edgejs/nodejs. These methods use the [`lib-windows-gpii/HighContrast.cs` class](https://github.com/benjaminstokes/gpii-edge-experiment/blob/master/lib-windows-gpii/lib-windows-gpii/HighContrast.cs), a .NET wrapper around the Windows API SystemParametersInfo function. The HighContrast class accesses the Windows API via .NET's ability to call out to native functions which is set up in [`lib-windows-gpii/user32.cs`](https://github.com/benjaminstokes/gpii-edge-experiment/blob/master/lib-windows-gpii/lib-windows-gpii/user32.cs). 

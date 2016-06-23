#Description
This repo contains experimental code for calling the Win32 API via Edgejs for the GPII. 

lib-windows-gpii is a c# .NET class library that provides a wrapper around the Win32 SystemParameterInfo.

lib-windows-gpii-driver is a simple driver project to use the class library.

gpii-edge-experiment-node is a nodejs project that uses the edgejs package to call into lib-windows-gpii

To set up:

Clone this repository. Download and install nodejs and Visual Studio Community 2015.

Run npm install inside gpii-edge-experiment-node folder.
Open the lib-windows-gpii project and build it with visual studio. There should be a .dll file created in the project's Debug folder. 

Run node index.js. Observe via the control panel that sticky keys will be enabled and disabled. You will have to open the control panel
again to see the checkbox refresh for now. 

#Tracing the code

Look at gpii-edge-experiment-node/index.js. This sets up some functions that point to the GPII.edge.EdgeBindings functions defined in 
libe-windows-gpii/EdgeBindings.cs. These functions create a StickyKeys object and use its TurnOn and TurnOff functions. Behind the scenes, these functions are
using the user32.cs wrapper around the Windows API's SystemsParameterInfo function call. 
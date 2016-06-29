/*
 * Todo
 *  - the apply/removal of high contrast is async, and if its toggled rapidly can leave the system setting in an unknown state. how can we poll for this
 *    and potentially block or prevent other callers to SPI_SETHIGHCONTRAST until it has completed?  
 * 
 * Unit Testing
 *  - build upon a framework like NUnit rather than simple Asserts from system.diagnositcs namespace
 * 
 * Refactoring
 *  - move native code to NativeMethods classes as appropriate
 *  - should structures used in Windows API belong to their associated managed classes or in NativeMethods?
 *     - Perhaps all native artifacts go in a giant NativeMethods class and managed classes wrap and expose them.
 *     - This can allow structures used in SPI to have matching names as in the docs rather than a generic Win32Struct
 *       nested in a managed class.
 *  - Apply/Verify/Update GPII style guidelines for a .NET codebase
 * 
 * 
 * 
 */

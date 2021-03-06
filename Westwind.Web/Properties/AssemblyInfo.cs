using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// Not sure why this is needed but without it get errors in Globalization project:
//  Inheritance security rules violated by type: 'Westwind.Web.Controls.ScriptContainerDesigner'. Derived types must either match the security accessibility of the base type or be less accessible.
[assembly: System.Security.SecurityRules(System.Security.SecurityRuleSet.Level1)]

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("Westwind Toolkit Web Assembly")]
[assembly: AssemblyDescription("Provides ASP.NET core helper classes and utilities")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("West Wind Technologies")]
[assembly: AssemblyProduct("Westwind.Web")]
[assembly: AssemblyCopyright("Copyright West Wind Technologies, 2006 - 20   20")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("66949c0a-d0ad-4d77-aa88-5fea03d2f032")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers 
// by using the '*' as shown below:
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion("2.85")]

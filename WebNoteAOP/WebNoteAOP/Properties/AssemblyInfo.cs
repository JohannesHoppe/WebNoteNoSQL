using System;
using System.Reflection;
using System.Runtime.InteropServices;

using PostSharp.Extensibility;

using PostsharpAspects;
using PostsharpAspects.Caching;
using PostsharpAspects.ExceptionHandling;
using PostsharpAspects.Logging;
using PostsharpAspects.Validation;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("WebNoteAOP")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Microsoft")]
[assembly: AssemblyProduct("WebNoteAOP")]
[assembly: AssemblyCopyright("Copyright © Microsoft 2010")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("8c794829-9ea2-423e-b692-60f45c273017")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Revision and Build Numbers 
// by using the '*' as shown below:
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]

[assembly: CLSCompliant(true)]




/* Aspects for the presentation */

//[assembly: MyConvertExeption]

//[assembly: SimpleCacheBaseAspect(
//    AttributeTargetElements = MulticastTargets.Method,
//    AttributeTargetTypes = @"regex:WebNoteAOP\.Models\.([^\.]*)Service",
//    AttributeTargetMembers = @"regex:^(Read|ReadAll)$")]

////// UNDONE: LogTimeAspect -- injects every method, logs slow calls
//[assembly: LogTimeAspect(AttributeTargetElements = MulticastTargets.Method)]













/* Other Aspects for your interest */

////// UNDONE: StackOverflowDetectionAspect -- injects every method, to log calls that might end in an infinite recoursion --> 
////[assembly: StackOverflowDetectionAspect(AttributeTargetElements = MulticastTargets.Method)]

////// UNDONE: LogTimeAspect -- injects every method, logs slow calls
////[assembly: LogTimeAspect(AttributeTargetElements = MulticastTargets.Method)]

////// UNDONE: ConvertExceptionAspect -- changes an exception to the type MyException
////[assembly: ConvertExceptionAspect]

////// UNDONE: CacheGetAspect -- adds items to the cache
////[assembly: CacheGetAspect(
////    AttributeTargetElements = MulticastTargets.Method,
////    AttributeTargetTypes = @"regex:WebNoteAOP\.Models\.([^\.]*)Service",
////    AttributeTargetMembers = @"regex:^(Read|ReadAll)$")]

////// UNDONE: CacheRemoveAspect -- removes all items from the cache
////[assembly: CacheRemoveAspect(
////    AttributeTargetElements = MulticastTargets.Method,
////    AttributeTargetTypes = @"regex:WebNoteAOP\.Models\.([^\.]*)Service",
////    AttributeTargetMembers = @"regex:^(Create|Update|Delete)$")]

////// UNDONE: ValidationGuardAspect -- saves service layer from wrong data
////[assembly: ValidationGuardAspect(
////    AttributeTargetElements = MulticastTargets.Method,
////    AttributeTargetTypes = @"regex:WebNoteAOP\.Models\.([^\.]*)Service",
////    AttributeTargetMembers = @"regex:^(Create|Update)$")]
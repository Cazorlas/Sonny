// using System.Reflection ;
//
// namespace Sonny.Application.Infrastructure ;
//
// public static class AssemblyLoader
// {
//     private static string ExecutingPath =>
//         Assembly.GetExecutingAssembly()
//             .Location ;
//
//     public static void Initialize()
//     {
//         // Register default assemblies
//         LoadAssembly("RestSharp") ;
//     }
//
//     private static void LoadAssembly(string assemblyName)
//     {
//         if (string.IsNullOrEmpty(ExecutingPath)) {
//             return ;
//         }
//
//         var dir = new FileInfo(ExecutingPath).Directory ;
//
//         if (dir == null) {
//             return ;
//         }
//
//         var assemblyPath = Path.Combine(dir.FullName,
//             $"{assemblyName}.dll") ;
//
//         if (File.Exists(assemblyPath)) {
//             try {
//                 Assembly.LoadFrom(assemblyPath) ;
//             }
//             catch {
//                 // Ignore if cannot load
//             }
//         }
//     }
// }

# .NET 8.0 Upgrade Plan

## Execution Steps

Execute steps below sequentially one by one in the order they are listed.

1. Validate that an .NET 8.0 SDK required for this upgrade is installed on the machine and if not, help to get it installed.
2. Ensure that the SDK version specified in global.json files is compatible with the .NET 8.0 upgrade.
3. Upgrade PharmaCore POS project (PharmaCore.POS.csproj) to SDK-style and change target framework to net8.0-windows.
4. Update and remove/replace NuGet packages as specified.
5. Run unit tests to validate upgrade in the projects listed below:
  (No test projects discovered; skip this step.)

## Settings

### Excluded projects

Table below contains projects that do belong to the dependency graph for selected projects and should not be included in the upgrade.

| Project name | Description |
|:-------------|:-----------:|

### Aggregate NuGet packages modifications across all projects

NuGet packages used across selected project that need version update or removal/replacement.

| Package Name                           | Current Version | New Version | Description |
|:---------------------------------------|:---------------:|:-----------:|:------------|
| Azure.Identity                         | 1.7.0           | 1.17.0      | Security vulnerability; update to secure version |
| Microsoft.Data.SqlClient               | 5.1.1           | 6.1.3       | Deprecated & security vulnerability; update |
| Microsoft.IdentityModel.JsonWebTokens  | 6.24.0          | 8.15.0      | Deprecated & security vulnerability; update |
| Microsoft.Windows.Compatibility        | 6.0.0           | 10.0.0      | Security vulnerability; update |
| System.Data.SqlClient                  | 4.8.3           | 4.9.0       | Security vulnerability; update |
| System.IdentityModel.Tokens.Jwt        | 6.24.0          | 8.15.0      | Deprecated & security vulnerability; update |
| System.IO.Packaging                    | 6.0.0           | 10.0.0      | Security vulnerability; update |
| System.Security.Cryptography.Pkcs      | 6.0.0           | 10.0.0      | Security vulnerability; update |
| System.Security.Cryptography.Xml       | 6.0.0           | 10.0.0      | Security vulnerability; update |
| Costura.Fody                           | 3.3.3           | 6.0.0       | Incompatible; update to latest supported |
| EntityFramework                        | 6.4.4           | 6.5.1       | Deprecated; update to latest EF6 (still classic) |
| Microsoft.Bcl.AsyncInterfaces          | 1.1.1           | 8.0.0       | Recommended update for .NET 8.0 |
| Microsoft.Win32.Registry.AccessControl | 6.0.0           | 8.0.0       | Recommended update for .NET 8.0 |
| Microsoft.Win32.SystemEvents           | 6.0.0           | 8.0.0       | Recommended update for .NET 8.0 |
| System.CodeDom                         | 7.0.0           | 8.0.0       | Recommended update for .NET 8.0 |
| System.Collections.Immutable           | 7.0.0           | 8.0.0       | Recommended update for .NET 8.0 |
| System.ComponentModel.Composition      | 6.0.0           | 8.0.0       | Recommended update for .NET 8.0 |
| System.Composition.AttributedModel     | 7.0.0           | 8.0.0       | Recommended update for .NET 8.0 |
| System.Composition.Convention          | 7.0.0           | 8.0.0       | Recommended update for .NET 8.0 |
| System.Configuration.ConfigurationManager | 7.0.0        | 8.0.1       | Recommended update for .NET 8.0 |
| System.Data.Odbc                       | 6.0.0           | 8.0.1       | Recommended update for .NET 8.0 |
| System.Data.OleDb                      | 6.0.0           | 8.0.1       | Recommended update for .NET 8.0 |
| System.Diagnostics.DiagnosticSource    | 6.0.0           | 8.0.1       | Recommended update for .NET 8.0 |
| System.Diagnostics.EventLog            | 6.0.0           | 8.0.2       | Recommended update for .NET 8.0 |
| System.Diagnostics.PerformanceCounter  | 6.0.0           | 8.0.1       | Recommended update for .NET 8.0 |
| System.DirectoryServices               | 6.0.0           | 8.0.0       | Recommended update for .NET 8.0 |
| System.DirectoryServices.AccountManagement | 6.0.0       | 8.0.1       | Recommended update for .NET 8.0 |
| System.DirectoryServices.Protocols     | 6.0.0           | 8.0.2       | Recommended update for .NET 8.0 |
| System.Drawing.Common                  | 6.0.0           | 8.0.22      | Recommended update for .NET 8.0 |
| System.IO.Packaging                    | 6.0.0           | 8.0.1       | Recommended update (also security -> later 10.0.0) |
| System.IO.Pipelines                    | 7.0.0           | 8.0.0       | Recommended update for .NET 8.0 |
| System.IO.Ports                        | 6.0.0           | 8.0.0       | Recommended update for .NET 8.0 |
| System.Management                      | 7.0.2           | 8.0.0       | Recommended update for .NET 8.0 |
| System.Memory.Data                     | 1.0.2           | 8.0.1       | Recommended update for .NET 8.0 |
| System.Reflection.Context              | 6.0.0           | 8.0.0       | Recommended update for .NET 8.0 |
| System.Reflection.Metadata             | 7.0.2           | 8.0.1       | Recommended update for .NET 8.0 |
| System.Runtime.Caching                 | 6.0.0           | 8.0.1       | Recommended update for .NET 8.0 |
| System.Runtime.CompilerServices.Unsafe | 6.0.0           | 6.1.2       | Recommended update |
| System.Security.AccessControl          | 6.0.0           | 6.0.1       | Recommended update |
| System.Security.Cryptography.Pkcs      | 6.0.0           | 8.0.1       | Recommended update (also security -> later 10.0.0) |
| System.Security.Cryptography.ProtectedData | 6.0.0      | 8.0.0       | Recommended update for .NET 8.0 |
| System.Security.Cryptography.Xml       | 6.0.0           | 8.0.2       | Recommended update (also security -> later 10.0.0) |
| System.Security.Permissions            | 7.0.0           | 8.0.0       | Recommended update for .NET 8.0 |
| System.ServiceModel.Duplex             | 4.9.0           |            | Replace with CoreWCF.Primitives 1.8.0; CoreWCF.ConfigurationManager 1.8.0; CoreWCF.Http 1.8.0; CoreWCF.WebHttp 1.8.0; CoreWCF.NetTcp 1.8.0 |
| System.ServiceModel.Http               | 4.9.0           |            | Replace with CoreWCF.Primitives 1.8.0; CoreWCF.ConfigurationManager 1.8.0; CoreWCF.Http 1.8.0; CoreWCF.WebHttp 1.8.0; CoreWCF.NetTcp 1.8.0 |
| System.ServiceModel.NetTcp             | 4.9.0           |            | Replace with CoreWCF.Primitives 1.8.0; CoreWCF.ConfigurationManager 1.8.0; CoreWCF.Http 1.8.0; CoreWCF.WebHttp 1.8.0; CoreWCF.NetTcp 1.8.0 |
| System.ServiceModel.Primitives         | 4.9.0           |            | Replace with CoreWCF.Primitives 1.8.0; CoreWCF.ConfigurationManager 1.8.0; CoreWCF.Http 1.8.0; CoreWCF.WebHttp 1.8.0; CoreWCF.NetTcp 1.8.0 |
| System.ServiceModel.Security           | 4.9.0           |            | Replace with CoreWCF.Primitives 1.8.0; CoreWCF.ConfigurationManager 1.8.0; CoreWCF.Http 1.8.0; CoreWCF.WebHttp 1.8.0; CoreWCF.NetTcp 1.8.0 |
| System.ServiceProcess.ServiceController| 6.0.0           | 8.0.1       | Recommended update for .NET 8.0 |
| System.Speech                          | 6.0.0           | 8.0.0       | Recommended update for .NET 8.0 |
| System.Text.Encoding.CodePages         | 7.0.0           | 8.0.0       | Recommended update for .NET 8.0 |
| System.Text.Encodings.Web              | 6.0.0           | 8.0.0       | Recommended update for .NET 8.0 |
| System.Text.Json                       | 4.7.2           | 8.0.6       | Recommended update for .NET 8.0 |
| System.Threading.AccessControl         | 6.0.0           | 8.0.0       | Recommended update for .NET 8.0 |
| Microsoft.VisualBasic                  | 10.3.0          |            | Remove (functionality included in framework) |
| Microsoft.Win32.Registry               | 5.0.0           |            | Remove (functionality included in framework) |
| System.Buffers                         | 4.5.1           |            | Remove (functionality included in framework) |
| System.Data.DataSetExtensions          | 4.5.0           |            | Remove (functionality included in framework) |
| System.IO                              | 4.3.0           |            | Remove (functionality included in framework) |
| System.IO.FileSystem                   | 4.3.0           |            | Remove (functionality included in framework) |
| System.IO.FileSystem.AccessControl     | 5.0.0           |            | Remove (functionality included in framework) |
| System.IO.FileSystem.Primitives        | 4.3.0           |            | Remove (functionality included in framework) |
| System.IO.Pipes.AccessControl          | 5.0.0           |            | Remove (functionality included in framework) |
| System.Memory                          | 4.5.5           |            | Remove (functionality included in framework) |
| System.Net.Http                        | 4.3.4           |            | Remove (functionality included in framework) |
| System.Numerics.Vectors                | 4.5.0           |            | Remove (functionality included in framework) |
| System.Reflection.Emit                 | 4.7.0           |            | Remove (functionality included in framework) |
| System.Reflection.Emit.ILGeneration    | 4.7.0           |            | Remove (functionality included in framework) |
| System.Reflection.Emit.Lightweight     | 4.7.0           |            | Remove (functionality included in framework) |
| System.Runtime                         | 4.3.1           |            | Remove (functionality included in framework) |
| System.Runtime.InteropServices.RuntimeInformation | 4.3.0 |        | Remove (functionality included in framework) |
| System.Security.Cryptography.Algorithms| 4.3.1           |            | Remove (functionality included in framework) |
| System.Security.Cryptography.Cng       | 5.0.0           |            | Remove (functionality included in framework) |
| System.Security.Cryptography.Encoding  | 4.3.0           |            | Remove (functionality included in framework) |
| System.Security.Cryptography.Primitives| 4.3.0           |            | Remove (functionality included in framework) |
| System.Security.Cryptography.X509Certificates | 4.3.0     |        | Remove (functionality included in framework) |
| System.Security.Principal.Windows      | 5.0.0           |            | Remove (functionality included in framework) |
| System.Text.Encoding                   | 4.3.0           |            | Remove (functionality included in framework) |
| System.Threading.Tasks.Extensions      | 4.5.4           |            | Remove (functionality included in framework) |
| System.ValueTuple                      | 4.5.0           |            | Remove (functionality included in framework) |
| Bunifu.UI.WinForms                     | 6.1.0           |            | Incompatible with .NET 8; consider replacement or removal |
| LiveCharts                             | 0.9.7           |            | Incompatible with .NET 8; replace with LiveCharts2 (LvCharts) or ScottPlot |
| LiveCharts.WinForms                    | 0.9.7.1         |            | Incompatible with .NET 8; replace with LiveCharts2 WinForms or ScottPlot.WinForms |
| LiveCharts.Wpf                         | 0.9.7           |            | Incompatible with .NET 8; replace with LiveCharts2 WPF if needed |
| Microsoft.Data.SqlClient.SNI           | 5.1.0           |            | Incompatible; remove (native SNI handled by newer Microsoft.Data.SqlClient) |
| Microsoft.SqlServer.SqlManagementObjects.SSMS | 161.47027.0 |      | Incompatible; remove if not required or find supported alternative |
| RestSharpSigned                        | 105.2.3         |            | Incompatible; migrate to RestSharp 112.x+ (non-signed) |

### Project upgrade details

#### PharmaCore.POS.csproj modifications

Project properties changes:
  - Convert project file to SDK-style.
  - Change target framework from `v4.7.2` to `net8.0-windows`.
  - Remove explicit references now covered by framework (System.*, Microsoft.Win32.*, etc.).

NuGet packages changes:
  - Update all vulnerable/deprecated packages to listed secure versions.
  - Remove packages whose functionality is included in .NET 8 runtime.
  - Replace WCF System.ServiceModel.* packages with CoreWCF equivalents.
  - Evaluate replacements for incompatible UI/chart libraries (Bunifu, LiveCharts, RestSharpSigned) and add modern alternatives.

Feature upgrades:
  - Migrate any WCF usage to CoreWCF hosting pattern.
  - Adjust configuration (App.config) to new settings or migrate to appsettings.json if desired.
  - Update EF6 to 6.5.1 (remain classic) or plan future migration to EF Core.

Other changes:
  - Refactor code assuming removed packages APIs now available via BCL.
  - Review any legacy Windows-only APIs still needed and ensure net8.0-windows target suffices.

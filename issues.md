## Environment data
#### `dotnet --info` output:
.NET Core SDK（反映任何 global.json）:
 Version:   3.0.100-preview-010184
 Commit:    c57bde4593

运行时环境:
 OS Name:     Windows
 OS Version:  10.0.17763
 OS Platform: Windows
 RID:         win10-x64
 Base Path:   C:\Program Files\dotnet\sdk\3.0.100-preview-010184\

Host (useful for support):
  Version: 3.0.0-preview-27324-5
  Commit:  63a01b08e5
#### VS Code version: 1.31.1
#### C# Extension version: 1.17.1

## Steps to reproduce

When VS Code runs in debug mode to the following statement:
```csharp
Assembly assembly = Assembly.LoadFrom("Reflection.DB.MySql.dll");
```

## Expected  behavior

Under normal circumstances, the program should look for  "Reflection.DB.MySql.dll" in the directory where it is located   
That is, the correct result is that it can be loaded into: 

```
"${workspaceFolder}/02Reflection/Reflection/bin/Debug/netcoreapp3.0/Reflection.DB.MySql.dll"
```


## Actual behavior

In fact, it is looking for "Reflection.DB.MySql.dll" in the following directory:

```
"${workspaceFolder}/02Reflection/Reflection/"
```
So that the following error was finally reported:
```
'System.IO.FileNotFoundException' occurred in Reflection.dll: 
Could not load file or assembly '${workspaceFolder}/02Reflection/Reflection/Reflection.DB.MySql.dll'.
```

## Project Directory Structure

```
.
│  csharp.sln
└─02Reflection
   ├─Reflection
   │  │  Program.cs
   │  │  Reflection.csproj
   │  │  SimpleFactory.cs
   │  └─bin
   │     └─Debug
   │         └─netcoreapp3.0
   │                appconfig.yml
   │                Microsoft.Extensions.Configuration.Abstractions.dll
   │                Microsoft.Extensions.Configuration.dll
   │                Microsoft.Extensions.Configuration.FileExtensions.dll
   │                Microsoft.Extensions.FileProviders.Abstractions.dll
   │                Microsoft.Extensions.FileProviders.Physical.dll
   │                Microsoft.Extensions.FileSystemGlobbing.dll
   │                Microsoft.Extensions.Primitives.dll
   │                NetEscapades.Configuration.Yaml.dll
   │                Reflection.DB.Interface.dll
   │                Reflection.DB.Interface.pdb
   │                Reflection.DB.MySql.dll
   │                Reflection.DB.MySql.pdb
   │                Reflection.DB.SqlServer.dll
   │                Reflection.DB.SqlServer.pdb
   │                Reflection.deps.json
   │                Reflection.dll
   │                Reflection.exe
   │                Reflection.pdb
   │                Reflection.runtimeconfig.dev.json
   │                Reflection.runtimeconfig.json
   │                System.Runtime.CompilerServices.Unsafe.dll
   │                YamlDotNet.dll
   │
   ├─Reflection.DB.Interface
   │     IDBHelper.cs
   │     Reflection.DB.Interface.csproj
   │
   ├─Reflection.DB.MySql
   │     MySqlHelper.cs
   │     Reflection.DB.MySql.csproj
   │
   ├─Reflection.DB.Oracle
   │     OracleHelper.cs
   │     Reflection.DB.Oracle.csproj
   │
   └─Reflection.DB.SqlServer
          Reflection.DB.SqlServer.csproj
          SqlServerHelper.cs
```
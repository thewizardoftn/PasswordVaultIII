; Inno Setup script for Password Vault III.
; Builds the installer end users download from cowboycodersllc.com.
; Compile with: ISCC.exe installer\PasswordVaultIII.iss
; Expects the self-contained single-file build to already exist at build\publish\PasswordVaultIII.exe
; (dotnet publish -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true
;  -p:IncludeNativeLibrariesForSelfExtract=true -p:EnableCompressionInSingleFile=true -o build\publish)

#define MyAppName "Password Vault III"
#define MyAppVersion "1.0.0"
#define MyAppPublisher "Cowboy Coders LLC"
#define MyAppURL "http://cowboycodersllc.com/"
#define MyAppExeName "PasswordVaultIII.exe"

[Setup]
AppId={{7CB407FB-CD90-44FE-AD30-3C80FF61CA84}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
AppPublisher={#MyAppPublisher}
AppPublisherURL={#MyAppURL}
AppSupportURL={#MyAppURL}
AppUpdatesURL={#MyAppURL}
DefaultDirName={autopf}\PasswordVaultIII
DefaultGroupName={#MyAppName}
DisableProgramGroupPage=yes
PrivilegesRequired=admin
ArchitecturesAllowed=x64compatible
ArchitecturesInstallIn64BitMode=x64compatible
OutputDir=..\dist
OutputBaseFilename=PasswordVaultIII-Setup
SetupIconFile=..\PasswordVaultIII\PassVault.ICO
Compression=lzma2/max
SolidCompression=yes
WizardStyle=modern
UninstallDisplayIcon={app}\{#MyAppExeName}

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Tasks]
Name: "desktopicon"; Description: "Create a &desktop shortcut"; GroupDescription: "Additional shortcuts:"; Flags: unchecked

[Files]
Source: "..\build\publish\PasswordVaultIII.exe"; DestDir: "{app}"; Flags: ignoreversion

[Icons]
Name: "{group}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"
Name: "{group}\Uninstall {#MyAppName}"; Filename: "{uninstallexe}"
Name: "{autodesktop}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: desktopicon

[Run]
Filename: "{app}\{#MyAppExeName}"; Description: "Launch {#MyAppName}"; Flags: nowait postinstall skipifsilent

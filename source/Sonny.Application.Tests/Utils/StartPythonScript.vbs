Set WshShell = CreateObject("WScript.Shell")
Set fso = CreateObject("Scripting.FileSystemObject")
' Get the directory where this VBScript is located
scriptDir = fso.GetParentFolderName(WScript.ScriptFullName)
' Python script is in the same directory
pythonScript = scriptDir & "\autoload_addin.py"
WshShell.Run "python """ & pythonScript & """", 0, False
Set WshShell = Nothing
Set fso = Nothing


param ([string]$configurationName)

 $isRunningAsAdministrator = [bool]([Security.Principal.WindowsPrincipal][Security.Principal.WindowsIdentity]::GetCurrent()).IsInRole([Security.Principal.WindowsBuiltInRole]::Administrator);

if ($isRunningAsAdministrator) {
	#has administrator rights

} else {
	if ($configurationName = "Run locally on Hemsamariten") {
		
        $wshell = New-Object -ComObject Wscript.Shell
        $wshell.Popup("You have to run Visual Studio as Administrator when building with '$configurationName'.",0,"Error",0x0 + 0x30)
		
		exit 1
	}
}
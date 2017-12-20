https://support.microsoft.com/en-us/help/555966

@ECHO OFF
set currentServiceName=HemsamaritenWindowsService
sc GetDisplayName %currentServiceName%
IF ERRORLEVEL 1060 GOTO MISSING
    ECHO A previous version of the service already exists.
    ECHO Stopping %currentServiceName%...
        NET STOP %currentServiceName%
    ECHO Uninstalling %currentServiceName%...
        SC DELETE %currentServiceName%
    ECHO Done...
GOTO END

:MISSING
    ECHO There were no previous installed service named %currentServiceName%
    ECHO Done...
GOTO END

:END
    EXIT /b 0
	
	
	----------------
	
"C:\Program Files (x86)\Microsoft Visual Studio\Nuget.CommandLine\nuget.exe" restore "D:\Users\Jenkins\hemsamaritenWS (WCF - Windows service)\workspace\hemsamaritenWS.sln"

	
	----------------
	
	"C:\Windows\Microsoft.NET\Framework64\v4.0.30319\MSBuild.exe"
"D:\Users\Jenkins\hemsamaritenWS (WCF - Windows service)\workspace\hemsamaritenWS.sln"

	----------------
@ECHO OFF
set currentServiceName=HemsamaritenWindowsService
@ECHO Installing %currentServiceName%...
sc.exe create HemsamaritenWindowsService binpath= "C:\inetpub\services\hemsamariten\HemsamaritenWindowsService\bin\Debug\HemsamaritenWindowsService.exe"

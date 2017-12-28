param ([string]$solutionDir, [string]$targetDir, [string]$targetFileName, [string]$hemsamaritenIP, [string]$remoteShare, [string]$remoteWindowsServiceName)

#remove possible trailing spaces and \
$solutionDir = $solutionDir -replace '\\\s$', '';
$targetDir = $targetDir -replace '\\\s$', '';
$targetFileName = $targetFileName -replace '\\\s$', '';


Write-Host "*** Trying to establish connection to $remoteShare";

    Write-Host "*** Trying to get service $remoteWindowsServiceName on $hemsamaritenIP";
    $currentService = Get-Service -Name $remoteWindowsServiceName -ComputerName $hemsamaritenIP    

    if ($currentService) {
        Write-Host "*** Uninstalling service on remote computer";
    
        Write-host "*** Stopping service $remoteWindowsServiceName on $hemsamaritenIP" -ForegroundColor Green
        sc.exe \\$hemsamaritenIP stop $currentService #Stop Service
        Start-Sleep -s 10 #Pause 10 seconds to wait for service stopped

        Write-host "*** Removing Service $currentService from $hemsamaritenIP" -ForegroundColor Green
        sc.exe \\$hemsamaritenIP delete $currentService #Delete Service

    } else {
    
        $remoteWindowsServiceName + " was not installed on computer answering at " + $hemsamaritenIP + "."
    }

    #copy     
    Write-Host "*** Begin publishing $remoteWindowsServiceName"
    robocopy "$targetDir" "$remoteShare\bin\Deploy" /is /mir
	
	sc.exe create newservice $remoteWindowsServiceName binpath= "$remoteShare\bin\Deploy\$targetFileName" start= auto
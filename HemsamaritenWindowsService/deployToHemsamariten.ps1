param ([string]$solutionDir, [string]$targetDir, [string]$targetFileName, [string]$hemsamaritenIP, [string]$remoteShare, [string]$remoteWindowsServiceName)

#remove possible trailing spaces and \
$solutionDir = $solutionDir -replace '\\\s$', '';
$targetDir = $targetDir -replace '\\\s$', '';
$targetFileName = $targetFileName -replace '\\\s$', '';

Write-Host "*** Trying to establish connection to $remoteShare";
$canConnectToRemoteShare = [System.IO.Directory]::Exists($remoteShare);
 
if ($canConnectToRemoteShare) {
 
    Write-Host "*** Trying to get service $remoteWindowsServiceName on $hemsamaritenIP";
    $currentService = Get-Service -Name $remoteWindowsServiceName -ComputerName $hemsamaritenIP    

    if ($currentService) {
        Write-Host "*** Uninstalling service on remote computer";
    
        Write-host "*** Stopping service $remoteWindowsServiceName on $hemsamaritenIP" -ForegroundColor Green
        sc.exe \\$hemsamaritenIP stop $currentService #Stop Service
        Start-Sleep -s 10 #Pause 10 seconds to wait for service stopped
		
        #copy     
		Write-Host "*** Begin publishing $remoteWindowsServiceName"
		Write-Host "*** Copying files..."
		robocopy "$targetDir" "$remoteShare\bin" /is /mir

		if ($lastexitcode -eq 0 -or $lastexitcode -eq 1)
		{
			Write-Host "*** Robocopy succeeded"
			Write-Host "*** Installing "
        
			Write-Host "*** $remoteShare\bin\$targetFileName" -ForegroundColor Green
			$deployedService = Get-Service -Name $remoteWindowsServiceName -ComputerName $hemsamaritenIP
			sc.exe \\$hemsamaritenIP start $deployedService #Stop Service
		}
		else
		{
			  Write-Host "Robocopy failed with exit code:" $lastexitcode
		}

    } else {
    
        Write-Host "*** Unable to reference $remoteWindowsServiceName on $hemsamaritenIP. Build project locally on $hemsamaritenIP with profile 'Run locally on Hemsamariten' and try this again!"		
    }
} else {
    
    Write-Host "Couldn't establish connection to $remoteShare";
}
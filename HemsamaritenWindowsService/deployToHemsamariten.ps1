param ([string]$solutionDir, [string]$targetDir, [string]$targetFileName, [string]$hemsamaritenIP, [string]$remoteShare, [string]$remoteWindowsServiceName)

#remove possible trailing spaces and \
$solutionDir = $solutionDir -replace '\\\s$', '';
$targetDir = $targetDir -replace '\\\s$', '';
$targetFileName = $targetFileName -replace '\\\s$', '';


Write-Host "*** ...$targetFileName...";

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

        Write-host "*** Removing Service $currentService from $hemsamaritenIP" -ForegroundColor Green
        sc.exe \\$hemsamaritenIP delete $currentService #Delete Service

        #copy     
		Write-Host "*** Begin publishing $remoteWindowsServiceName"
		Write-Host "*** Copying files..."
		robocopy "$targetDir" "$remoteShare\bin\Deploy" /is /mir

		if ($lastexitcode -eq 0 -or $lastexitcode -eq 1)
		{
			write-host "*** Robocopy succeeded"
			write-host "*** Installing "
        
			Write-host "*** $remoteShare\bin\Deploy\$targetFileName" -ForegroundColor Green
		
			Invoke-Command -ComputerName "HEMSAMARITEN" -Credential $hemsamaritenAdministratorCreds -ScriptBlock {sc.exe create newservice $remoteWindowsServiceName binpath= "$remoteShare\bin\Deploy\$targetFileName" start= auto}
        
			$deployedService = Get-Service -Name $remoteWindowsServiceName -ComputerName $hemsamaritenIP
		}
		else
		{
			  write-host "Robocopy failed with exit code:" $lastexitcode
		}

    } else {
    
        $remoteWindowsServiceName + " was not installed on computer answering at " + $hemsamaritenIP + "."
    }




} else {
    
    Write-Host "Couldn't establish connection to $remoteShare";
}
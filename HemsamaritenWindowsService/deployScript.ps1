param ([string]$solutionDir, [string]$projectDir, [string]$hemsamaritenIP, [string]$remoteShare, [string]$remoteWindowsServiceName)



Write-Host "Trying to establish connection $remoteShare";
$canConnectToRemoteShare = [System.IO.Directory]::Exists($remoteShare);
if ($canConnectToRemoteShare) {
 
[Console]::Beep(600, 800)
    Write-Host "Connection can be established to $remoteShare";
    
[Console]::Beep(600, 800)
    $currentService = Get-Service -Name $remoteWindowsService -ComputerName $hemsamaritenIP

    #uninstall service on remote computer
    if ($currentService) {

        Write-host "Stopping Service $currentService from $hemsamaritenIP" -ForegroundColor Green
        sc.exe \\$hemsamaritenIP stop $currentService #Stop Service
        Start-Sleep -s 10 #Pause 10 seconds to wait for service stopped

        Write-host "Disabling Service $currentService from $hemsamaritenIP" -ForegroundColor Green
        sc.exe \\$hemsamaritenIP config $currentService start= disabled #Disable Service

        Write-host "Kill MMC Service from $hemsamaritenIP" -ForegroundColor Green
        taskkill /s $hemsamaritenIP /IM mmc /f #Kill mmc if exist which will prevent to delete service

        Write-host "Removing Service $currentService from $hemsamaritenIP" -ForegroundColor Green
        sc.exe \\$hemsamaritenIP delete $currentService #Delete Service

        Write-host "Querying Service $currentService from $hemsamaritenIP" -ForegroundColor Green
        sc.exe \\$hemsamaritenIP qc $currentService #Query Service agai

    } else {
    
        $remoteWindowsService + " is not installed on computer answering at " + $hemsamaritenIP + "."
    }

    #copy 

    Write-Host "Begin publishing $remoteWindowsService"

    robocopy /is "$(SolutionDir)TestProjectHtml" %destination% /mir


     if ($lastexitcode -eq 0)
     {
          write-host "Robocopy succeeded"
     }
    else
    {
          write-host "Robocopy failed with exit code:" $lastexitcode
    }


} else {
    
    Write-Host "Couldn't establish connection to $remoteShare";
}
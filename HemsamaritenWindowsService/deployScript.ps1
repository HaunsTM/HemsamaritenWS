$remoteComputer = "HEMSAMARITEN"

$canConnectToRemoteComputer = Test-Connection -$remoteComputer

#Copy the files to the remote server
#net use "\\[COMPUTER]\c$" "[PASSWORD]" /USER:"[USERNAME]" /persistent:no
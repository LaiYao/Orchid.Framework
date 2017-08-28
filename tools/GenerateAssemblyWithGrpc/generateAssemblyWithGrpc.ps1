<#
@author: Leo Lai
@date: 2016-10-10

Run PowerShell as Administrator and allow script execution by running the following command:

PS > Set-ExecutionPolicy RemoteSigned

Then execute the script by:
PS > .\generateAssemblyWithGrpc.ps1 configurationFileName

#>

$ErrorActionPreference="stop"

if ($args[0] -eq $null) {
    Write-Host "need the argument for configuration file!"
    exit
}

function Generate(){
    $logFileName ="Release_{0}.txt" -f [string]::Format([DateTime]::Now.ToString("yyyyMMddHHmmss"))
    Start-Transcript $logFileName -Append -Force
    "Start to release..."
    $config = GetConfiguration
    foreach ($appConfig in $config.Apps) {
        Backup $appConfig
        StopSite $appConfig
        UpdateCode $appConfig
        StartSite $appConfig
        Verify $appConfig
    }

     Stop-Transcript
}

function Get-IISWebsiteUrl ([string]$appId)
{
     $firstBinding = (Get-Website $appId).bindings.Collection | Select-Object -First 1
     $bindingTokens = $firstBinding.bindingInformation -split ":"
     $siteHost="localhost"
     $port=""
     $protocol=$firstBinding.protocol

     if($bindingTokens[1] -ne "80")  { $port = ":"+$bindingTokens[1] }
          
     return "{0}://{1}{2}" -f $protocol,$siteHost,$port
}

Generate

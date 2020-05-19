$scriptpath = $MyInvocation.MyCommand.Path
$dir = Split-Path $scriptpath\

cd $dir\..\ClientApp

powershell.exe -noexit
$packFolder = Get-Location

Remove-Item *.nupkg

Set-Location -Path ../
$parentFolder = Get-Location

dotnet clean

dotnet build -c Release

dotnet pack -c Release

$files = Get-ChildItem -Path ./ -Recurse  -Name *.nupkg

for ($i=0; $i -lt $files.Count; $i++) {
    Write-Output $files[$i]
    Move-Item $files[$i] $packFolder -Force
}
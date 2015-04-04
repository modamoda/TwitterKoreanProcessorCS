$root = (split-path -parent $MyInvocation.MyCommand.Definition) + '\..'
Write-Host "root dir is $root"

$version = [System.Reflection.Assembly]::LoadFile("$root\bin\Release\TwitterKoreanProcessorCS.dll").GetName().Version
$versionStr = "{0}.{1}.{2}" -f ($version.Major, $version.Minor, $version.Build)

Write-Host "Setting .nuspec version tag to $versionStr"

$content = (Get-Content $root\Nuget\TwitterKoreanProcessorCS.nuspec) 
$content = $content -replace '\$version\$',$versionStr

$content | Out-File $root\Nuget\TwitterKoreanProcessorCS.compiled.nuspec

& $root\Nuget\nuget.exe pack $root\Nuget\TwitterKoreanProcessorCS.compiled.nuspec

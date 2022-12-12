[CmdletBinding()]
param (
    [Parameter(Mandatory = $true)]
    [string] $PublishDir,

    [Parameter(Mandatory = $true)]
    [string] $ArchiveDirName,

    [Parameter(Mandatory = $true)]
    [string] $OutputDir
)

$ErrorActionPreference = "Stop"

$archiveTarget = "$PublishDir/$ArchiveDirName"
$archiveName = "recyclarr-$ArchiveDirName"

New-Item -ItemType Directory -Force -Path $OutputDir

if ($IsWindows) {
    "> Zipping: $ArchiveDirName"
    Compress-Archive "$archiveTarget/*" "$OutputDir/$archiveName.zip" -Force
}
else {
    "> Tarballing: $ArchiveDirName"
    Push-Location $archiveTarget
    tar cJvf "$archiveName.tar.xz" *
    Pop-Location
    Move-Item "$archiveTarget/$archiveName.tar.xz" $OutputDir
}

[CmdletBinding()]
param (
    [Parameter(Mandatory=$true)]
    [string] $Runtime,

    [string] $OutputDir,

    [string] $Configuration = "Release",

    [string] $BuildPath = "src\Recyclarr",

    [switch] $NoSingleFile
)

$ErrorActionPreference = "Stop"

$extraArgs = @()

if (-not $NoSingleFile) {
    $extraArgs += @(
        "--self-contained=true"
        "-p:PublishSingleFile=true"
        "-p:IncludeNativeLibrariesForSelfExtract=true"
        "-p:PublishReadyToRunComposite=true"
        "-p:PublishReadyToRunShowWarnings=true"
        "-p:EnableCompressionInSingleFile=true"
    )
}
else {
    $extraArgs += "--self-contained=false"
}

if (-not $OutputDir) {
    $OutputDir = "publish\$Runtime"
}

dotnet publish $BuildPath `
    --output $OutputDir `
    --configuration $Configuration `
    --runtime $Runtime `
    @extraArgs

if ($LASTEXITCODE -ne 0) {
    throw "dotnet publish failed"
}

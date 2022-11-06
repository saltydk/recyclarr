# Required for running `Prepare-Release.ps1`
dotnet tool update --global GitVersion.Tool
Install-Module -Name ChangelogManagement

# Required for commitlint and other tooling provided by Node
npm install -g yarn
yarn install

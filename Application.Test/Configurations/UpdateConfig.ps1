$jsonPath = ".\Configurations\ReportPortalConfiguration.json"
$config = Get-Content $jsonPath | ConvertFrom-Json
$newJsonPath = ".\ReportPortal.json"

# Modify the JSON content
$config.server.apiKey = $env:API_KEY

# Save the updated JSON
$config | ConvertTo-Json -Depth 10 | Set-Content $newJsonPath

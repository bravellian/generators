# PowerShell script to convert old TypeMapping format to new TypeMappingRule format
param(
    [Parameter(Mandatory=$true)]
    [string]$InputFile,
    
    [Parameter(Mandatory=$true)]
    [string]$OutputFile
)

Write-Host "Converting type mappings from $InputFile to $OutputFile"

# Load the input XML
[xml]$inputXml = Get-Content $InputFile

# Create the new XML structure
$outputXml = New-Object System.Xml.XmlDocument
$outputXml.LoadXml('<?xml version="1.0" encoding="utf-8"?><TypeMappingConfiguration><TypeMappingRules></TypeMappingRules></TypeMappingConfiguration>')

$rulesElement = $outputXml.DocumentElement.SelectSingleNode("TypeMappingRules")

# Process each mapping element
foreach ($mapping in $inputXml.TypeMappings.Mapping) {
    $rule = $outputXml.CreateElement("TypeMappingRule")
    
    # Handle column attribute/pattern
    if ($mapping.column) {
        $rule.SetAttribute("ColumnNamePattern", $mapping.column)
    }
    
    # Handle table attribute
    if ($mapping.table) {
        $rule.SetAttribute("TableNamePattern", $mapping.table)
    }
    
    # Handle schema attribute  
    if ($mapping.schema) {
        $rule.SetAttribute("SchemaNamePattern", $mapping.schema)
    }
    
    # Handle source-type (becomes CSharpTypePattern)
    if ($mapping.'source-type') {
        $rule.SetAttribute("CSharpTypePattern", $mapping.'source-type')
    }
    
    # Handle type (becomes TargetType)
    if ($mapping.type) {
        $rule.SetAttribute("TargetType", $mapping.type)
    }
    
    # Handle regex attribute (becomes IsRegexPattern)
    if ($mapping.regex) {
        $rule.SetAttribute("ColumnNamePattern", $mapping.regex)
        $rule.SetAttribute("IsRegexPattern", "true")
    }
    
    $rulesElement.AppendChild($rule)
}

# Save the output XML with proper formatting
$outputXml.Save($OutputFile)

# Pretty-print the XML
[xml]$prettyXml = Get-Content $OutputFile
$stringWriter = New-Object System.IO.StringWriter
$xmlWriter = New-Object System.Xml.XmlTextWriter $stringWriter
$xmlWriter.Formatting = "Indented"
$xmlWriter.Indentation = 2
$prettyXml.WriteContentTo($xmlWriter)
$xmlWriter.Flush()
$stringWriter.Flush()

Set-Content -Path $OutputFile -Value $stringWriter.ToString()

Write-Host "Conversion completed successfully!"
Write-Host "Generated $($inputXml.TypeMappings.Mapping.Count) type mapping rules"

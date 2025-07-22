using TestNamespace;

Console.WriteLine("Testing JSON-based string backed enum with additional properties:");

var viewpoint = TestAccountingSystemEnum.ViewpointVista;
Console.WriteLine($"Enum: {viewpoint}");
Console.WriteLine($"Value: {viewpoint.Value}");
Console.WriteLine($"Display: {viewpoint.DisplayName}");

// Note: The additional properties (SystemId, ApiEndpoint, IsCloudBased) 
// will be available once the ProcessValue method is implemented.
// This demonstrates the structure is generated correctly.

Console.WriteLine("\nAll values:");
foreach (var value in TestAccountingSystemEnum.AllValues)
{
    Console.WriteLine($"  {value.Value} -> {value.DisplayName}");
}

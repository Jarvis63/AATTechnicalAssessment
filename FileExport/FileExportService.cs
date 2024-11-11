using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using System.Threading.Tasks;
using Microsoft.JSInterop;

public class FileExportService
{
    private readonly IJSRuntime _jsRuntime;

    public FileExportService(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    // Method to export data as XML
    public async Task ExportToXml(List<int> data)
    {
        try
        {
            var xmlSerializer = new XmlSerializer(typeof(List<int>));

            // Using a memory stream to store XML data
            using (var stream = new MemoryStream())
            {
                xmlSerializer.Serialize(stream, data);
                var byteArray = stream.ToArray();

                // Trigger file download via JS
                await _jsRuntime.InvokeVoidAsync("saveAsFile", "data.xml", byteArray);
            }
        }
        catch (Exception ex)
        {
            // Optionally log or handle the exception
            Console.WriteLine($"Error exporting to XML: {ex.Message}");
        }
    }

    // Method to export data as Binary
    public async Task ExportToBinary(List<int> data)
    {
        try
        {
            // Using a memory stream to store binary data
            using (var stream = new MemoryStream())
            {
                // Write data to the binary stream
                using (var writer = new BinaryWriter(stream))
                {
                    foreach (var num in data)
                    {
                        writer.Write(num);
                    }
                }
                var byteArray = stream.ToArray();

                // Trigger file download via JS
                await _jsRuntime.InvokeVoidAsync("saveAsFile", "data.bin", byteArray);
            }
        }
        catch (Exception ex)
        {
            // Optionally log or handle the exception
            Console.WriteLine($"Error exporting to binary: {ex.Message}");
        }
    }
}

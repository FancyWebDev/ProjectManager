using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;

namespace BLL.Services;

public interface IFileService
{
    Task<string> Upload(IFormFile file);
} 

public class FileService : IFileService
{
    private readonly IHostingEnvironment _hostingEnvironment;

    public FileService(IHostingEnvironment hostingEnvironment)
    {
        _hostingEnvironment = hostingEnvironment;
    }
    
    public async Task<string> Upload(IFormFile file)
    {
        var fileName = new string(Path.GetFileNameWithoutExtension(file.FileName).Take(10).ToArray()).Replace(' ', '-');
        fileName += DateTime.Now.ToString("yyyy-M-d") + Path.GetExtension(file.FileName);
        var filePath = Path.Combine(_hostingEnvironment.ContentRootPath, "Files", fileName);
        await using var fileStream = new FileStream(filePath, FileMode.Create);
        await file.CopyToAsync(fileStream);
        return fileName;
    }
}
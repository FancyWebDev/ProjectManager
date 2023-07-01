using System.IO;
using System.Threading.Tasks;
using Dropbox.Api;
using Dropbox.Api.Files;
using Microsoft.AspNetCore.Http;

namespace BLL.Services
{
    public class FileService
    {
        private const string Token = "sl.BhWc1kbeC5T0docTPI5kpNRmpkOoiZWhlSVBnC4MFgJaTtAwJV7NG1pcS6jLBq_Mt6_UJRGmlkk5B80pH0Kbp3w1f6obaO80ewqLHOrl8d5vcyP736R9s4_g-XwIcQePg9F7RMk";
   
        public async Task<string> Upload(string folder, IFormFile file)
        {
            using var dropboxClient = new DropboxClient(Token);
            string filePath = Path.GetTempFileName();
            await using var mem = new MemoryStream(await File.ReadAllBytesAsync(filePath));
            string path = folder + "/" + file.FileName; 

            await using (var stream = File.Create(filePath)) 
                await file.CopyToAsync(stream);

            await dropboxClient.Files.UploadAsync(
                path,
                WriteMode.Overwrite.Instance,
                body: mem);
            
            var shareLinkInfo = new Dropbox.Api.Sharing.CreateSharedLinkWithSettingsArg(path);
            var responseShare = await dropboxClient.Sharing.CreateSharedLinkWithSettingsAsync(shareLinkInfo);
            
            return responseShare.Url;
        }

        public async Task Download(string folder, string file)
        {
            using var dropboxClient = new DropboxClient(Token);
            using var response = await dropboxClient.Files.DownloadAsync(folder + "/" + file);
        }
    }
}
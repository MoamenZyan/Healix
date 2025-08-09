using Microsoft.AspNetCore.Http;

public interface IS3Service
{
    Task<string> UploadFile(IFormFile file);
}

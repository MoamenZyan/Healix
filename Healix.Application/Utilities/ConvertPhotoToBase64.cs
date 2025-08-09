using Microsoft.AspNetCore.Http;

public static class ConvertFileToBase64
{
    public static async Task<string> ConvertFile(string fileUrl)
    {
        using (HttpClient client = new HttpClient())
        {
            byte[] fileBytes = await client.GetByteArrayAsync(fileUrl);

            string base64String = Convert.ToBase64String(fileBytes);

            return base64String;
        }
    }
}

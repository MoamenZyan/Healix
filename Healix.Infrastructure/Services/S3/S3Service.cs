using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

public class S3Service : IS3Service
{
    private readonly IAmazonS3 _amazonS3;
    private readonly AwsConfigurations _awsConfigurations;

    public S3Service(IAmazonS3 amazonS3, IOptions<AwsConfigurations> awsConfigurations)
    {
        _amazonS3 = amazonS3;
        _awsConfigurations = awsConfigurations.Value;
    }

    public async Task<string> UploadFile(IFormFile file)
    {
        using (var stream = file.OpenReadStream())
        {
            var key = Guid.NewGuid().ToString();
            var putRequest = new PutObjectRequest()
            {
                BucketName = _awsConfigurations.BucketName,
                Key = key,
                InputStream = stream,
                ContentType = file.ContentType,
            };

            var response = await _amazonS3.PutObjectAsync(putRequest);
            if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
                return $"https://{_awsConfigurations.BucketName}.s3.eu-north-1.amazonaws.com/"
                    + key;

            throw new Exception("Photo upload failed");
        }
    }
}

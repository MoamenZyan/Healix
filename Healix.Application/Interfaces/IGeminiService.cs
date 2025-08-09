using Microsoft.AspNetCore.Http;

public interface IGeminiService
{
    Task<Content> SendMessageAsync(List<Content> contents);
    Task<Content> GetPatientSummary(List<Content> contents);
    Task<Content> GetFamilySummary(List<Content> contents);
}

namespace Resume_Portfolio.Services;

public class CompanyService
{
    private readonly HttpClient httpClient;
    private readonly ILogger<CompanyService> logger;

    public CompanyService(HttpClient httpClient, ILogger<CompanyService> logger)
    {
        this.httpClient = httpClient;
        this.logger = logger;
    }

    public async Task<string> RetrieveCompanyWebsiteUrlAsync(int companyId)
    {
        try
        {
            var response = await httpClient.GetAsync($"https://externalapi.com/applicationstatus/{companyId}");

            if (response.IsSuccessStatusCode)
            {
                var companyWebsiteUrl = await response.Content.ReadAsStringAsync();
                return companyWebsiteUrl;
            }
            else
            {
                // Log the error
                logger.LogError($"An error occurred while retrieving company website URL: {response.StatusCode}");
                return string.Empty; // Return empty string instead of null
            }
        }
        catch (HttpRequestException e)
        {
            // Log the error and rethrow the exception.
            logger.LogError(e, "HttpRequestException occurred while retrieving company website URL.");
            throw;
        }
        catch (Exception e)
        {
            // Log the error and rethrow the exception.
            logger.LogError(e, "Unexpected error occurred while retrieving company website URL.");
            throw;
        }
    }
}

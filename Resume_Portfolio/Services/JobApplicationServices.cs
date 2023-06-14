using Resume_Portfolio.Models;

namespace Resume_Portfolio.Services;

public class JobApplicationService
{
    private readonly IHttpClientFactory clientFactory;
    private readonly ILogger<JobApplicationService> logger;

    public JobApplicationService(IHttpClientFactory clientFactory, ILogger<JobApplicationService> logger)
    {
        this.clientFactory = clientFactory;
        this.logger = logger;
    }

    public async Task RetrieveApplicationStatusUrlAsync(JobApplication jobApplication)
    {
        try
        {
            var client = clientFactory.CreateClient();
            var response = await client.GetAsync($"https://externalapi.com/applicationstatus/{jobApplication.JobApplicationID}");

            if (response.IsSuccessStatusCode)
            {
                var applicationStatusUrl = await response.Content.ReadAsStringAsync();
                jobApplication.ApplicationStatusPage = applicationStatusUrl;
            }
            else
            {
                // Log the error or do something with it.
                logger.LogError($"An error occurred: {response.StatusCode}");
            }
        }
        catch (HttpRequestException e)
        {
            // Handle HttpRequestException which could occur if the request fails.
            logger.LogError(e, "Request exception");
            throw; // rethrow the exception if you can't handle it here.
        }
        catch (Exception e)
        {
            // Handle any other exception.
            logger.LogError(e, "Unexpected error");
            throw; // rethrow the exception if you can't handle it here.
        }
    }
}

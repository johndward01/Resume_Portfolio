using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Resume_Portfolio.Data;
using Resume_Portfolio.Interfaces;
using Resume_Portfolio.Models;

namespace Resume_Portfolio.Repositories;

public class JobRepository : IJobRepository
{
    private readonly JobContext context;
    private readonly ILogger<JobRepository> logger;
    private readonly IMapper mapper;

    public JobRepository(JobContext context, ILogger<JobRepository> logger, IMapper mapper)
    {
        this.context = context;
        this.logger = logger;
        this.mapper = mapper;
    }

    public async Task<List<JobViewModel>> List()
    {
        var jobs = await context.Jobs.ToListAsync();
        var jobViewModels = mapper.Map<List<JobViewModel>>(jobs);
        return jobViewModels;
    }

    public async Task<IEnumerable<Job>> GetAllJobs()
    {
        return await context.Jobs.ToListAsync();
    }

    public async Task<IEnumerable<Company>> GetCompanies()
    {
        try
        {
            var companies = await context.Companies.ToListAsync();
            return companies;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while getting companies");
            throw;
        }
    }

    public async Task<Job> GetJob(int id)
    {
        var job = await context.Jobs.Include(j => j.RequiredSkills).FirstOrDefaultAsync(j => j.JobID == id);

        if (job == null)
        {
            throw new Exception($"No jobs found with ID {id}");
        }
        return job;
    }

    public async Task<Job> CreateJob(JobDto jobData)
    {
        try
        {
            if (jobData == null)
            {
                throw new ArgumentNullException(nameof(jobData), "Job data cannot be null");
            }

            if (context == null)
            {
                throw new InvalidOperationException("The context is null");
            }

            if (logger == null)
            {
                throw new InvalidOperationException("The logger is null");
            }

            var job = new Job
            {
                Title = jobData.Title,
                Location = jobData.Location,
                ExperienceYears = jobData.ExperienceYears,
                Remote = jobData.Remote,
                CompanyID = jobData.CompanyID
            };

            context.Jobs.Add(job);

            await context.SaveChangesAsync();

            return job;
        }
        catch (Exception ex)
        {
            // Log the exception here using a logger, if you have one.
            logger?.LogError(ex, "An error occurred while creating the job");

            // Then, throw the exception again to let the caller know that something went wrong.
            throw;
        }
    }

    public async Task<Job> UpdateJob(int jobId, JobDto jobData)
    {
        var job = await context.Jobs.FindAsync(jobId);

        if (job != null)
        {
            job.Title = jobData.Title;
            job.Location = jobData.Location;
            job.ExperienceYears = jobData.ExperienceYears;
            job.Remote = jobData.Remote;
            job.CompanyID = jobData.CompanyID;

            context.Jobs.Update(job);

            await context.SaveChangesAsync();
        }
        return job;
    }

    public async Task<IEnumerable<RequiredSkill>> GetRequiredSkills()
    {
        return await context.RequiredSkills.ToListAsync();
    }

    public async Task<Job> AssignRequiredSkill(Job job, IEnumerable<RequiredSkill> requiredSkills)
    {
        job.RequiredSkills = requiredSkills.ToList();

        context.Jobs.Update(job);

        await context.SaveChangesAsync();

        return job;
    }

    public async Task DeleteJob(int jobId)
    {
        var job = await context.Jobs.FindAsync(jobId);

        if (job != null)
        {
            context.Jobs.Remove(job);

            await context.SaveChangesAsync();
        }
        else
        {
            throw new Exception($"Job with ID {jobId} not found");
        }
    }

}
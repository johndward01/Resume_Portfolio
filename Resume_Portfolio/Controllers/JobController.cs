using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Resume_Portfolio.Interfaces;
using Resume_Portfolio.Models;

namespace Resume_Portfolio.Controllers;
public class JobController : Controller
{
    private readonly IJobRepository repo;
    private readonly ILogger<JobController> logger;
    private readonly IMapper mapper;
    private readonly IConfiguration configuration;
    private readonly string apiKey;

    public JobController(
        IJobRepository repo,
        ILogger<JobController> logger,
        IMapper mapper,
        IConfiguration configuration)
    {
        this.repo = repo;
        this.logger = logger;
        this.mapper = mapper;
        this.configuration = configuration;
        apiKey = configuration["SensitiveConfig:ApiKey"];
    }

    public async Task<IActionResult> List()
    {
        try
        {
            var jobViewModels = await repo.List();
            return View(jobViewModels);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred in List action");
            throw;
        }
    }

    // GET: /<controller>/
    public async Task<IActionResult> Index()
    {
        try
        {
            var jobs = await repo.GetAllJobs();
            var jobViewModels = mapper.Map<List<JobViewModel>>(jobs);
            return View(jobViewModels);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred in Index action");
            throw;
        }
    }

    public async Task<IActionResult> ViewJob(int id)
    {
        try
        {
            var job = await repo.GetJob(id);
            var jobViewModel = mapper.Map<JobViewModel>(job);
            return View(jobViewModel);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, $"Error occurred in ViewJob action for job ID {id}");
            throw;
        }
    }

    public async Task<IActionResult> UpdateJob(int id)
    {
        try
        {
            var job = await repo.GetJob(id);
            var jobViewModel = mapper.Map<JobViewModel>(job);

            if (jobViewModel == null)
            {
                return View("JobNotFound");
            }

            return View(jobViewModel);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, $"Error occurred in UpdateJob action for job ID {id}");
            throw;
        }
    }

    [HttpPost]
    public async Task<IActionResult> UpdateJobToDatabase(JobViewModel jobViewModel)
    {
        try
        {
            if (ModelState.IsValid)
            {
                var jobDto = mapper.Map<JobDto>(jobViewModel.Job);
                var job = await repo.UpdateJob(jobDto.JobID, jobDto);
                var jobUpdatedViewModel = mapper.Map<JobViewModel>(job);

                return View("ViewJob", jobUpdatedViewModel);
            }
            else
            {
                return View("UpdateJob", jobViewModel);
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, $"Error occurred in UpdateJobToDatabase action for job ID {jobViewModel.Job.JobID}");
            throw;
        }
    }

    public async Task<IActionResult> InsertJob()
    {
        try
        {
            var companies = await repo.GetCompanies();
            var jobViewModel = new JobViewModel
            {
                Job = new Job(),
                Companies = companies
            };

            return View(jobViewModel);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred in InsertJob action");
            throw;
        }
    }

    [HttpPost]
    public async Task<IActionResult> InsertJobToDatabase(JobViewModel model)
    {
        try
        {
            if (ModelState.IsValid)
            {
                var jobDto = mapper.Map<JobDto>(model.Job);
                var job = await repo.CreateJob(jobDto);
                var jobViewModel = mapper.Map<JobViewModel>(job);

                return RedirectToAction("ViewJob", new { id = jobViewModel.Job.JobID });
            }
            else
            {
                model.Companies = await repo.GetCompanies();
                return View("InsertJob", model);
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred in InsertJobToDatabase action");
            throw;
        }
    }

    public async Task<IActionResult> DeleteJob(int id)
    {
        try
        {
            await repo.DeleteJob(id);
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, $"Error occurred in DeleteJob action for job ID {id}");
            throw;
        }
    }
}

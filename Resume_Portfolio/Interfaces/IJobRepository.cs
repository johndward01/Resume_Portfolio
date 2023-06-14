using Resume_Portfolio.Models;

namespace Resume_Portfolio.Interfaces;

public interface IJobRepository
{
    Task<IEnumerable<Job>> GetAllJobs();
    Task<Job> GetJob(int id);
    Task<Job> CreateJob(JobDto jobData);
    Task<Job> UpdateJob(int jobId, JobDto jobData);
    Task<IEnumerable<RequiredSkill>> GetRequiredSkills();
    Task<Job> AssignRequiredSkill(Job jobs, IEnumerable<RequiredSkill> requiredSkills);
    Task DeleteJob(int jobId);
    Task<IEnumerable<Company>> GetCompanies();
    Task<List<JobViewModel>> List();
}

namespace Resume_Portfolio.Models;

public class JobViewModel
{
    public int JobID { get; set; }
    public string? Title { get; set; }
    public string? Location { get; set; }
    public int? ExperienceYears { get; set; }
    public bool Remote { get; set; }
    public int? CompanyID { get; set; }

    public IEnumerable<Company>? Companies { get; set; }

    public Job Job { get; set; }

    public JobViewModel()
    {
        Title = string.Empty;
        Location = string.Empty;
        ExperienceYears = 0;
        Remote = false;
        CompanyID = null;
    }
}



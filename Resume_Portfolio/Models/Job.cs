using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Resume_Portfolio.Models;

[Table("jobs")]
public class Job
{
    public Job()
    {
        Title = string.Empty;
        Location = string.Empty;
        Company = new Company();
    }

    [Key]
    public int JobID { get; set; }

    [Required]
    public int CompanyID { get; set; }

    [Required, MaxLength(100)]
    public string Title { get; set; }

    [Required, MaxLength(100)]
    public string Location { get; set; }

    [Required]
    public int ExperienceYears { get; set; }

    [Required]
    public bool Remote { get; set; }

    // Navigation property
    public virtual Company Company { get; set; }
    public virtual ICollection<RequiredSkill> RequiredSkills { get; set; } = new List<RequiredSkill>();
}
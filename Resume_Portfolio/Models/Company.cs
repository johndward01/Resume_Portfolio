using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Resume_Portfolio.Models;

[Table("companies")]
public class Company
{
    public Company()
    {
        Jobs = new List<Job>();
    }

    [Key]
    public int CompanyID { get; set; }

    [Required, MaxLength(100)]
    public string? CompanyName { get; set; }

    [Required, MaxLength(100)]
    public string? CompanyWebsite { get; set; }

    public int CompanySize { get; set; }

    // Navigation property
    public virtual ICollection<Job> Jobs { get; set; }
}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Resume_Portfolio.Models;

[Table("jobapplications")]
public class JobApplication
{
    [Key]
    public int JobApplicationID { get; set; }
    [Required]
    public int JobID { get; set; }
    [Required]
    public int ApplicantID { get; set; }
    [Required]
    public string? ApplicationStatusPage { get; set; }
    public string? Status { get; set; }
    public string? Notes { get; set; }
    // Navigation properties
    public Job? Job { get; set; } // Added this line
}

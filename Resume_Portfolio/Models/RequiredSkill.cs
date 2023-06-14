using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Resume_Portfolio.Models;

[Table("requiredskills")]
public class RequiredSkill
{
    [Key]
    public int RequiredSkillID { get; set; }

    [Required, MaxLength(100)]
    public string? RSkill { get; set; }

    public int JobID { get; set; }
    public Job? Job { get; set; }
}
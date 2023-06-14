using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Resume_Portfolio.Models;

[Table("jobrequiredskills")]
public class JobRequiredSkill
{
    [Key]
    public int JobID { get; set; }

    [Key]
    public int RequiredSkillID { get; set; }

}

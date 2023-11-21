
namespace Domain.Model;

public class Survey
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int SupplierId { get; set; }
    
    public int CreatorId { get; set; }
    public DateTime CreationTime { get; set; }
    
    public List<Question> Questions { get; set; }
    public DateTime? AnsweredTime { get; set; }
    
    
}
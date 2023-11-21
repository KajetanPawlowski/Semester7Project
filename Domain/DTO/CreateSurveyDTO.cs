using Domain.Model;

namespace Domain.DTO;

public class CreateSurveyDTO
{
    public string Name { get; init; }
    public int SupplierId { get; init; }
    
    public string CreatorMail { get; init; }
    
    public List<Question> Questions { get; init; }
}

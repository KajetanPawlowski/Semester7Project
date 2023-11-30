using Domain.Model;

namespace Domain.DTO;

public class CreateSurveyDTO
{
    
    public int SupplierId { get; set; }
    
    public int CreatorId { get; set; }
    
    public List<CreateQuestionDTO> Questions { get; set; }
}

namespace Domain.Model;

public class QuestionCategory
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int RiskCategoryId { get; set; }
    public int RiskMappingId { get; set; }
    public int ImpactedGroupId { get; set; }
}
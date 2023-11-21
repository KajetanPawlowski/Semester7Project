namespace Application.LogicInterface;

public interface IRiskAttributeLogic
{
    void CreateFromFile(string type, int score, string description);
}
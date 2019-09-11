namespace StoryTeller
{
    public interface ICommandGenerator 
    {
        bool Generate(string line, out ICommand command);
    }
}
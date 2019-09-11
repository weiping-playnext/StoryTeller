namespace StoryTeller
{
    public interface IActorFactory
    {
        IActor Create(string uri);
    }
}
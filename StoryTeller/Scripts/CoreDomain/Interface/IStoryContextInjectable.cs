using System;
namespace StoryTeller
{
    public interface IStoryContextInjectable
    {
        void Inject(IStoryContext context);
    }
}

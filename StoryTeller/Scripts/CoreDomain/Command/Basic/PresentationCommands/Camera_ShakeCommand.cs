using System;

namespace StoryTeller
{
    public class Camera_ShakeCommand : ICommand
    {
        public bool wait = false;

        public Camera_ShakeCommand()
        {
        }

        public void Execute(ICallStack callStack, IStoryContext context)
        {
            context.ScenePresenter.CameraShake(context.MessagePresenter, wait);
        }

        public bool ProceedInSameFrame { get { return !wait; } }
        public int GetNextCommandIndex(int current)
        {
            return current + 1;
        }
    }
}
using System;
namespace StoryTeller
{
    public class Chara_FaceCommand : ICommand
    {
        public string name = String.Empty;
        public string face = String.Empty;
        public string asset = String.Empty;

        public Chara_FaceCommand()
        {
        }

        public bool ProceedInSameFrame
        {
            get { return true; }
        }

        public void Execute(ICallStack callStack, IStoryContext context)
        {
            context.ActorPresenter.RegisterActorFace(name, face, asset);
        }

        public int GetNextCommandIndex(int current)
        {
            return current + 1;
        }
    }
}

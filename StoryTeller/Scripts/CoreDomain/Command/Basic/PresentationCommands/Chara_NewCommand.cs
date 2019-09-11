using System;
namespace StoryTeller
{
    public class Chara_NewCommand : ICommand
    {
        public string name = String.Empty;
        public string tag = String.Empty;
        public string dname = String.Empty;
        public string asset = String.Empty;
        public string size = String.Empty;

        public Chara_NewCommand()
        {
        }

        public bool ProceedInSameFrame { get { return true; } }

        public void Execute(ICallStack callStack, IStoryContext context)
        {
            context.VariableRepository.SetString(name, dname);
            context.ActorPresenter.RegisterNewActor(name, tag, asset);
            if (!string.IsNullOrEmpty(size))
            {
                context.ActorPresenter.SetSize(name, size);
            }
        }

        public int GetNextCommandIndex(int current)
        {
            return current + 1;
        }
    }
}

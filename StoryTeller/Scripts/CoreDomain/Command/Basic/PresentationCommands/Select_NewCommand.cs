using System;

namespace StoryTeller
{
    public class Select_NewCommand : ICommand, ISelectionEventListener
    {
        public string text = String.Empty;

        public string target = String.Empty;
        int index = -1;

        ICallStack callStack;
        IStoryContext context;

        public Select_NewCommand()
        {
        }

        public bool ProceedInSameFrame { get { return true; } }

        public void Execute(ICallStack callStack, IStoryContext context)
        {
            this.callStack = callStack;
            this.context = context;
            context.SelectionPresenter.AddSelection(text, this);
        }

        void JumpToTarget()
        {
            callStack.ClearIfStack();
            target = ArgumentExpression.replaceVariable(target, context.VariableRepository);
            index = context.ScenarioRepository.GetCurrentRunningScenario().getIndex(target);
            callStack.JumpToCommand(index);
            
        }

        public int GetNextCommandIndex(int current)
        {
            return current + 1;
        }

        public void OnSelected(string selection)
        {
            if(selection == text)
            {
                JumpToTarget();
                context.StoryPlayer.Play();
            }
        }
    }
}

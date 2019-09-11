using System;
namespace StoryTeller
{
    public class _InverseIfCommand : ILogicControlCommand
    {
        public string MainExp = String.Empty;
        public string OptionalExp = String.Empty;

        int nextIndex;

        public _InverseIfCommand()
        {

        }

        public bool ProceedInSameFrame { get { return true; } }

        public void Execute(ICallStack callStack, IStoryContext context)
        {

            bool result = bool.Parse(ArgumentExpression.Calculate(MainExp));
            bool optionalResult = true;
            if(!string.IsNullOrEmpty(OptionalExp))
            {
                optionalResult = bool.Parse(ArgumentExpression.Calculate(OptionalExp));
            }

            bool resultingIf = callStack.ResultingIf;
            callStack.AddIfStack(result);
            if (!result && resultingIf && optionalResult)
            {
                nextIndex = callStack.CurrentCommandIndex + 1;
            }
            else
            {
                var currentScenario = context.ScenarioRepository.GetCurrentRunningScenario();
                for (int i = callStack.CurrentCommandIndex + 1; i < currentScenario.Commands.Count; i++)
                {
                    if (typeof(ILogicControlCommand).IsAssignableFrom(currentScenario.Commands[i].GetType()))
                    {
                        nextIndex = i;
                        break;
                    }
                }
            }
        }

        public int GetNextCommandIndex(int current)
        {
            return nextIndex;
        }
    }
}

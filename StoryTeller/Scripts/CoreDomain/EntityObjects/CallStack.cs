using System;
using System.Collections.Generic;
using System.Linq;

namespace StoryTeller
{
    public interface ICallStack
    {
        int Push();
        CallStackItem Pop();
        int Count { get; }
        int CurrentCommandIndex { get; }

        void JumpToCommand(int commandIndex);
        void AddIfStack(bool value);
        IfStackItem CurrentIf { get; }
        IfStackItem PopIfStack();
        int IfStackCount { get; }
        bool ResultingIf { get; }
        void ClearIfStack();
    }

    public class CallStack : ICallStack
    {
        int commandIndex = 0;
        public int CurrentCommandIndex
        { 
            get { return commandIndex; }
            set { commandIndex = value; }
        }

        Stack<CallStackItem> callStacks = new Stack<CallStackItem>();

        Stack<IfStackItem> ifStacks = new Stack<IfStackItem>();

        IStoryContext context;

        public CallStack(IStoryContext context)
        {
            this.context = context;
        }

        public int Push()
        {
            callStacks.Push(new CallStackItem(context.ScenarioRepository.GetCurrentRunningScenario().name, CurrentCommandIndex));
            return CurrentCommandIndex;
        }

        public CallStackItem Pop()
        {
            return callStacks.Pop();
        }

        public void AddIfStack(bool value)
        {
            ifStacks.Push(new IfStackItem(value));
        }

        public IfStackItem PopIfStack()
        {
            if (ifStacks.Count > 0)
            {
                return ifStacks.Pop();
            }
            return null;
        }

        public void ClearIfStack()
        {
            ifStacks.Clear();
        }

        public void JumpToCommand(int commandIndex)
        {
            CurrentCommandIndex = commandIndex;
        }

        public int Count
        {
            get
            {
                return callStacks.Count;
            }
        }

        public IfStackItem CurrentIf => ifStacks.Peek();

        public int IfStackCount { get { return ifStacks.Count; } }

        public bool ResultingIf { get { return ifStacks.All((arg) => arg.ifValue); } }
    }
}

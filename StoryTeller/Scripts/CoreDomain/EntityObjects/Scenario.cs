using System;
using System.Collections.Generic;
using System.Linq;

namespace StoryTeller
{
    [Serializable]
    public class Scenario
    {
        public string name;
        public Dictionary<string, int> dicLabel = new Dictionary<string, int>();

        public IReadOnlyList<ICommand> Commands = new List<ICommand>();

        public Scenario()
        {
        }

        public Scenario(IEnumerable<ICommand> commands)
        {
            var commandList= new List<ICommand>(commands);
            Commands = commandList;
            var labels = commands.OfType<LabelCommand>()
                .ToArray();
            foreach(var label in labels)
            {
                addLabel(label.name, commandList.IndexOf(label));
            }
        }

        public void addLabel(string label_name, int index)
        {
            //Debug.Log (label_name);
            this.dicLabel[label_name] = index;
        }

        public int getIndex(string label_name)
        {

            if (string.IsNullOrEmpty(label_name))
                return -1;

            if (!this.dicLabel.ContainsKey(label_name))
            {
                return -1;
            }

            return this.dicLabel[label_name];

        }

    }
}

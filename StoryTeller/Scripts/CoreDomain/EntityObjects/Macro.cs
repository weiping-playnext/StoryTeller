using System;
namespace StoryTeller
{
    [Serializable]
    public class Macro
    {
        public string name;
        public string file_name;
        public int index;

        public Macro()
        {
        }

        public Macro(string name, string file_name, int index)
        {
            this.name = name;
            this.file_name = file_name;
            this.index = index;
        }

    }
}

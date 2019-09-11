using System;
namespace StoryTeller
{
    //if文の入れ子などを管理するスタック
    [Serializable]
    public class IfStackItem
    {

        public bool ifValue = false;

        public IfStackItem()
        {
        }

        public IfStackItem(bool val)
        {
            this.ifValue = val;
        }


    }
}

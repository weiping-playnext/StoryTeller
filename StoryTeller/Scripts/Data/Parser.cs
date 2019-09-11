using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.ComponentModel;
using System.Linq;
using System.IO;

namespace StoryTeller.Data
{

    //スクリプトファイルを読み込んで、適切な形にパースして返します
    /*

*start

[cm  ]test [l][r]
[back  storage="room.jpg"  time="5000"  ]

*/
    public class Parser : IStoryParser
    {
        CommandGenerator commandGenerator = new CommandGenerator();

        public string errorMessage = "";
        public string warningMessage = "";

        private System.Globalization.TextInfo tf = new System.Globalization.CultureInfo("en-US", false).TextInfo;

        private string classPrerix = "";

        public Parser(string classPrefix)
        {
            this.classPrerix = classPrefix;
        }

        struct LineTagStruct
        {
            public int line_num;
            public string line;
            public LineTagStruct(int line_num, string line)
            {
                this.line_num = line_num;
                this.line = line;
            }
        }


        ICommand GenerateCommand(string line, int line_num)
        {
            ICommand command = null;
            if(commandGenerator.Generate(line, out command))
            {
                return command;
            }
            else
            {
                Debug.LogError("Parse failed. Line:" + line_num + " | " + line);
                return null;
            }
        }

        public IList<ICommand> Parse(string script_text)
        {
            List<ICommand> commands = new List<ICommand>();

            StringReader stringReader = new StringReader(script_text);
            List<LineTagStruct> lineTags = new List<LineTagStruct>();

            int line_num = 0;
            bool commentNow = false;
            while(stringReader.Peek() > 0)
            {
                string line = stringReader.ReadLine();
                line = line.Trim();
                line_num++;
                if (string.IsNullOrEmpty(line))
                    continue;
                if(line.IndexOf("/*",StringComparison.InvariantCultureIgnoreCase) == 0)
                {
                    commentNow = true;
                }
                if(line.IndexOf("*/",StringComparison.InvariantCultureIgnoreCase) >= 0)
                {
                    commentNow = false;
                    continue;
                }
                if (commentNow)
                    continue;

                if (PrepareLine(ref line))
                {
                    PopulateTagLines(line_num, line, ref lineTags);
                }
            }

            TranslateTagLinesToCommands(lineTags, ref commands);

            FlattenSystemMacros(ref commands);

            return commands;
        }

        bool PrepareLine(ref string line)
        {
            char firstChar = line[0];
            if (firstChar == ';')
                return false;
            switch (firstChar)
            {
                case '*':
                    line = "[label name='" + line.Substring(1).Trim() + "' ]";
                    break;
                case '@':
                    line = "[" + line.Substring(1).Trim() + "]";
                    break;
                case '#':
                    line = "[talk_name val='" + line.Substring(1).Trim() + "' ]";
                    break;

            }
            return true;
        }

        void FlattenSystemMacros(ref List<ICommand> commands)
        {
            var macroCommands = commands.Where((arg) => typeof(ISystemMacroCommand).IsAssignableFrom(arg.GetType()))
                .Cast<ISystemMacroCommand>()
                .ToList();
            while (macroCommands.Count > 0)
            {
                foreach (var macroCommand in macroCommands)
                {
                    int index = commands.IndexOf(macroCommand);
                    var newCommands = macroCommand.GetDerivedCommands(commands);
                    commands.RemoveAt(index);
                    commands.InsertRange(index, newCommands);
                }
                macroCommands = commands.Where((arg) => typeof(ISystemMacroCommand).IsAssignableFrom(arg.GetType()))
                .Cast<ISystemMacroCommand>()
                .ToList();
            }
        }

        void PopulateTagLines(int line_num, string line, ref List<LineTagStruct> lineTags)
        {
            StringBuilder tagLine = new StringBuilder();
            int strLen = line.Length;
            bool toReadForTag = false;
            for (int i = 0; i < strLen; i++)
            {
                char c = line[i];
                if (toReadForTag && c == '[')
                {
                    lineTags.Add(new LineTagStruct(line_num, tagLine.ToString()));
                    toReadForTag = false;
                    tagLine.Clear();
                }

                tagLine.Append(c);

                if (i == strLen - 1)
                {
                    lineTags.Add(new LineTagStruct(line_num, tagLine.ToString()));
                    break;
                }

                toReadForTag = true;

                if (c == ']')
                {
                    lineTags.Add(new LineTagStruct(line_num, tagLine.ToString()));
                    toReadForTag = false;
                    tagLine.Clear();
                }
            }
        }

        void TranslateTagLinesToCommands(List<LineTagStruct> lineTags, ref List<ICommand> commands)
        {
            foreach (LineTagStruct lo in lineTags)
            {

                string line = lo.line;
                int line_num = lo.line_num;
                char firstChar = line[0];
                //テキストファイルの場合
                if (firstChar != '[' && firstChar != '@')
                {

                    line = "[story val=\"" + line + "\"]";
                    firstChar = '[';

                }

                if (firstChar == '[' || firstChar == '@')
                {
                    ICommand cmp = this.GenerateCommand(line, line_num);
                    if (cmp != null)
                    {
                        //リストに追加
                        commands.Add(cmp);
                    }
                    else
                    {
                        Tag tag = Tag.Parse(line);
                        _MacrostartCommand macrostartCommand = new _MacrostartCommand();
                        macrostartCommand.name = tag.Name;
                        commands.Add(macrostartCommand);
                    }
                    continue;
                }
            }
        }


    }
}
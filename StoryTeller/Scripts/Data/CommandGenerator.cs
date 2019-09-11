using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System;
using System.ComponentModel;

namespace StoryTeller.Data
{
    public class CommandGenerator : ICommandGenerator
    {
        Assembly searchAssembly;

        private System.Globalization.TextInfo tf = new System.Globalization.CultureInfo("en-US", false).TextInfo;

        public CommandGenerator()
        {
            searchAssembly = Assembly.GetAssembly(typeof(ICommand));
        }


        public bool Generate(string line, out ICommand command)
        { 
            command = null;
            Tag tag = Tag.Parse(line);
            string className = "StoryTeller." + tf.ToTitleCase(tag.Name) + "Command";
            Type masterType = searchAssembly.GetType(className);

            if (masterType != null && typeof(ICommand).IsAssignableFrom(masterType))
            {
                var rawCommand = Activator.CreateInstance(masterType);
                var dict = tag.getParamByDictionary();
                foreach (var kvp in dict)
                {
                    try
                    {
                        FieldInfo fieldInfo = masterType.GetField(kvp.Key, BindingFlags.GetField | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                        if (fieldInfo != null)
                        {
                            TypeConverter typeConverter = TypeDescriptor.GetConverter(fieldInfo.FieldType);
                            if (typeConverter != null)
                            {
                                string valString = kvp.Value.TrimStart('*');
                                var objVal = typeConverter.ConvertFromString(valString);
                                fieldInfo.SetValue(rawCommand, objVal);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        //Debug.LogError("Parse failed. Line:" + line_num + " " + kvp.ToString());
                        Debug.LogException(ex);
                        return false;
                    }
                }

                command = (ICommand)rawCommand;
                return true;
            }
            return false;
        }
    }
}
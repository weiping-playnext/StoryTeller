using System;
using System.Reflection;
using System.Collections.Generic;

namespace StoryTeller
{
    public static class StoryContextUtility
    {
        public static void Resolve(this IStoryContext context)
        {
            Type myType = context.GetType();
            PropertyInfo[] propertyInfos = myType.GetProperties();
            List<IStoryEventListener> eventListeners = new List<IStoryEventListener>();
            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                var propInstance = propertyInfo.GetValue(context);
                if (propInstance != null)
                {
                    Type propType = propInstance.GetType();
                    if (typeof(IStoryContextInjectable).IsAssignableFrom(propType))
                    {
                        IStoryContextInjectable injectable = (IStoryContextInjectable)propInstance;
                        injectable.Inject(context);
                    }
                    if(typeof(IStoryEventListener).IsAssignableFrom(propType))
                    {
                        eventListeners.Add((IStoryEventListener)propInstance);
                    }
                }
            }

            if(typeof(IStoryEventDispatcher).IsAssignableFrom(myType))
            {
                var methodInfo = myType.GetMethod("AddEventListener");
                methodInfo.Invoke(context, new object[] { eventListeners.ToArray() });
            }
        }
    }
}

using UnityEngine;

namespace StoryTeller
{
    public class Region : MonoBehaviour
    {
        public string Tag { get; }
        public Transform Transform { 
            get 
            { 
                return gameObject.transform; 
            } 
        }
    }
}
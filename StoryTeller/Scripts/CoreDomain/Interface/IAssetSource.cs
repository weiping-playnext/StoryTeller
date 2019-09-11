using UnityEngine;

namespace StoryTeller
{
    public interface IAssetSourceEventListener 
    {
        void OnAssetLoadStart();
        void OnAssetLoadComplete();
    }

    public interface IAssetSource : System.IDisposable
    {
        void AddAssetURI(string uri);
        void AddTag(string uri, string tag);
        T GetAsset<T>(string tag) where T : Object;
    }
}

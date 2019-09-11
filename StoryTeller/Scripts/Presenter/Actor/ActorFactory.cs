using UnityEngine;

namespace StoryTeller.Presentation
{
    public class ActorFactory<T> : IStoryContextInjectable, IActorFactory where T : IActor 
    {
        IAssetSource assetSource;

        public IActor Create(string uri)
        {
            var go = GameObject.Instantiate(assetSource.GetAsset<GameObject>(uri));
            return go.GetComponentInChildren<IActor>();
        }

        public void Inject(IStoryContext context)
        {
            this.assetSource = context.AssetSource;
        }
    }
}

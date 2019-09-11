using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StoryTeller.Presentation
{
    public class ActorPresenter : MonoBehaviour, IActorPresenter, IStoryContextInjectable
    {
        // [SerializeField] List<SpriteActor> spriteActors;
        [SerializeField] Transform actorView = null;
        [SerializeField] List<Region> regions = null;
        [SerializeField] GameObject characterContainer = null;

        Dictionary<string, IActor> actors = new Dictionary<string, IActor>();
        Dictionary<string, string> faceTagAssetIdTable = new Dictionary<string, string>();

        IActorFactory actorFactory;
        IMessagePresenter messagePresenter;

        public ActorPresenter()
        {
        }

        public void Inject(IStoryContext context)
        {
            this.actorFactory = context.ActorFactory;
            messagePresenter = context.MessagePresenter;
        }

        public void HideActor(string name, float time, bool wait = false)
        {
            var actor = actors[name];
            actor.HideActor(0.0f, time, () => { actor.Enabled = false; });
        }

        public void RegisterActorFace(string name, string face, string assetTag)
        {
            faceTagAssetIdTable[name + "/" + face] = assetTag;
        }

        public void RegisterNewActor(string name, string tag, string assetTag)
        {
            var instance = actorFactory.Create(assetTag);
            instance.Transform.SetParent(actorView, false);
            instance.Enabled = false;
            actors[name] = instance;
        }

        public void SetActorFace(string name, string face)
        {
            var actor = actors[name];
            var asset = faceTagAssetIdTable[name + "/" + face];
            actor.ChangeFace(asset);
        }

        public void SetFocus(string tag)
        {
            foreach (var actor in actors)
            {
                actor.Value.SpeakFocus(tag == actor.Key);
            }
        }

        public void SetRegion(string name, string region)
        {
            var actor = actors[name];
            if (regions.Count != 0)
            {
                var dest = regions.Find(a => a.gameObject.name.ToLower() == region);
                dest = dest == null ? regions[0] : dest;
                actor.Transform.position = dest.Transform.position;
            }
            else
            {
                Debug.LogWarning("regions invalid.");
            }
            actor.Transform.SetParent(characterContainer.transform);
        }

        public void ShowActor(string name, float time, bool wait = false)
        {
            var actor = actors[name];
            if (wait)
            {
                messagePresenter.Interactable = false;
            }
            actor.ShowActor(0.0f, 0.0f, () =>
            {
                actor.Enabled = true;
                actor.ShowActor(1.0f, time, () =>
                {
                    messagePresenter.Interactable = true;
                });
            });
        }

        public void TweenRegion(string name, string region, float duration, Action completion = null)
        {
            var actor = actors[name];
            var dest = regions.Find(a => a.gameObject.name.ToLower() == region);
            actor.TweenRegion(dest, duration, completion);
        }

        public void ShakeActor(string name, bool wait)
        {
            var actor = actors[name];

            if (wait)
            {
                StartCoroutine(ShakeActorCoroutine(messagePresenter, actor));
            }
            else
            {
                actor.Shake();
                Debug.Log(actor.isPlayingShakeAnimation);
            }
        }

        IEnumerator ShakeActorCoroutine(IMessagePresenter messagePresenter, IActor actor)
        {
            messagePresenter.Interactable = false;
            actor.Shake();

            while (!actor.PlayingAnimationIsName("ActorShake"))
            {
                yield return null;
            }

            while (actor.isPlayingShakeAnimation)
            {
                yield return null;
            }

            messagePresenter.Interactable = true;
        }

        public void SetSize(string name, string sizeKey, float time = 0u)
        {
            var actor = actors[name];
            if (actor != null)
            {
                actor.SetSize(sizeKey, time);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

namespace StoryTeller.Presentation
{
    public class SpriteActor : MonoBehaviour, IActor
    {
        [Serializable]
        public class ActorScale
        {
            public string Key = String.Empty;
            public Vector3 Scale = Vector3.one;

        }

        [SerializeField] List<Sprite> sprites = null;
        [SerializeField] Image actorRenderer = null;
        [SerializeField] Animator shakeAnimator = null;
        [SerializeField] ActorScale[] availableScales = new ActorScale[0];

        public Transform Transform { get { return transform; } }

        Color targetColor = Color.white;

        // [SerializeField] Vector3 positionAdjustment = Vector3.zero;

        // void Update()
        // {
        //     Transform.position += positionAdjustment;
        // }

        public bool Enabled
        {
            set
            {
                gameObject.SetActive(value);
            }
            get
            {
                return gameObject.activeSelf;
            }
        }

        public void ShowActor(float destination, float time, Action completion = null)
        {
            TweenAlpha(destination, time, completion);
        }

        public void HideActor(float destination, float time, Action completion = null)
        {
            TweenAlpha(destination, time, completion);
        }

        void TweenAlpha(float destination, float duration, Action onCompleted = null)
        {
            LeanTween.cancel(actorRenderer.gameObject);
            targetColor.a = destination;

            LeanTween.alpha((RectTransform)actorRenderer.transform, destination, duration)
                .setOnComplete(() => { if (onCompleted != null) onCompleted(); });
        }

        public void ChangeFace(string faceId)
        {
            var face = sprites.Find(a => a.name == faceId);
            actorRenderer.sprite = face;
        }

        public void ChangePose(string poseId)
        {
            throw new System.NotImplementedException();
        }

        public void SetColor(Color color)
        {
            actorRenderer.color = color;
        }

        public void SetPosition(Vector3 position)
        {
            throw new System.NotImplementedException();
        }

        public void SetRotation(Vector3 rotation)
        {
            throw new System.NotImplementedException();
        }

        public void SetScale(Vector3 scale)
        {
            throw new System.NotImplementedException();
        }

        public void TweenRegion(Region region, float duration, Action onCompleted = null)
        {
            LeanTween.cancel(gameObject);

            LeanTween.move(this.gameObject, region.Transform, duration)
                .setOnComplete(() => { if (onCompleted != null) onCompleted(); });
        }

        public void Shake()
        {
            shakeAnimator.Play("ActorShake");
        }

        public bool isPlayingShakeAnimation
        {
            get { return shakeAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f; }
        }

        public bool PlayingAnimationIsName(string name)
        {
            return shakeAnimator.GetCurrentAnimatorStateInfo(0).IsName(name);
        }



        public void SpeakFocus(bool isSpeaker)
        {
            LeanTween.cancel(gameObject);
            float alpha = targetColor.a;

            targetColor = isSpeaker ? Color.white : Color.gray;
            targetColor.a = alpha;
            actorRenderer.color = targetColor;

            LeanTween.color((RectTransform)actorRenderer.transform, targetColor, 0.3f);

            if (isSpeaker) transform.SetAsLastSibling();
            //Debug.Log("isSpeaker: " + isSpeaker + " / Speaking: " + gameObject.name);
        }

        public void SetSize(string sizeKey, float time = 0)
        {
            ActorScale actorScale = new ActorScale();
            if(availableScales.Length > 0)
            {
                var selected = availableScales.SingleOrDefault(s => s.Key == sizeKey);
                if (selected != null)
                {
                    actorScale = selected;
                }
            }
            transform.localScale = actorScale.Scale;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace StoryTeller.Presentation
{
    public class ScenePresenter : MonoBehaviour
        , IScenePresenter
        , ISystemPresenter
        , IStoryContextInjectable
    {
        [SerializeField] RectTransform backgroundLayerParent = null;
        [SerializeField] RectTransform imageLayerParent = null;
        [SerializeField] Image overlayMask = null;
        [SerializeField] Animator cameraShakeAnimator = null;

        IAssetSource assetSource;
        IMessagePresenter msgPresenter;
        IStoryPlayer storyPlayer;

        Dictionary<string, List<string>> backgroundGroups = new Dictionary<string, List<string>>();
        Dictionary<string, GameObject> backgroundViews = new Dictionary<string, GameObject>();

        Dictionary<string, List<string>> imageGroups = new Dictionary<string, List<string>>();
        Dictionary<string, GameObject> imageViews = new Dictionary<string, GameObject>();

        Coroutine maskTransitionCoroutine = null;

        string activeTag = string.Empty;

        public void HideBackground(string tag)
        {
            if (backgroundGroups.ContainsKey(tag))
            {
                foreach (var viewNames in backgroundGroups[tag])
                {
                    var view = backgroundViews[viewNames];
                    view.SetActive(false);
                }
            }
            else if (backgroundViews.ContainsKey(tag))
            {
                backgroundViews[tag].SetActive(false);
            }
            activeTag = string.Empty;
        }

        public void Inject(IStoryContext context)
        {
            this.assetSource = context.AssetSource;
            this.msgPresenter = context.MessagePresenter;
            this.storyPlayer = context.StoryPlayer;
        }

        IEnumerator AlterMaskAlpha(float targetAlpha, float time, bool wait)
        {
            Color color = overlayMask.color;
            if (time >= float.Epsilon)
            {
                float elapsed = 0f;
                float origA = color.a;
                while(elapsed < time)
                {
                    elapsed += Time.deltaTime;
                    float lerpVal = elapsed / time;
                    color.a = Mathf.Lerp(origA, targetAlpha, lerpVal);
                    overlayMask.color = color;
                    yield return null;
                }
            }
            color.a = targetAlpha;
            overlayMask.color = color;
            yield return null;
            if(wait)
            {

            }
        }

        public void MaskOff(float time, bool wait = false)
        {
            if (maskTransitionCoroutine != null)
            {
                StopCoroutine(maskTransitionCoroutine);
            }
            maskTransitionCoroutine = StartCoroutine(AlterMaskAlpha(0f, time, wait));
            //throw new System.NotImplementedException();
        }

        public void MaskOn(float time, Color color, bool wait = false)
        {
            if(maskTransitionCoroutine != null)
            {
                StopCoroutine(maskTransitionCoroutine);
            }
            Color newColor = color;
            newColor.a = overlayMask.color.a;
            overlayMask.color = newColor;
            maskTransitionCoroutine = StartCoroutine(AlterMaskAlpha(color.a, time, wait));
            //throw new System.NotImplementedException();
        }

        public void RegisterBackgroundFromAssetSource(string name, string tag, string assetTag)
        {
            //throw new System.NotImplementedException();
            if(!backgroundGroups.ContainsKey(tag))
            {
                backgroundGroups[tag] = new List<string>();
            }
            backgroundGroups[tag].Add(name);
            var viewObject = Instantiate(assetSource.GetAsset<GameObject>(assetTag), backgroundLayerParent);
            backgroundViews[name] = viewObject;
            viewObject.SetActive(false);
        }

        public void RegisterBackgroundFromExternalSource(string name, string tag, string source)
        {
            //throw new System.NotImplementedException();
        }

        public void ShowBackground(string tag)
        {
            if(!string.IsNullOrEmpty(activeTag))
            {
                HideBackground(activeTag);
            }

            if (backgroundGroups.ContainsKey(tag))
            {
                foreach(var viewNames in backgroundGroups[tag])
                {
                    var view = backgroundViews[viewNames];
                    view.SetActive(true);
                }
            }
            else if(backgroundViews.ContainsKey(tag))
            {
                backgroundViews[tag].SetActive(true);
            }
            activeTag = tag;
        }

        public void RegisterImageFromAssetSource(string name, string tag, string asset)
        {
            if(!imageGroups.ContainsKey(tag))
            {
                imageGroups[tag] = new List<string>();
            }
            imageGroups[tag].Add(name);
            var sprite = assetSource.GetAsset<Sprite>(asset);
            var go = new GameObject();
            var image = go.AddComponent<Image>();
            image.sprite = sprite;
            imageViews[name] = go;
            go.transform.SetParent(imageLayerParent, false);
            go.SetActive(false);
        }

        public void ShowImage(string tagToUse)
        {
            if (imageGroups.ContainsKey(tagToUse))
            {
                foreach(var viewNames in imageGroups[tagToUse])
                {
                    var view = imageViews[viewNames];
                    view.SetActive(true);
                }
            }
            else if(imageViews.ContainsKey(tagToUse))
            {
                imageViews[tagToUse].SetActive(true);
            }
        }


        public void HideImage(string tagToUse)
        {
            if (imageGroups.ContainsKey(tagToUse))
            {
                foreach(var viewNames in imageGroups[tagToUse])
                {
                    var view = imageViews[viewNames];
                    view.SetActive(false);
                }
            }
            else if(imageViews.ContainsKey(tagToUse))
            {
                imageViews[tagToUse].SetActive(false);
            }
        }

        public void CameraShake(IMessagePresenter messagePresenter, bool wait)
        {
            if (wait)
            {
                StartCoroutine(CameraShakeCoroutine(messagePresenter));
            }
            else
            {
                cameraShakeAnimator.Play("CameraShake");
            }
        }

        IEnumerator CameraShakeCoroutine(IMessagePresenter messagePresenter)
        {
            messagePresenter.Interactable = false;
            cameraShakeAnimator.Play("CameraShake");
            while (cameraShakeAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
            {
                yield return null;
            }

            messagePresenter.Interactable = true;
        }

        public void Wait(float time)
        {
            msgPresenter.Interactable = false;
            StartCoroutine(SystemWait(time));
        }

        IEnumerator SystemWait(float time)
        {
            float elapsed = 0f;
            while (elapsed < time)
            {
                elapsed += Time.deltaTime;
                yield return null;
            }
            msgPresenter.Interactable = true;
            storyPlayer.Play();
        }
    }
}
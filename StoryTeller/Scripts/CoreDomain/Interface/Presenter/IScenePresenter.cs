using System.Collections;
using System.Collections.Generic;
using StoryTeller.Presentation;
using UnityEngine;

namespace StoryTeller
{
    public interface IScenePresenter
    {
        void RegisterBackgroundFromAssetSource(string name, string tag, string assetTag);
        void RegisterBackgroundFromExternalSource(string name, string tag, string source);
        void ShowBackground(string tag);
        void HideBackground(string tag);

        void RegisterImageFromAssetSource(string name, string tag, string asset);
        void ShowImage(string tagToUse);
        void HideImage(string tagToUse);

        void MaskOn(float time, Color color, bool wait = false);
        void MaskOff(float time, bool wait = false);

        void CameraShake(IMessagePresenter messagePresenter, bool wait);
    }
}
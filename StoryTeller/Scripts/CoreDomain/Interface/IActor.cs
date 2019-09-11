using System;
using UnityEngine;

namespace StoryTeller
{
    public interface IActor
    {
        Transform Transform { get; }
        bool Enabled { get; set; }
        void ShowActor(float destinations, float time, Action completion = null);
        void HideActor(float destinations, float time, Action completion = null);
        void ChangeFace(string faceId);
        void ChangePose(string poseId);
        void SetColor(Color color);
        void SetScale(Vector3 scale);
        void SetPosition(Vector3 position);
        void SetRotation(Vector3 rotation);
        void SetSize(string sizeKey, float time = 0);


        void TweenRegion(Region destination, float duration, Action onCompleted = null);

        void Shake();

        bool isPlayingShakeAnimation { get; }
        bool PlayingAnimationIsName(string name);
        void SpeakFocus(bool isSpeaker);
    }
}
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts.UI {
    [AddComponentMenu("UI/Sound Button", 170)]
    [Serializable]
    public class SoundButton : Button {
        public UISounds sounds;

        protected override void Start() {
            base.Start();
            sounds = GameObject.Find("MainCanvas").GetComponent<UISounds>();
        }

        public override void OnSelect(BaseEventData eventData) {
            base.OnSelect(eventData);
            if (!sounds.isPlaying())
                sounds.PlaySelect();
        }

        public override void OnSubmit(BaseEventData eventData) {
            base.OnSubmit(eventData);
            sounds.PlayClick();
        }
    }
}
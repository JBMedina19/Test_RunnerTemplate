using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace RunnerTemplate.Collectibles
{
    public class UIPopUp : MonoBehaviour
    {
        public float MoveYRelative;
        public float MoveDuration;
        public float FadeDuration;
        public CanvasGroup CanvasGroup;

        // Start is called before the first frame update
        void Start()
        {
            CanvasGroup.alpha = 1;
            transform.DOMoveY(MoveYRelative, MoveDuration).SetRelative(true);
            CanvasGroup.DOFade(0, FadeDuration);
        }
    }
}


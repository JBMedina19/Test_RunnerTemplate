using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RGVA;
using Sirenix.OdinInspector;
using TMPro;
using Dreamteck.Splines;
using UnityEngine.UI;

namespace RunnerTemplate.Progress
{
    public class ProgressManager : Singleton<ProgressManager>
    {
        [FoldoutGroup("References"), SerializeField, ReadOnly] private Slider m_Slider;
        [FoldoutGroup("References"), SerializeField] private SplineFollower m_PlayerSplineFollower;

        [Button]
        private void SetRefs()
        {
            m_Slider = transform.FindDeepChild<Slider>("Progress Slider");
        }

        private void Update()
        {
            m_Slider.value = (float)m_PlayerSplineFollower.result.percent;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RGVA;
using Sirenix.OdinInspector;
using TMPro;
using DG.Tweening;

namespace RunnerTemplate.Collectibles
{
    public class CollectibleManager : Singleton<CollectibleManager>
    {
        public delegate void CollectAmountEvent(int amount);
        public static event CollectAmountEvent OnCollectAmount = delegate { };
        public static event CollectAmountEvent OnSpendAmount = delegate { };

        [FoldoutGroup("Collectibles")] public int Collected;
        [FoldoutGroup("References"), SerializeField, ReadOnly] private TextMeshProUGUI m_CoinsText;
        [FoldoutGroup("References"), SerializeField, ReadOnly] private Canvas m_Canvas;
        [FoldoutGroup("References"), SerializeField] private GameObject m_CollectPopUp;
        [FoldoutGroup("References"), SerializeField] private GameObject m_SpendPopUp;

        [Button]
        private void SetRefs()
        {
            m_CoinsText = transform.FindDeepChild<TextMeshProUGUI>("Coins Text");
            m_Canvas = transform.FindDeepChild<Canvas>("CollectCanvas");
        }

        private void OnEnable()
        {
            Collected = 0;
            UpdateUI();
        }

        //called by collectibles in game (ex: money, coins)
        public void Collect(int i_Amount)
        {
            Collected += i_Amount;
            UpdateUI();
            OnCollectAmount(i_Amount);
        }

        //called when losing money (ex: buying from shop, bumping into obstacles)
        public void Spend(int i_Amount)
        {
            Collected -= i_Amount;
            UpdateUI();
            OnSpendAmount(i_Amount);
        }

        public void UpdateUI()
        {
            m_CoinsText.text = Collected.ToString();
            m_CoinsText.transform.DOPunchScale(Vector3.one, .25f);
        }

        public void SpawnPopUp(Vector3 i_WorldSpace, bool i_Collect)
        {
            Vector3 screenSpawn = Camera.main.WorldToScreenPoint(i_WorldSpace);
            GameObject popUp;
            if (i_Collect)
            {
                popUp = Instantiate(m_CollectPopUp, screenSpawn, Quaternion.identity, m_Canvas.transform);
            }
            else
            {
                popUp = Instantiate(m_SpendPopUp, screenSpawn, Quaternion.identity, m_Canvas.transform);
            }
        }
    }
}



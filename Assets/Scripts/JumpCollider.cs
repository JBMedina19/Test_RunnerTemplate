using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class JumpCollider : MonoBehaviour
{
    public static event Action OnJumpCollision;

    [SerializeField] private Transform m_LandingPos;
    [SerializeField] private Ease m_JumpEase;
    [SerializeField] private Ease m_LandEase;
    [SerializeField] private float m_JumpForce = 5f;
    [SerializeField] private float m_JumpDuration = 3f;

    private float m_DefaultY;

    private void OnTriggerEnter(Collider i_Other)
    {
        if (i_Other.CompareTag("Player"))
        {
            InvokeOnJumpCollision(i_Other);
        }
    }

    private void InvokeOnJumpCollision(Collider i_Other)
    {
        OnJumpCollision?.Invoke();

        m_DefaultY = i_Other.transform.position.y;
        i_Other.transform.DOLocalMoveY(m_JumpForce, m_JumpDuration).SetEase(m_JumpEase).OnComplete(() =>
        {
            i_Other.transform.DOLocalMoveY(m_DefaultY, m_JumpDuration).SetEase(m_LandEase);
        });
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorHelper : MonoBehaviour
{
    private PlayerMovement m_PlayerMovement;

    private void OnValidate()
    {
        m_PlayerMovement = GetComponentInParent<PlayerMovement>();
    }
}

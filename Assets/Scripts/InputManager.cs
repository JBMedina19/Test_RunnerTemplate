using UnityEngine;
using Sirenix.OdinInspector;
using System;
using RGVA;

public class InputManager : Singleton<InputManager>
{
    [ShowInInspector, ReadOnly] private ScreenData m_ScreenData;
    [SerializeField] private bool LogScreenData = false;

    public float DragSensitivity = 10;

    public bool IsInputDown { get { return m_IsInputDown; } }
    private bool m_IsInputDown;

    private Vector3 m_MousePos => Input.mousePosition * m_ScreenData.Scaling;

    private Vector3 m_LastInputPos;

    public Vector2 DeltaDrag { get { return m_DeltaDrag * DragSensitivity / m_ScreenData.FinalDPI * m_ScreenData.Scaling; } }
    private Vector2 m_DeltaDrag;

    public static Action<Vector2> OnInputDown;
    public static Action OnInputUp;
    public static Action<Vector2> OnDragDelta;
    public static Action<Vector2> OnDrag;

    protected override void OnAwakeEvent()
    {
        base.OnAwakeEvent();
        m_ScreenData.CalculateData(LogScreenData);
    }

    private void ResetValues()
    {

    }

    private void FixedUpdate()
    {
        m_DeltaDrag = m_MousePos - m_LastInputPos;

        if (m_DeltaDrag != Vector2.zero)
            OnDragDelta?.Invoke(m_DeltaDrag);

        m_LastInputPos = m_MousePos;
    }

    public void InputDown()
    {
        m_IsInputDown = true;
        ResetValues();

        OnInputDown?.Invoke(Input.mousePosition);
    }

    public void InputUp()
    {
        m_IsInputDown = false;
        ResetValues();

        OnInputUp?.Invoke();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Dreamteck.Splines;
using Sirenix.OdinInspector;

public class PlayerMovement : MonoBehaviour
{
    [TitleGroup("Movement Settings")]
    [SerializeField] private AnimationCurve m_JumpCurve;
    [SerializeField] private float m_ForwardMoveSpeed = 10;
    [SerializeField] private float m_SmoothMoveSpeed = 1;
    [SerializeField] private float m_SmoothRotSpeed = 1;
    [SerializeField] private float m_ClampX = 5;
    [SerializeField] private float m_TargetStartPosition = .039f;

    [TitleGroup("Other Settings")]
    [SerializeField] private bool m_UseJump = true;

    [FoldoutGroup("Object References"), SerializeField] private Transform m_PlayerModel;
    [FoldoutGroup("Object References"), SerializeField] private SplineFollower m_PlayerSplineFollower;
    [FoldoutGroup("Object References"), SerializeField] private SplineFollower m_TargetSplineFollower;
    [FoldoutGroup("Object References"), SerializeField] private Animator m_PlayerAnimator;
    [FoldoutGroup("Object References"), SerializeField] private Transform m_TargetGO;

    #region Cache
    private Vector3 m_Drag;
    private Vector3 m_TargetPos;
    private Vector3 m_ResetPos;
    private Vector3 m_JumpPos;
    private Vector3 m_TargetGoPos;
    private Vector3 m_TargetRot;
    private Vector3 m_ClampPos;
    private float m_CurveDeltaTime = .0f;
    #endregion

    [Button]
    private void SetRef()
    {
        m_PlayerModel = transform.GetChild(0);
        m_PlayerAnimator = GetComponentInChildren<Animator>();
        m_TargetSplineFollower = m_TargetGO.GetComponent<SplineFollower>();
        m_PlayerSplineFollower = GetComponent<SplineFollower>();

        m_TargetSplineFollower.startPosition = m_TargetStartPosition;
    }

    private void OnEnable()
    {
        JumpCollider.OnJumpCollision += OnJumpCollision;
    }
    private void OnDisable()
    {
        JumpCollider.OnJumpCollision -= OnJumpCollision;
    }

    private void OnJumpCollision()
    {

    }

    private void Update()
    {
        SplineMovement();

        if (m_UseJump)
        {
            Jump();
        }
    }
    private void FixedUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            Movement();
            Rotation();
        }
        else
        {
            ResetMovement();
            ResetRotation();
        }
    }

    private void Movement()
    {
        m_Drag = InputManager.Instance.DeltaDrag;
        m_Drag.y = 0;
        m_Drag.z = 0;

        m_TargetPos += m_Drag * InputManager.Instance.DragSensitivity;

        m_ClampPos = m_TargetPos;
        m_ClampPos.x = Mathf.Clamp(m_TargetPos.x, -m_ClampX, m_ClampX);
        m_TargetPos = m_ClampPos;
        m_TargetPos.y = m_PlayerModel.localPosition.y;

        m_PlayerModel.localPosition = Vector3.Lerp(m_PlayerModel.localPosition, m_TargetPos, m_SmoothMoveSpeed * Time.deltaTime);

        m_TargetGoPos = m_TargetPos;
        m_TargetGoPos.z = m_PlayerModel.position.z + m_TargetStartPosition;
        m_TargetGO.position = m_TargetGoPos;
    }
    private void Rotation()
    {
        m_TargetRot.x = Input.GetAxis("Mouse X") * m_SmoothRotSpeed;
        m_PlayerModel.localRotation = Quaternion.Lerp(m_PlayerModel.localRotation, Quaternion.LookRotation(m_TargetRot), m_SmoothRotSpeed * Time.deltaTime);
    }
    private void Jump()
    {
        m_CurveDeltaTime += Time.deltaTime;
        m_JumpPos.y = m_JumpCurve.Evaluate(m_CurveDeltaTime);
        m_PlayerAnimator.transform.localPosition = m_JumpPos;
    }
    private void ResetMovement()
    {
        m_ResetPos = m_PlayerModel.position;
        m_ResetPos.z = m_PlayerModel.position.z + m_TargetStartPosition;

        m_TargetGO.position = Vector3.Lerp(m_TargetGO.position, m_ResetPos, (m_SmoothMoveSpeed) * Time.deltaTime);
    }
    private void ResetRotation()
    {
        m_TargetRot.x = 0;
        m_PlayerModel.localRotation = Quaternion.Lerp(m_PlayerModel.localRotation, Quaternion.LookRotation(m_TargetRot), m_SmoothRotSpeed * Time.deltaTime);
    }
    private void SplineMovement()
    {
        m_PlayerSplineFollower.followSpeed = m_ForwardMoveSpeed;
        m_TargetSplineFollower.followSpeed = m_PlayerSplineFollower.followSpeed;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RunnerTemplate.Collectibles;
using Sirenix.OdinInspector;
using RGVA;
using DG.Tweening;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Mover))]
[RequireComponent(typeof(Rotator))]
[RequireComponent(typeof(Rigidbody))]
public class BasicCoin : Collectible_Base
{
    [FoldoutGroup("Collecting Settings")] public float CollectForce; // how much force to apply on this gameobject
    [FoldoutGroup("Collecting Settings")] public float DelayToCollect; // how long until this object start moving towards the collector
    [FoldoutGroup("Collecting Settings")] public float CollectingSpeed; // how fast will this object move
    [FoldoutGroup("Collecting Settings")] public float DistanceToCollect; // how close should this object be to the collector in order for it to disappear

    [FoldoutGroup("References"), ReadOnly, SerializeField] private Collider m_Collider;
    [FoldoutGroup("References"), ReadOnly, SerializeField] private Mover m_Mover;
    [FoldoutGroup("References"), ReadOnly, SerializeField] private Rotator m_Rotator;
    [FoldoutGroup("References"), ReadOnly, SerializeField] private Rigidbody m_Rigidbody;
    [FoldoutGroup("References"), ReadOnly, SerializeField] private Transform m_Collector;
    [FoldoutGroup("References"), Button] private void SetRefs()
    {
        m_Collider = GetComponent<Collider>();
        m_Mover = GetComponent<Mover>();
        m_Rotator = GetComponent<Rotator>();
        m_Rigidbody = GetComponent<Rigidbody>();
        m_Rigidbody.isKinematic = true;
    }

    public override void Collect()
    {
        base.Collect();
        CollectibleManager.Instance.SpawnPopUp(transform.position, true);

        m_Collider.enabled = false;

        //stop moving
        m_Mover.StopAnimation();
        m_Rotator.StopAnimation();

        StartCoroutine(Collecting());

    }

    private IEnumerator Collecting()
    {
        m_Rigidbody.isKinematic = false;
        m_Rigidbody.AddForce(Vector3.up * CollectForce, ForceMode.Impulse);

        //do a spin
        transform.DORotate(Vector3.up * 360f, 1f).SetRelative(true);
        
        yield return new WaitForSeconds(DelayToCollect);

        //scale down
        transform.DOScale(0f, .25f);

        while (Vector3.Distance(transform.position, m_Collector.position) > DistanceToCollect)
        {
            transform.position = Vector3.Lerp(transform.position, m_Collector.position, Time.deltaTime * CollectingSpeed);
            yield return null;
        }

        //queue if pooling
        transform.DOKill();
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            m_Collector = other.transform;
            Collect();
        }
    }

}

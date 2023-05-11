using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RunnerTemplate.Portals
{
    public class Portal_Base : MonoBehaviour
    {
        public virtual void Enter()
        {
            PortalsManager.Instance.Enter();
        }
    }
}

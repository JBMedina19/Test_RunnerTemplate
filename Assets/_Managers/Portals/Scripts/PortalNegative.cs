using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace RunnerTemplate.Portals
{
    public class PortalNegative : Portal_Base
    {
        private void OnTriggerEnter(Collider i_Other)
        {
            if (i_Other.CompareTag("Player"))
            {
                Enter();
            }
        }

        public override void Enter()
        {
            base.Enter();
        }
    }
}

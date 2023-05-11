using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RGVA;
using Sirenix.OdinInspector;
using TMPro;

namespace RunnerTemplate.Portals
{
    public class PortalsManager : Singleton<PortalsManager>
    {
        public delegate void PortalEnterEvent();
        public static event PortalEnterEvent OnPortalEnter = delegate { };

        public void Enter()
        {
            OnPortalEnter();
        }
    }
}

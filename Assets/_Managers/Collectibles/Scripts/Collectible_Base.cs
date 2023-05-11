using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RunnerTemplate.Collectibles
{
    //BASE CLASS THAT ALL COLLECTIBLES SHOULD INHERIT FROM
    public class Collectible_Base : MonoBehaviour
    {
        public int CollectAmount;

        //CALLED IN 'PLAYER-TO-OBJECT' INTERACTIONS (EX: COLLISION EVENTS)
        public virtual void Collect()
        {
            CollectibleManager.Instance.Collect(CollectAmount);
        }
    }
}


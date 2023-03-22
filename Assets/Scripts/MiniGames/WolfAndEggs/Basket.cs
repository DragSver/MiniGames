using System.Collections.Generic;
using UnityEngine;

namespace MiniGames.WolfAndEggs
{
    public class Basket : MonoBehaviour
    {
        [HideInInspector] public BasketStatus Status;
        private Dictionary<Vector3, BasketStatus> _statusMap;

        public void BasketMove(Vector3 vector3)
        {
            gameObject.transform.position = vector3;
        }
    }
}


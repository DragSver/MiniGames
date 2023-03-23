using DG.Tweening;
using UnityEngine;

namespace MiniGames.WolfAndEggs
{
    public class Basket : MonoBehaviour
    {
        [HideInInspector] public BasketStatus Status;

        public void BasketMove(Vector3 vector3)
        {
            transform.DOMove(vector3, 0.2f);
        }
    }
}


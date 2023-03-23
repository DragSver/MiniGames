using UnityEngine;

namespace MiniGames.WolfAndEggs
{
    public class SplinePoint : MonoBehaviour
    {
        public EggStatus EggStatus;
        public BasketStatus BasketStatus;
        public Vector3 Vector3;

        public void Start()
        {
            Vector3 = transform.position;
        }
    }
}
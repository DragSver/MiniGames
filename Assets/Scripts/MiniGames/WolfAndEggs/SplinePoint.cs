using UnityEngine;
using UnityEngine.Serialization;

namespace MiniGames.WolfAndEggs
{
    public class SplinePoint : MonoBehaviour
    {
        [FormerlySerializedAs("EggStatus")] public MoveStatus moveStatus;
        public BasketStatus BasketStatus;
        public Vector3 Vector3;

        public void Start()
        {
            Vector3 = transform.position;
        }
    }
}
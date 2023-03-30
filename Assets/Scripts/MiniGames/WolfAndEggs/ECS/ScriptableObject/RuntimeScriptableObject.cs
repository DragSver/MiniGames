using UnityEngine;

namespace MiniGames.WolfAndEggs.ECS.ScriptableObject
{
    [CreateAssetMenu(fileName = "RuntimeScriptableObject", menuName = "ScriptableObjects/RuntimeScriptableObject", order = 1)]
    public class RuntimeScriptableObject : UnityEngine.ScriptableObject
    {
        public float SpeedBasket;
        public float SpeedMove;
        public float SpawnInterval;
        public BasketStatus StartBasketStatus;
    }
}

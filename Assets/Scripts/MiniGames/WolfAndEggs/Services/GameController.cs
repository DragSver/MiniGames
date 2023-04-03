using System.Collections.Generic;
using MiniGames.WolfAndEggs.ECS.ScriptableObject;
using Unity.Plastic.Newtonsoft.Json.Serialization;
using UnityEngine;

namespace MiniGames.WolfAndEggs.Services
{
    public class GameController : MonoBehaviour
    {
        public Action<Vector3, BasketStatus> SendInputData;
        public Action SendInputPauseData;
        
        [HideInInspector] public UIController UIController;

        public GameObject StartBasketPosition;
        public List<Spline> ListSplinePoints;
        
        public RuntimeScriptableObject RuntimeScriptableObject;

        private void Start()
        {
            UIController = gameObject.GetComponent<UIController>();
        }

        public void SendPauseData()
        {
            SendInputPauseData?.Invoke();
        }
        
        public void SendBasketData(Vector3 position, BasketStatus status)
        {
            SendInputData?.Invoke(position, status);
        }
    }
}
using System.Collections.Generic;
using Leopotam.EcsLite;
using MiniGames.WolfAndEggs.ECS.ScriptableObject;
using Unity.Plastic.Newtonsoft.Json.Serialization;
using UnityEngine;

namespace MiniGames.WolfAndEggs.Services
{
    public class GameController : MonoBehaviour
    {
        public Action<Vector3, BasketStatus> SendInputData;
        [HideInInspector] public bool IsPause;
        
        [HideInInspector] public UIController UIController;

        public GameObject StartBasketPosition;
        public List<Spline> ListSplinePoints;
        
        public RuntimeScriptableObject RuntimeScriptableObject;
        
        public Points Points;
        public Lives Lives;

        private void Start()
        {
            UIController = gameObject.GetComponent<UIController>();
            
            Points = new Points(this);
            Lives = new Lives(this);

            IsPause = false;
        }

        public void SwitchPause()
        {
            IsPause = !IsPause;
            UIController.SwitchPauseButton();
        }
        
        public void EndGame()
        {
            SwitchPause();
        }

        public void SendBasketData(Vector3 position, BasketStatus status)
        {
            if (IsPause) return;
            SendInputData?.Invoke(position, status);
        }
    }
}
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace MiniGames.WolfAndEggs.Services
{
    public class GameController : MonoBehaviour
    {
        public Action<Vector3, BasketStatus> SendInputData;
        public Action SendInputPauseData;
        public Action SendInputNewGameData;

        public GameObject StartBasketPosition;
        public List<Spline> ListSplinePoints;

        public void SendNewGameData()
        {
            SendInputNewGameData?.Invoke();
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
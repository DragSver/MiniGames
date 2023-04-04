using System;
using System.Collections.Generic;
using UnityEngine;

namespace MiniGames.WolfAndEggs.Services
{
    public class GameController : MonoBehaviour
    {
        public Action<Vector3, BasketStatus> SendInputData;
        public Action SendInputPauseData;

        public GameObject StartBasketPosition;
        public List<Spline> ListSplinePoints;

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
using MiniGames.WolfAndEggs.Services;
using UnityEngine;
using UnityEngine.Serialization;

namespace MiniGames.WolfAndEggs
{
    public class MoveButton : MonoBehaviour
    {
        [SerializeField] private GameController _gameController;
        [SerializeField] private CatchZone _catchZone;

        public void BasketMove()
        {
            _gameController.Basket.BasketMove(_catchZone.transform.position);
        }
        
    }
}
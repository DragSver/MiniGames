using MiniGames.WolfAndEggs.Services;
using UnityEngine;

namespace MiniGames.WolfAndEggs
{
    public class MoveButton : MonoBehaviour
    {
        [SerializeField] private GameController _gameController;
        [SerializeField] private CatchZone _catchZone;
        [SerializeField] private BasketStatus _basketStatus;

        public void BasketMove()
        {
            _gameController.Basket.Status = _basketStatus;
            _gameController.Basket.BasketMove(_catchZone.transform.position);
        }
        
    }
}
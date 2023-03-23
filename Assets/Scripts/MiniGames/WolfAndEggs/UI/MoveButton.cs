using MiniGames.WolfAndEggs.Services;
using UnityEngine;

namespace MiniGames.WolfAndEggs.UI
{
    public class MoveButton : MonoBehaviour
    {
        [SerializeField] private GameController _gameController;
        [SerializeField] private BasketStatus _basketStatus;
        [SerializeField] private GameObject _basketPosition;

        public void BasketMove()
        {
            _gameController.Basket.Status = _basketStatus;
            _gameController.Basket.BasketMove(_basketPosition.transform.position);
        }
        
    }
}
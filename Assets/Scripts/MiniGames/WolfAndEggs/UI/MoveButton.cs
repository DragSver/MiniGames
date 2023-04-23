using MiniGames.WolfAndEggs.Services;
using UnityEngine;
using Zenject;

namespace MiniGames.WolfAndEggs.UI
{
    public class MoveButton : MonoBehaviour
    {
        [Inject] private GameController _gameController;
        [SerializeField] private BasketStatus _basketStatus;
        [SerializeField] private GameObject _basketPosition;

        public void BasketMove()
        {
            _gameController.SendBasketData(_basketPosition.transform.position, _basketStatus);
        }
    }
}
using MiniGames.WolfAndEggs.Services;
using UnityEngine;
using Zenject;

namespace MiniGames.WolfAndEggs.UI
{
    public class NewGameButton : MonoBehaviour
    {
        [Inject] private GameController _gameController;

        public void NewGame()
        {
            _gameController.SendNewGameData();
        }
    }
}
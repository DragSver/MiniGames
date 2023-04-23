using MiniGames.WolfAndEggs.Services;
using UnityEngine;
using Zenject;

namespace MiniGames.WolfAndEggs.UI
{
    public class PauseButton : MonoBehaviour
    {
        [Inject] private GameController _gameController;

        public void Pause()
        {
            _gameController.SendPauseData();
        }
    }
}
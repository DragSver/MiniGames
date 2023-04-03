using MiniGames.WolfAndEggs.Services;
using UnityEngine;

namespace MiniGames.WolfAndEggs.UI
{
    public class PauseButton : MonoBehaviour
    {
        [SerializeField] private GameController _gameController;

        public void Pause()
        {
            _gameController.SendPauseData();
        }
    }
}
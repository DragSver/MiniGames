using MiniGames.WolfAndEggs.Services;

namespace MiniGames.WolfAndEggs
{
    public class Lives
    {
        private readonly GameController _gameController;

        private byte _countLives;

        public Lives(GameController gameController)
        {
            _gameController = gameController;
            _countLives = 3;
        }

        public void LostLive()
        {
            _gameController.UIController.LoseLife(--_countLives);
            
            if (_countLives<1) _gameController.EndGame();
        }
    }
}
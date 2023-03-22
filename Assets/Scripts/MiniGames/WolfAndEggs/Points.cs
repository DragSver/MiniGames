using System;
using MiniGames.WolfAndEggs.Services;

namespace MiniGames.WolfAndEggs
{
    public class Points
    {
        private readonly GameController _gameController;
        
        public int Point { get; private set; }

        public Points(GameController gameController)
        {
            _gameController = gameController;
            
            Point = 0;
        }
        
        public void Add(int points)
        {
            Point += points>=0 ? points : 0;
            PointsUpdate();
        }

        private void PointsUpdate()
        {
            _gameController.UIController.PointsUpdate(Point);
        }
    }
}
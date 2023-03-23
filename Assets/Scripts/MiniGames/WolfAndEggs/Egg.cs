using MiniGames.WolfAndEggs.Services;
using UnityEngine;

namespace MiniGames.WolfAndEggs
{
    public class Egg : MonoBehaviour
    {
        private GameController _gameController;

        [HideInInspector] public EggStatus Status;
        private SpawnEggs _spawnEggs;
        private Vector3 _endPosition;
        private byte _numTrajectories;
        
        public Animator Animator;

        public void Initialize(GameController gameController, SpawnEggs spawnEggs)
        {
            _gameController = gameController;
            _spawnEggs = spawnEggs;
            
            _numTrajectories = 0;
            Status = EggStatus.RollingDown;
            UpdateEndPosition(_spawnEggs.Vector3[_numTrajectories]);
        }

        public void RollingDown(float speed)
        {
            transform.position = Vector3.MoveTowards(transform.position, _endPosition, speed);

            if (transform.position != _endPosition) return;
            
            if (++_numTrajectories == _spawnEggs.Vector3.Count)
            {
                UpdateEndPosition(new Vector3(0, -0.4f, 0));
                Status = EggStatus.CanCatch;
            }
            else
                UpdateEndPosition(_spawnEggs.Vector3[_numTrajectories]);
        }

        public void CanCatch(float speed)
        {
            transform.position = Vector3.MoveTowards(transform.position, _endPosition, speed);
            
            if (_gameController.Basket.Status == _spawnEggs.BasketStatus)
                Catch();
            
            if (transform.position == _endPosition)
                Status = EggStatus.Fall;
        }

        public void Fall(float speed)
        {
            var endPosition = new Vector3(transform.position.x, -1.38f, 0);
            transform.position = Vector3.MoveTowards(transform.position, endPosition, speed);
                
            if (transform.position == endPosition)
                Break();
        }
        
        private void Catch()
        {
            _gameController.Points.Add(10);
            Status = EggStatus.Destroy;
        }

        private void Break()
        {
            _gameController.Lives.LostLive();
            Status = EggStatus.Destroy;
        }
        
        private void UpdateEndPosition(Vector3 toVector3)
        {
            _endPosition = new Vector3(transform.position.x + toVector3.x, transform.position.y + toVector3.y, 0);
        }
    }
}
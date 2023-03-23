using System.Collections.Generic;
using MiniGames.WolfAndEggs.Services;
using UnityEngine;

namespace MiniGames.WolfAndEggs
{
    public class Egg : MonoBehaviour
    {
        private GameController _gameController;

        private int _numberSplinePoint;
        private List<SplinePoint> _splinePoints;
        private Vector3 _endPosition;
        [SerializeField] private List<Animation> _animations;

        public EggStatus Status;

        public Animator Animator;

        public void Initialize(GameController gameController, List<SplinePoint> splinePoints)
        {
            _gameController = gameController;
            _splinePoints = splinePoints;
            _numberSplinePoint = -1;
            NextSplinePoint();
        }

        public void Move(float speed)
        {
            transform.position = Vector3.MoveTowards(transform.position, _endPosition, speed);
            
            switch (Status)
            {
                case EggStatus.RollingDown:
                    break;
                case EggStatus.CanCatch:
                    CanCatch();
                    break;
                case EggStatus.Fall:
                    Fall();
                    break;
            }
            
            if (transform.position != _endPosition) return;
            NextSplinePoint();
        }

        private void CanCatch()
        {
            if (_gameController.Basket.Status == _splinePoints[_numberSplinePoint].BasketStatus)
                Catch();
        }

        private void Fall()
        {
            if (transform.position == _endPosition) _gameController.Lives.LostLive();
        }

        private void Catch()
        {
            _gameController.Points.Add(10);
            Status = EggStatus.Destroy;
        }

        private void NextSplinePoint()
        {
            ++_numberSplinePoint;
            Status = _splinePoints[_numberSplinePoint].EggStatus;
            if (Status == EggStatus.Destroy) return;
            _endPosition = _splinePoints[_numberSplinePoint + 1].Vector3;
            switch (Status)
            {
                case EggStatus.RollingDown:
                    if (_endPosition.x < transform.position.x)
                    {
                        Animator.SetInteger("Int", 0);
                    }
                    else if (_endPosition.x > transform.position.x)
                    {
                        Animator.SetInteger("Int", 1);
                    }
                    else 
                        Animator.SetInteger("Int", 2);
                    break;
                case EggStatus.CanCatch:
                    Animator.SetInteger("Int", 2);
                    break;
                case EggStatus.Fall:
                    Animator.SetInteger("Int", -1);
                    break;
            }
        }
    }
}
using System.Collections.Generic;
using UnityEngine;

namespace MiniGames.WolfAndEggs.Services
{
    public class SpawnEggsController : MonoBehaviour
    {
        private float _spawnInterval;
        private GameObject _eggPrefab;
        private System.Random _random;
        private GameController _gameController;

        [SerializeField] private List<Spline> _listSplinePoints;

        public void Initialize(GameController gameController)
        {
            _gameController = gameController;
            
            _spawnInterval = 3;
            _random = new System.Random();
            _eggPrefab = Resources.Load<GameObject>("Prefabs/Egg");
        }

        private void Start()
        {
            InvokeRepeating("SpawnEgg", GetSpawnInterval(), GetSpawnInterval());
        }

        private void SpawnEgg()
        {
            if (_gameController.IsPause) return;

            var numberSpawnPlace = _random.Next(0, _listSplinePoints.Count);
            var spawnPlace = _listSplinePoints[numberSpawnPlace];

            var eggGO = Instantiate(_eggPrefab, spawnPlace.SplinePoints[0].Vector3, Quaternion.identity);
            _gameController.MoveEggsController.Eggs.Add(eggGO.GetComponent<Egg>());
            _gameController.MoveEggsController.Eggs[^1].Initialize(_gameController, spawnPlace.SplinePoints);
        }

        private float GetSpawnInterval()
        {
            var point = _gameController.Points.Point/10;
            return _spawnInterval - (point > 0.1f ? point : 0.1f);
        }
    }
}
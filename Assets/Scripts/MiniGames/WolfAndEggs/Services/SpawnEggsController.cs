using UnityEngine;

namespace MiniGames.WolfAndEggs.Services
{
    public class SpawnEggsController : MonoBehaviour
    {
        private System.Random _random;
        private GameController _gameController;
        private float _spawnInterval;
        private GameObject _eggPrefab;
        
        [SerializeField] public SpawnEggs[] SpawnPlaces;

        public void Initialize(GameController gameController)
        {
            _gameController = gameController;
            
            _spawnInterval = 3;
            _random = new System.Random();
            _eggPrefab = Resources.Load<GameObject>("Prefabs/Egg");
        }

        private void Start()
        {
            InvokeRepeating("SpawnEgg", 1-_gameController.Points.Point/100, GetSpawnInterval());
        }

        private void SpawnEgg()
        {
            if (_gameController.IsPause) return;
            var spawnEgg = SpawnPlaces[_random.Next(0, SpawnPlaces.Length)];

            var eggGO = Instantiate(_eggPrefab, spawnEgg.transform.position, Quaternion.identity);
            _gameController.MoveEggsController.Eggs.Add(eggGO.GetComponent<Egg>());
            _gameController.MoveEggsController.Eggs[^1].Initialize(_gameController, spawnEgg);
        }

        private float GetSpawnInterval()
        {
            var point = _gameController.Points.Point/10;
            return _spawnInterval - (point > 0.1f ? point : 0.1f);
        }
    }
}
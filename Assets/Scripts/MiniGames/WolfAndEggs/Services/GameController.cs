using UnityEngine;

namespace MiniGames.WolfAndEggs.Services
{
    public class GameController : MonoBehaviour
    {
        [HideInInspector] public UIController UIController;
        [HideInInspector] public MoveEggsController MoveEggsController;
        [HideInInspector] public SpawnEggController SpawnEggController;
        
        [HideInInspector] public Basket Basket;
        private GameObject _basketPrefab;
        [SerializeField] private CatchZone _startCatchZone;

        public Points Points;
        public Lives Lives;

        public void Start()
        {
            UIController = gameObject.GetComponent<UIController>();
            MoveEggsController = gameObject.GetComponent<MoveEggsController>();
            SpawnEggController = gameObject.GetComponent<SpawnEggController>();
            
            MoveEggsController.Initialize(this);
            SpawnEggController.Initialize(this);
            
            Points = new Points(this);
            Lives = new Lives(this);
            
            _basketPrefab = Resources.Load<GameObject>("Prefabs/Basket");
            
            Basket = Instantiate(_basketPrefab, _startCatchZone.gameObject.transform.position, Quaternion.identity)
                .GetComponent<Basket>();
            
        }

        public void EndGame()
        {
            Time.timeScale = 0;
        }
    }
}
using UnityEngine;
using UnityEngine.Serialization;

namespace MiniGames.WolfAndEggs.Services
{
    public class GameController : MonoBehaviour
    {
        [HideInInspector] public UIController UIController;
        [HideInInspector] public MoveEggsController MoveEggsController;
        [HideInInspector] public SpawnEggsController SpawnEggsController;

        [HideInInspector] public bool IsPause;
        
        [HideInInspector] public Basket Basket;
        private GameObject _basketPrefab;
        [SerializeField] private CatchZone _startCatchZone;
        

        public Points Points;
        public Lives Lives;

        public void Start()
        {
            UIController = gameObject.GetComponent<UIController>();
            MoveEggsController = gameObject.GetComponent<MoveEggsController>();
            SpawnEggsController = gameObject.GetComponent<SpawnEggsController>();
            
            MoveEggsController.Initialize(this);
            SpawnEggsController.Initialize(this);
            
            Points = new Points(this);
            Lives = new Lives(this);

            IsPause = false;
            
            _basketPrefab = Resources.Load<GameObject>("Prefabs/Basket");
            
            Basket = Instantiate(_basketPrefab, _startCatchZone.gameObject.transform.position, Quaternion.identity)
                .GetComponent<Basket>();
            
        }

        public void SwitchPause()
        {
            if (IsPause)
            {
                SpawnEggsController.enabled = true;
                MoveEggsController.enabled = true;
                foreach (var egg in MoveEggsController.Eggs)
                    egg.Animator.enabled = true;
            }
            else
            {
                SpawnEggsController.enabled = false;
                MoveEggsController.enabled = false;
                foreach (var egg in MoveEggsController.Eggs)
                    egg.Animator.enabled = false;
            }
            IsPause = !IsPause;
        }
        
        public void EndGame()
        {
            SwitchPause();
        }
    }
}
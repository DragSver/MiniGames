using UnityEngine;

namespace MiniGames.WolfAndEggs.Services
{
    public class GameController : MonoBehaviour
    {
        [HideInInspector] public bool IsPause;
        
        [HideInInspector] public UIController UIController;
        [HideInInspector] public MoveEggsController MoveEggsController;
        [HideInInspector] public SpawnEggsController SpawnEggsController;
        
        [HideInInspector] public Basket Basket;
        [SerializeField] private GameObject _startBasketPosition;
        
        public Points Points;
        public Lives Lives;

        private void Start()
        {
            UIController = gameObject.GetComponent<UIController>();
            MoveEggsController = gameObject.GetComponent<MoveEggsController>();
            SpawnEggsController = gameObject.GetComponent<SpawnEggsController>();
            
            MoveEggsController.Initialize(this);
            SpawnEggsController.Initialize(this);
            
            Points = new Points(this);
            Lives = new Lives(this);

            IsPause = false;

            Basket = Instantiate(Resources.Load<GameObject>("Prefabs/Basket"),
                    _startBasketPosition.transform.position, Quaternion.identity).GetComponent<Basket>();
            
        }

        public void SwitchPause()
        {
            SpawnEggsController.enabled = IsPause;
            MoveEggsController.enabled = IsPause;
            IsPause = !IsPause;
            UIController.SwitchPauseButton();
            foreach (var egg in MoveEggsController.Eggs)
                egg.Animator.enabled = !egg.Animator.enabled;
        }
        
        public void EndGame()
        {
            SwitchPause();
        }
    }
}
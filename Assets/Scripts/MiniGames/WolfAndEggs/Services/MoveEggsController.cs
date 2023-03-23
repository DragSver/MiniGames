using System.Collections.Generic;
using UnityEngine;

namespace MiniGames.WolfAndEggs.Services
{
    public class MoveEggsController : MonoBehaviour
    {
        private GameController _gameController;
        
        [HideInInspector] public List<Egg> Eggs;
        [SerializeField] private float Speed;
        
        public void Initialize(GameController gameController)
        {
            _gameController = gameController;
            
            Eggs = new List<Egg>();
            Speed = 0.5f;
        }
        
        public void Update()
        {
            for (var index = 0; index < Eggs.Count; index++)
            {
                var egg = Eggs[index];
                switch (egg.Status)
                {
                    case EggStatus.Destroy:
                        Eggs.RemoveAt(index--);
                        Destroy(egg.gameObject);
                        break;
                    default:
                        egg.Move(GetSpeed() * Time.deltaTime);
                        break;
                }
            }
        }

        private float GetSpeed()
        {
            return _gameController.Points.Point / 500 + Speed;
        }
    }
}
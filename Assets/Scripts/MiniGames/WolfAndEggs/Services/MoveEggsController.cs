using System;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

namespace MiniGames.WolfAndEggs.Services
{
    public class MoveEggsController : MonoBehaviour
    {
        private GameController _gameController;
        
        [HideInInspector] public List<Egg> Eggs;

        public void Initialize(GameController gameController)
        {
            _gameController = gameController;
            
            Eggs = new List<Egg>();
        }
        
        public void Move(Egg egg)
        {
            Vector3 vector3;

            var sequence = DOTween.Sequence();

            foreach (var trajectory in egg.SpawnEggs.Trajectories)
            {
                sequence.AppendCallback(() =>
                    {
                        vector3 = new Vector3(egg.transform.position.x+trajectory.Vector3.x, 
                            egg.transform.position.y+trajectory.Vector3.y, 0);
                        
                        egg.transform.DOMove(vector3, trajectory.Speed).SetEase(Ease.Linear);
                    })
                    .AppendInterval(trajectory.Speed);
            }
        }
    }
}
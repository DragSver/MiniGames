using DG.Tweening;
using MiniGames.WolfAndEggs.Services;
using UnityEngine;
using UnityEngine.Serialization;

namespace MiniGames.WolfAndEggs
{
    public class Egg : MonoBehaviour
    {
        private GameController _gameController;

        [HideInInspector] public bool Catched;
        [HideInInspector] public SpawnEggs SpawnEggs;
        [HideInInspector] public Animator Animator;
        [HideInInspector] public Collider2D Collider;

        public void Initialize(GameController gameController, SpawnEggs spawnEggs)
        {
            _gameController = gameController;
            SpawnEggs = spawnEggs;
            
            Animator = gameObject.GetComponent<Animator>();
            Collider = gameObject.GetComponent<Collider2D>();
            
            _gameController.MoveEggsController.Move(this);
        }
        
        public void Catch()
        {
            _gameController.Points.Add(10);
            Catched = true;
            Destroy(gameObject);
        }

        public void Break()
        {
            Animator.enabled = false;
            Collider.enabled = false;
            var sequence = DOTween.Sequence();
            Vector3 vector3;
            sequence.AppendCallback(() =>
                {
                    vector3 = new Vector3(transform.position.x, -1.38f, 0);
                    transform.DOMove(vector3, 0.5f).SetEase(Ease.Linear);
                })
                .AppendInterval(0.5f)
                .AppendCallback(_gameController.Lives.LostLive)
                .AppendInterval(2f)
                .AppendCallback(() =>
                {
                    Destroy(gameObject);
                });
        }
    }
}
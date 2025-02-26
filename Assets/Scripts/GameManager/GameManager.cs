
using UnityEngine;

namespace ShootEmUp
{
    public sealed class GameManager : MonoBehaviour, IStartGameListener, IFinishGameListener
    {
        private CharacterController _characterController;
        
        public void OnStartGame()
        {
            _characterController = FindObjectOfType<CharacterController>();
            _characterController.HitPointsComponent.OnHpEmptyEvent += FinishGame;
        }

        public void OnFinishGame()
        {
            _characterController.HitPointsComponent.OnHpEmptyEvent -= FinishGame;
        }

        private void FinishGame(GameObject gameObject)
        {
            GameStateController.Instance.FinishGame();
        }
    }
}
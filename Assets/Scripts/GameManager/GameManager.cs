using UnityEngine;

namespace ShootEmUp
{
    
public sealed class GameManager : MonoBehaviour
    {
        private CharacterController _characterController;
        
        private void Start()
        {
            StartGame();
        }
        private void OnDestroy()
        {
            ExitGame();
        }
        
        private void StartGame()
        {
            _characterController = FindObjectOfType<CharacterController>();
            _characterController.HitPointsComponent.OnHpEmptyEvent += FinishGame;
        }
        
        public void ExitGame()
        {
            _characterController.HitPointsComponent.OnHpEmptyEvent -= FinishGame;
        }
        
        private void FinishGame(GameObject gameObject)
        {
            Debug.Log("Game finished");
            Time.timeScale = 0;
        }
    }
}
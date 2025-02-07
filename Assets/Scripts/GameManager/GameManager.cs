using System;
using UnityEngine;

namespace ShootEmUp
{
    
    public sealed class GameManager : MonoBehaviour
    {
        private CharacterController characterController;

        private void Awake()
        {
            characterController = FindObjectOfType<CharacterController>();
        }

        private void OnEnable()
        {
            characterController.HitPointsComponent.hpEmpty += FinishGame;
        }

        private void OnDisable()
        {
            characterController.HitPointsComponent.hpEmpty -= FinishGame;
        }
        
        private void FinishGame(GameObject gameObject)
        {
            Debug.Log("Game over!");
            Time.timeScale = 0;
        }
    }
}
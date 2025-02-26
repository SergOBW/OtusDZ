using UnityEngine;

namespace ShootEmUp
{
    [RequireComponent(typeof(HitPointsComponent))]
    [RequireComponent(typeof(WeaponComponent))]
    public sealed class CharacterController : MonoBehaviour, ITarget
    {
        public HitPointsComponent HitPointsComponent { get; private set; }
        public WeaponComponent WeaponComponent { get; private set; }
        
        private InputManager _inputManager;

        private void Awake()
        {
            HitPointsComponent = GetComponent<HitPointsComponent>();
            WeaponComponent = GetComponent<WeaponComponent>();
            _inputManager = FindObjectOfType<InputManager>();
        }
        private void Update()
        {
            if (!_inputManager  || !WeaponComponent )
            {
                return;
            }

            if (_inputManager.IsFireButtonDown)
            {
                WeaponComponent.Fire();
            }
        }
        

        public bool IsPlayer()
        {
            return true;
        }

        public Transform GetTransform()
        {
            return transform;
        }
    }
}
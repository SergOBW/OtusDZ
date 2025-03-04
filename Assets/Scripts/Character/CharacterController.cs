using UnityEngine;
using VContainer;

namespace ShootEmUp
{
    [RequireComponent(typeof(HitPointsComponent))]
    [RequireComponent(typeof(WeaponComponent))]
    public sealed class CharacterController : MonoBehaviour, ITarget, IUpdate, IFinishGameListener, IStartGameListener
    {
        public HitPointsComponent HitPointsComponent { get; private set; }
        public WeaponComponent WeaponComponent { get; private set; }
        
        private InputManager _inputManager;

        private Vector3 _startedPosition;
        
        [Inject]
        public void Construct(InputManager inputManager)
        {
            HitPointsComponent = GetComponent<HitPointsComponent>();
            WeaponComponent = GetComponent<WeaponComponent>();
            _inputManager = inputManager;
            _startedPosition = transform.position;
        }
        public void CustomUpdate()
        {
            if (_inputManager == null  || !WeaponComponent )
            {
                return;
            }

            if (_inputManager.IsFireButtonDown)
            {
                WeaponComponent.Fire();
            }
        }

        public Transform GetTransform()
        {
            return transform;
        }

        public void OnFinishGame()
        {
            transform.position = _startedPosition;
        }

        public void OnStartGame()
        {
            HitPointsComponent.SetDefaults();
        }
    }
    

    public interface ITarget
    {
        public Transform GetTransform();
    }
}
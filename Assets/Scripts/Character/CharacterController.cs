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
        
        private Vector3 _startedPosition;
        
        private InputManager _inputManager;
        private EventBus _eventBus;
        private BulletSystem _bulletSystem;
        
        [Inject]
        public void Construct(InputManager inputManager, EventBus eventBus, BulletSystem bulletSystem)
        {
            _inputManager = inputManager;
            _eventBus = eventBus;
            _bulletSystem = bulletSystem;
            _startedPosition = transform.position;
            
            HitPointsComponent = GetComponent<HitPointsComponent>();
            HitPointsComponent.OnHpEmptyEvent += OnHpEmpty;
            WeaponComponent = GetComponent<WeaponComponent>();
            WeaponComponent.Initialize(_bulletSystem);
        }

        private void OnHpEmpty(GameObject obj)
        {
            _eventBus.Publish(new PlayerDiedEvent());
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
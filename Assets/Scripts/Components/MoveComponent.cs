using UnityEngine;
using VContainer;

namespace ShootEmUp
{
    public sealed class MoveComponent : MonoBehaviour, IFixedUpdate
    {
        [SerializeField]
        private new Rigidbody2D rigidbody2D;

        [SerializeField]
        private float speed = 5.0f;

        private InputManager _inputManager;
        private ITeam _team;

        [Inject]
        public void Construct(InputManager inputManager)
        {
            _inputManager = inputManager;
            _team = GetComponent<ITeam>();
        }
        
        public void MoveByRigidbodyVelocity(Vector2 vector)
        {
            var nextPosition = rigidbody2D.position + vector * speed;
            rigidbody2D.MovePosition(nextPosition);
        }
        public void CustomFixedUpdate()
        {
            if (_inputManager == null || _team is null)
            {
                return;
            }
            
            if (_team.IsPlayer())
            {
                MoveByRigidbodyVelocity(new Vector2(_inputManager.HorizontalDirection, 0) * Time.fixedDeltaTime);
            }
        }
    }
}
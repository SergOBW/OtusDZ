using UnityEngine;

namespace ShootEmUp
{
    [RequireComponent(typeof(HitPointsComponent))]
    [RequireComponent(typeof(WeaponComponent))]
    public sealed class CharacterController : MonoBehaviour, ITeam
    {
        public HitPointsComponent HitPointsComponent { get; private set; }
        public WeaponComponent WeaponComponent { get; private set; }

        private void Awake()
        {
            HitPointsComponent = GetComponent<HitPointsComponent>();
            WeaponComponent = GetComponent<WeaponComponent>();
        }

        public bool IsPlayer()
        {
            return true;
        }
    }

    public interface ITeam
    {
        public bool IsPlayer();
    }
}
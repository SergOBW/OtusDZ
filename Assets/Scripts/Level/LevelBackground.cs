using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace ShootEmUp
{
    public sealed class LevelBackground : MonoBehaviour , IFixedUpdate
    {
        private float _startPositionY;

        private float _endPositionY;

        private float _movingSpeedY;

        private float _positionX;

        private float _positionZ;

        private Transform _myTransform;

        [FormerlySerializedAs("m_params")] [SerializeField]
        private Params @params;

        private void Awake()
        {
            _startPositionY = @params.StartPositionY;
            _endPositionY = @params.EndPositionY;
            _movingSpeedY = @params.MovingSpeedY;
            _myTransform = transform;
            var position = _myTransform.position;
            _positionX = position.x;
            _positionZ = position.z;
        }

        public void CustomFixedUpdate()
        {
            if (_myTransform.position.y <= _endPositionY)
            {
                _myTransform.position = new Vector3(
                    _positionX,
                    _startPositionY,
                    _positionZ
                );
            }

            _myTransform.position -= new Vector3(
                _positionX,
                _movingSpeedY * Time.fixedDeltaTime,
                _positionZ
            );
        }

        [Serializable]
        public sealed class Params
        {
            [FormerlySerializedAs("m_startPositionY")] [SerializeField]
            public float StartPositionY;

            [FormerlySerializedAs("m_endPositionY")] [SerializeField]
            public float EndPositionY;

            [FormerlySerializedAs("m_movingSpeedY")] [SerializeField]
            public float MovingSpeedY;
        }
    }
}
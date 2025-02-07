using UnityEngine;

namespace ShootEmUp
{
    public sealed class InputManager : MonoBehaviour
    {
        public float HorizontalDirection { get; private set; }
        public bool IsFireButtonDown { get; private set; }
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                IsFireButtonDown = true;
            }
            else
            {
                IsFireButtonDown = false;
            }

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                HorizontalDirection = -1;
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                HorizontalDirection = 1;
            }
            else
            {
                HorizontalDirection = 0;
            }
        }
    }
}
using UnityEngine;

namespace ShootEmUp
{
    //TODO: Make it in new input system
    public interface IInputManager
    {
        float HorizontalDirection { get; }
        bool IsFireButtonDown { get; }
    }
    
    public sealed class InputManager : MonoBehaviour,IInputManager , IUpdate
    {
        public float HorizontalDirection { get; private set; }
        public bool IsFireButtonDown { get; private set; }
        
        public void CustomUpdate()
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
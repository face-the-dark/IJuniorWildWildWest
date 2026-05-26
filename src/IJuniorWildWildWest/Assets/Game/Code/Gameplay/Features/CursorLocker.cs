using UnityEngine;

namespace Game.Gameplay.Features
{
    public class CursorLocker : MonoBehaviour
    {
        private void Start() => 
            Cursor.lockState = CursorLockMode.Locked;
    }
}
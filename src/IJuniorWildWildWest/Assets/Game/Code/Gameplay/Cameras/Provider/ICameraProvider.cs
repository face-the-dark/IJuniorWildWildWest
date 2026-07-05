using UnityEngine;

namespace Game.Gameplay.Cameras.Provider
{
    public interface ICameraProvider
    {
        Camera MainCamera { get; set; }
    }
}
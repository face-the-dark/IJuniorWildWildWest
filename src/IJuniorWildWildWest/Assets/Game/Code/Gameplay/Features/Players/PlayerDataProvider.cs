using Game.Gameplay.Cameras;

namespace Game.Gameplay.Features.Players
{
    public class PlayerDataProvider
    {
        public Player Player { get; set; }
        public PlayerCameraInfo PlayerCameraInfo { get; set; }
        public PlayerCamera PlayerCamera { get; set; }
        public Weapon Weapon { get; set; }
        public LookTarget LookTarget { get; set; }
    }
}
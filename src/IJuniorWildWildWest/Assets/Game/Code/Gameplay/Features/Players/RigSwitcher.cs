using UnityEngine;
using UnityEngine.Animations.Rigging;
using VContainer;

namespace Game.Gameplay.Features.Players
{
    [RequireComponent(typeof(RigBuilder))]
    public class RigSwitcher : MonoBehaviour
    {
        private const float RigOffValue = 0f;
        private const float RigOnValue = 1f;

        [SerializeField] private Rig _idleRig;
        [SerializeField] private Rig _aimRig;

        [SerializeField] private MultiAimConstraint _aimRigBody;
        [SerializeField] private MultiAimConstraint _aimRigRightHand;

        private bool _isAimed;

        [Inject]
        public void Construct(PlayerDataProvider playerDataProvider)
        {
            _aimRigBody.data.sourceObjects = new WeightedTransformArray
                { new WeightedTransform(playerDataProvider.LookTarget.transform, 1f) };
            _aimRigRightHand.data.sourceObjects = new WeightedTransformArray
                { new WeightedTransform(playerDataProvider.LookTarget.transform, 1f) };

            GetComponent<RigBuilder>().Build();
        }

        public void UpdateAim(bool isAimed)
        {
            _isAimed = isAimed;

            UpdateRigsValues();
        }

        private void UpdateRigsValues()
        {
            if (_isAimed)
            {
                _idleRig.weight = RigOffValue;
                _aimRig.weight = RigOnValue;
            }
            else
            {
                _idleRig.weight = RigOnValue;
                _aimRig.weight = RigOffValue;
            }
        }
    }
}
using UnityEngine;
using UnityEngine.Animations.Rigging;

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

        public void Construct(Transform lookTarget)
        {
            _aimRigBody.data.sourceObjects = new WeightedTransformArray { new WeightedTransform(lookTarget, 1f) };
            _aimRigRightHand.data.sourceObjects = new WeightedTransformArray { new WeightedTransform(lookTarget, 1f) };

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
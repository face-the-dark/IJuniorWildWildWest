using System.Collections.Generic;
using UnityEngine;

namespace EnemyComponents
{
    public class ShootPointsCalculator : MonoBehaviour
    {
        private const float RadiusModifier = 2f;
        private const float PlayerHeadPositionOffest = 1.75f;

        [SerializeField] private Transform _player;
        [SerializeField] private float _viewFieldDistance = 10f;

        public Vector3 CalculateNearShootPosition()
        {
            List<Vector3> shootPositions = CalculateShootPositions();

            Vector3 nearPosition = shootPositions[0];

            foreach (Vector3 shootPosition in shootPositions)
                if (Vector3.Distance(transform.position, shootPosition)
                    < Vector3.Distance(transform.position, nearPosition))
                    nearPosition = shootPosition;

            return nearPosition;
        }

        private List<Vector3> CalculateShootPositions()
        {
            List<Vector3> positions = new List<Vector3>();

            int positionsCount = Mathf.CeilToInt(2f * Mathf.PI * _viewFieldDistance);

            for (int i = 0; i < positionsCount; i++)
            {
                float angle = RadiusModifier * Mathf.PI * i / positionsCount;

                float x = _player.position.x + Mathf.Cos(angle) * _viewFieldDistance;
                float z = _player.position.z + Mathf.Sin(angle) * _viewFieldDistance;

                Vector3 position = new Vector3(x, 0, z);

                if (IsFreePosition(position))
                    positions.Add(position);
            }

            return positions;
        }

        private bool IsFreePosition(Vector3 position)
        {
            Vector3 positionPoint = new Vector3(position.x, position.y + PlayerHeadPositionOffest, position.z);
            Vector3 playerRaycastPoint =
                new Vector3(_player.position.x, _player.position.y + PlayerHeadPositionOffest, _player.position.z);

            Vector3 direction = (positionPoint - playerRaycastPoint).normalized;

            Ray ray = new Ray(playerRaycastPoint, direction);

            return Physics.Raycast(ray, out RaycastHit hit, _viewFieldDistance) == false;
        }
    }
}
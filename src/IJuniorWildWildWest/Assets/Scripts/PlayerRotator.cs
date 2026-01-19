using UnityEngine;

public class PlayerRotator : PlayerTransformer
{
    [SerializeField] private float _epsilon = 0.01f;

    protected override void Transform(Vector3 direction)
    {
        if (direction.sqrMagnitude > _epsilon)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Speed * Time.deltaTime);
        }
    }
}
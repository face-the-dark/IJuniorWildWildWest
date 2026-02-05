using UnityEngine;

public class DirectionCalculator
{
    private readonly Transform _mainCamera;

    public DirectionCalculator(Transform mainCamera) => 
        _mainCamera = mainCamera;

    public Vector3 CalculateCameraViewDirection(Vector2 inputDirection)
    {
        Vector3 direction = new Vector3(inputDirection.x, 0f, inputDirection.y);

        Vector3 cameraForward = _mainCamera.forward;
        Vector3 cameraRight = _mainCamera.right;

        cameraForward.y = 0f;
        cameraRight.y = 0f;

        return cameraForward.normalized * direction.z + cameraRight.normalized * direction.x;
    }
}
using UnityEngine;

public class CameraSizeUsage : MonoBehaviour
{
    [SerializeField] protected float _mapWidth = 30f;
    [SerializeField] protected float _mapHeight = 40f;
    [SerializeField] protected GameObject _camera;

    protected Camera _cameraComponent;

    protected virtual void Start()
    {
        _cameraComponent = _camera.GetComponent<Camera>();
    }

    protected (float leftBound, float rightBound, float bottomBound, float topBound) GetCameraBounds()
    {
        float verticalSize = _cameraComponent.orthographicSize;
        float horizontalSize = verticalSize * _cameraComponent.aspect;

        float leftBound = -_mapWidth / 2 + horizontalSize;
        float rightBound = _mapWidth / 2 - horizontalSize;
        float bottomBound = -_mapHeight / 2 + verticalSize;
        float topBound = _mapHeight / 2 - verticalSize;

        return (leftBound, rightBound, bottomBound, topBound);
    }
}

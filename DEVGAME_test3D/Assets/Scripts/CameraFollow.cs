using UnityEngine;

public class CameraFollow : CameraSizeUsage
{
    [SerializeField] private Transform _player;
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _cameraHeight = 10f;

    private Vector3 _target;

    private void Update()
    {
        if (_player == null)
            return;

        _target = new Vector3(_player.position.x, _cameraHeight, _player.position.z);

        // ѕолучаем границы камеры
        var (leftBound, rightBound, bottomBound, topBound) = GetCameraBounds();

        // ќграничиваем положение камеры в пределах карты
        float cameraX = Mathf.Clamp(_target.x, leftBound, rightBound);
        float cameraZ = Mathf.Clamp(_target.z, bottomBound, topBound);

        _target = new Vector3(cameraX, _cameraHeight, cameraZ);
        transform.position = Vector3.Lerp(transform.position, _target, _speed * Time.fixedDeltaTime);
    }
}

using UnityEngine;

[RequireComponent(typeof(PlayerShooting))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed = 4.0f;
    private PlayerShooting _playerShooting;
    private Camera _mainCamera;
    private const float _rotationSpeed = 180f; // �������� ��������

    private void Awake()
    {
        _playerShooting = GetComponent<PlayerShooting>();
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalInput, 0, verticalInput);
        if (direction.magnitude > 1)
        {
            direction.Normalize();
        }

        if (direction != Vector3.zero)
        {
            // �������������� ����� � ������ ����������� ������
            Vector3 cameraForward = _mainCamera.transform.up;
            cameraForward.y = 0;
            cameraForward.Normalize();

            Vector3 cameraRight = _mainCamera.transform.right;
            cameraRight.y = 0;
            cameraRight.Normalize();

            Vector3 moveDirection = direction.x * cameraRight + direction.z * cameraForward;
            moveDirection.y = 0; // �������� y-����������, ����� �������� �� "�����������"

            transform.Translate(_speed * Time.deltaTime * moveDirection, Space.World);

            if (_playerShooting != null && !_playerShooting.IsRotating() && !_playerShooting.IsShooting())
            {
                // ������� ��������� � ����������� ��������
                Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, _rotationSpeed * Time.deltaTime);
            }
        }
    }

    public void DecreaseSpeed(float speedDecrease)
    {
        _speed *= speedDecrease;
    }

    public void IncreaseSpeed(float speedIncrease)
    {
        _speed /= speedIncrease;
    }
}

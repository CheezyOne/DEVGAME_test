using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    private const float _rotationSpeed = 180f;
    private bool _isRotating = false;
    private float _shootAngle;
    private float _lastShootTime;
    private bool _isShooting = false;

    [SerializeField] private GameObject _bullet, _grenade;
    [SerializeField] private float _bulletFlyForce = 10f;
    [SerializeField] private Transform _bulletSpawnPoint;

    public enum WeaponType { Pistol, Rifle, Shotgun, GrenadeLauncher }
    public WeaponType CurrentWeapon;
    public float ShootInterval = 0.5f;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            UpdateShootAngle();
            _isRotating = true;
            _isShooting = false;
        }

        if (Input.GetMouseButton(0))
        {
            UpdateShootAngle();
            _isRotating = true; 

            if (!_isRotating && Time.time - _lastShootTime >= ShootInterval)
            {
                Shoot();
                _lastShootTime = Time.time;
                _isShooting = true;  
            }
        }
        else
        {
            _isShooting = false; 
        }

        if (_isRotating)
        {
            RotateTowards(_shootAngle);
        }
    }

    private void UpdateShootAngle()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.y = transform.position.y;
        Vector3 direction = mousePosition - transform.position;
        _shootAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
    }

    private void RotateTowards(float targetAngle)
    {
        float currentAngle = transform.eulerAngles.y;
        float newAngle = Mathf.MoveTowardsAngle(currentAngle, targetAngle, _rotationSpeed * Time.deltaTime);
        transform.eulerAngles = new Vector3(0, newAngle, 0);

        if (Mathf.Approximately(newAngle, targetAngle))
        {
            _isRotating = false;
            if (!_isShooting && Time.time - _lastShootTime >= ShootInterval)
            {
                Shoot();
                _lastShootTime = Time.time;
            }
        }
    }
    public void DecideNextShootInterval()
    {
        switch (CurrentWeapon)
        {
            case WeaponType.Shotgun:
                ShootInterval = 0.66667f;
                break;
            case WeaponType.Pistol:
                ShootInterval = 0.5f;
                break;
            case WeaponType.Rifle:
                ShootInterval = 0.1f;
                break;
            case WeaponType.GrenadeLauncher:
                ShootInterval = 1.5f;
                break;
        }
    }
    private void Shoot()
    {
        Bullet Bullet;
        switch (CurrentWeapon)
        {
            case WeaponType.Shotgun:
                ShootShotgun();
                break;
            case WeaponType.Pistol:
                Bullet = SpawnBullet(_shootAngle);
                Bullet.Damage = 3;
                break;
            case WeaponType.Rifle:
                Bullet = SpawnBullet(_shootAngle);
                Bullet.Damage = 1;
                break;
            case WeaponType.GrenadeLauncher:
                SpawnGrenade(_shootAngle, new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, transform.position.y, Camera.main.ScreenToWorldPoint(Input.mousePosition).z));
                break;
        }
        DecideNextShootInterval();
    }

    private void ShootShotgun()
    {
        float SpreadAngle = 10f;
        int BulletCount = 5;
        float BulletDistance = 7f;
        for (int i = 0; i < BulletCount; i++)
        {
            float angleOffset = Random.Range(-SpreadAngle / 2, SpreadAngle / 2);
            Bullet Bullet = SpawnBullet(_shootAngle + angleOffset, BulletDistance);
            Bullet.Damage = 2;
        }
    }

    private Bullet SpawnBullet(float Angle, float BulletDistance = 1000000f)
    {
        Quaternion BulletRotation = Quaternion.Euler(0, Angle, 0);
        GameObject Bullet = PoolManager.SpawnObject(_bullet, _bulletSpawnPoint.position, BulletRotation);
        Rigidbody BulletRigidbody = Bullet.GetComponent<Rigidbody>();
        Bullet BulletComponent = Bullet.GetComponent<Bullet>();
        BulletComponent.DistanceToCover = BulletDistance;
        BulletRigidbody.AddForce(Bullet.transform.forward * _bulletFlyForce, ForceMode.Impulse);
        return BulletComponent;
    }
    private void SpawnGrenade(float Angle, Vector3 Destination)
    {
        Quaternion GrenadeRotation = Quaternion.Euler(0, Angle, 0);
        GameObject Grenade = PoolManager.SpawnObject(_grenade, _bulletSpawnPoint.position, GrenadeRotation);
        Grenade.GetComponent<Grenade>().Destination = Destination;
    }

    public bool IsRotating()
    {
        return _isRotating;
    }

    public bool IsShooting()
    {
        return _isShooting;
    }
}

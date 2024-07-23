using System;
using System.Collections;
using UnityEngine;
using static PlayerShooting;

public class BonusesSpawner : CameraSizeUsage
{
    [SerializeField] private GameObject _player;
    [SerializeField] private float _spawnWeaponInterval = 10f, _spawnPowerUpInterval = 27f, BonusYCoordinate = 1.5f; // Интервал между спавнами
    [SerializeField] private GameObject[] _powerUps, _weapons;
    private new void Start()
    {
        base.Start();
        StartCoroutine(SpawnRandomPowerUp());
        StartCoroutine(SpawnRandomWeapon());
    }

    private IEnumerator SpawnRandomPowerUp()
    {
        while (true)
        {
            yield return new WaitForSeconds(_spawnPowerUpInterval);
            SpawnNewBonus(_powerUps[UnityEngine.Random.Range(0, _powerUps.Length)]);
        }
    }
    private IEnumerator SpawnRandomWeapon()
    {
        while (true)
        {
            yield return new WaitForSeconds(_spawnWeaponInterval);
            Array values = Enum.GetValues(typeof(PlayerShooting.WeaponType));
            System.Random _R = new System.Random();
            PlayerShooting.WeaponType randomWeapon;
            try
            {
                do
                {
                    randomWeapon = (PlayerShooting.WeaponType)values.GetValue(_R.Next(values.Length));
                }
                while (randomWeapon == _player.GetComponent<PlayerShooting>().CurrentWeapon && _player != null);
                foreach (GameObject Weapon in _weapons)
                {
                    if (Weapon.GetComponent<Weapon>().WeaponType == randomWeapon)
                    {
                        SpawnNewWeapon(Weapon);
                        break;
                    }
                }
            }
            catch
            {

            }
        }
    }
    private void SpawnNewWeapon(GameObject Weapon)
    {
        Vector3 randomPosition = GetRandomPositionInsideCameraView();
        GameObject NewWeapon = PoolManager.SpawnObject(Weapon, randomPosition, Quaternion.identity);
        NewWeapon.GetComponent<Weapon>().Player = _player;
    }
    private void SpawnNewBonus(GameObject Bonus)
    {
        Vector3 randomPosition = GetRandomPositionInsideCameraView();
        GameObject NewBonus = PoolManager.SpawnObject(Bonus, randomPosition, Quaternion.identity);
        NewBonus.GetComponent<Bonus>().Player = _player;
    }
    private Vector3 GetRandomPositionInsideCameraView()
    {
        float verticalSize = _cameraComponent.orthographicSize;
        float horizontalSize = verticalSize * _cameraComponent.aspect;
        Vector3 cameraPosition = _cameraComponent.transform.position;
        Vector3 randomPosition;

        float leftBound = cameraPosition.x - horizontalSize;
        float rightBound = cameraPosition.x + horizontalSize;
        float bottomBound = cameraPosition.z - verticalSize;
        float topBound = cameraPosition.z + verticalSize;

        float randomX = UnityEngine.Random.Range(leftBound, rightBound);
        float randomZ = UnityEngine.Random.Range(bottomBound, topBound);
        randomPosition = new Vector3(randomX, BonusYCoordinate, randomZ);

        return randomPosition;
    }
}

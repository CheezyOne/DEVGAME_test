using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : CameraSizeUsage
{
    [SerializeField] private float _spawnInterval = 2f, _intervalDecrease = 0.1f, _minimumInterval = 0.5f, _intervalDecreaseTimer = 10f; 
    [SerializeField] private GameObject[] _enemies;
    [SerializeField] private Transform _player;
    private new void Start()
    {
        base.Start();
        StartCoroutine(SpawnRandomPosition());
        StartCoroutine(DecreaseSpawnTime());
    }
    private IEnumerator DecreaseSpawnTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(_intervalDecreaseTimer);
            if (_spawnInterval <= _minimumInterval)
                yield break;
            _spawnInterval -= _intervalDecrease; 
        }
    }
    private IEnumerator SpawnRandomPosition()
    {
        while (true)
        {
            yield return new WaitForSeconds(_spawnInterval);
            Vector3 randomPosition = GetRandomPositionOutsideCameraView();
            if (randomPosition == Vector3.zero)
                yield return SpawnRandomPosition();
            int RandomNumber = Random.Range(1,100);
            int EnemyIndex;
            if (RandomNumber <= 60)
            {
                EnemyIndex = 0;
            }
            else if(RandomNumber <= 90)//will be changed later to consts
            {
                EnemyIndex = 1;
            }
            else
            {
                EnemyIndex = 2;
            }
            GameObject NewEnemy = PoolManager.SpawnObject(_enemies[EnemyIndex], randomPosition, Quaternion.identity);
            NewEnemy.GetComponent<EnemyNavMesh>().Player = _player;
        }
    }

    private Vector3 GetRandomPositionOutsideCameraView()
    {
        int Counter = 0;
        float verticalSize = _cameraComponent.orthographicSize + 0.5f; //+0.5f is not to see parts of enemies when spawned
        float horizontalSize = verticalSize * _cameraComponent.aspect;
        Vector3 CameraPosition = _cameraComponent.transform.position;
        Vector3 randomPosition;
        bool isInsideView;

        do
        {
            Counter++;
            if(Counter>25)
            {
                randomPosition = Vector3.zero;
                Debug.LogWarning("Wasn't able to spawn an enemy");
                break;
            }
            float randomX = Random.Range((-_mapWidth+1) / 2, (_mapWidth-1) / 2);//+1 -1 is not to spawn too far in the wall
            float randomZ = Random.Range((-_mapHeight+1) / 2, (_mapHeight-1) / 2);
            randomPosition = new Vector3(randomX, 0, randomZ);
            isInsideView = randomPosition.x > (CameraPosition.x - horizontalSize) &&
                           randomPosition.x < (CameraPosition.x + horizontalSize) &&
                           randomPosition.z > (CameraPosition.z - verticalSize) &&
                           randomPosition.z < (CameraPosition.z + verticalSize);
        }
        while (isInsideView);

        return randomPosition;
    }
}

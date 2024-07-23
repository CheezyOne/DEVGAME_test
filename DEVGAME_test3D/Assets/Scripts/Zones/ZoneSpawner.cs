using UnityEngine;

public class ZoneSpawner : MonoBehaviour
{
    [SerializeField] private int _deathZonesCount, _slowZonesCount;
    [SerializeField] private Transform _upperSide, _lowerSide, _leftSide, _rightSide;
    [SerializeField] private GameObject _deathZone, _slowZone, _player;
    private const int _deathZoneRadius = 1, _slowZoneRadius = 3, _distanceBetweenZones = 3;
    private void Start()
    {
        SpawnZones(_deathZonesCount, _deathZone, _deathZoneRadius);
        SpawnZones(_slowZonesCount, _slowZone, _slowZoneRadius);
    }
    private void SpawnZones(int ZonesCount, GameObject Zone, int ZoneRadius)
    {
        for (int i = 0; i < ZonesCount; i++)
        {
            Vector3 ZonePoint = GetAPoint();
            while (!SpawnZone(ZonePoint, ZoneRadius))
            {
                ZonePoint = GetAPoint();
            }
            GameObject NewZone = PoolManager.SpawnObject(Zone, ZonePoint, Quaternion.identity);
            NewZone.GetComponent<Zone>().Player = _player;
        }
    }
    private bool SpawnZone(Vector3 Position, int ZoneRadius)
    {
        Collider[] AllColliders = Physics.OverlapSphere(Position, ZoneRadius + _distanceBetweenZones);
        foreach (Collider Collider in AllColliders)
        {
            if(Collider.GetComponent<Zone>() != null || Collider.GetComponent<PlayerHealth>() != null)
            {
                return false;
            }
        }
        return true;
    }
    private Vector3 GetAPoint()
    {
        Vector3 RandomPosition = new(
        Random.Range(_leftSide.position.x, _rightSide.position.x),
        0,
        Random.Range(_lowerSide.position.z, _upperSide.position.z)
    );
        return RandomPosition;
    }
}

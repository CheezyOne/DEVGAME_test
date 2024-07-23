using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float _waitTillBackToPool = 10f;
    private Vector3 _startingPosition;
    public float DistanceToCover;
    public int Damage;
    private void OnEnable()
    {
        _startingPosition = transform.position;
        StartCoroutine(GetDestroyed());
    }
    private void Update()
    {
        if(Vector3.Distance(_startingPosition, transform.position) >= DistanceToCover)
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            PoolManager.ReturnObjectToPool(gameObject);
        }
    }
    private IEnumerator GetDestroyed()
    {
        yield return new WaitForSeconds(_waitTillBackToPool);
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        PoolManager.ReturnObjectToPool(gameObject);
        yield break;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.TryGetComponent<EnemyHealth>(out EnemyHealth enemyHealth))
        {
            enemyHealth.GetHit(Damage);
        }
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        PoolManager.ReturnObjectToPool(gameObject);
    }
}

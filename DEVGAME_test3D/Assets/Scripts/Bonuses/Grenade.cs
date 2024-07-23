using UnityEngine;

public class Grenade : MonoBehaviour
{
    public Vector3 Destination;
    private int Damage = 10;
    private float _speed = 5f, _explosionRadius = 2f, _minimalDistanceToTarget = 0.05f;
    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, Destination, _speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, Destination) < _minimalDistanceToTarget)
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            Explode();
        }
    }
    private void Explode()
    {
        Collider[] Colliders = Physics.OverlapSphere(transform.position, _explosionRadius);
        foreach (Collider collider in Colliders)
        {
            if (collider.TryGetComponent<EnemyHealth>(out EnemyHealth enemyHealth))
            {
                enemyHealth.GetHit(Damage);
            }
        }
        PoolManager.ReturnObjectToPool(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _explosionRadius);
    }
}

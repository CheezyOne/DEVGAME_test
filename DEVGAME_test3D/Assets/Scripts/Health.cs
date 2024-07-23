using UnityEngine;

public class Health : MonoBehaviour
{
    private protected int _currentHealth;
    [SerializeField] private protected int _maxHealth;
    private void OnEnable()
    {
        _currentHealth = _maxHealth;
    }
    public void GetHit(int Damage)
    {
        _currentHealth -= Damage;
        if(_currentHealth<=0)
        {
            Die();
        }
    }
    public virtual void Die()
    {
        PoolManager.ReturnObjectToPool(gameObject);
    }
}

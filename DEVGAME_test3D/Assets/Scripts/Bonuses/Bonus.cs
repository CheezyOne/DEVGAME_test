using System.Collections;
using UnityEngine;

public class Bonus : MonoBehaviour
{
    public GameObject Player;
    [SerializeField] private BonusType _bonustype;
    [SerializeField] private float _bonusAmount, _bonusTime;
    private bool _hasBeenTaken = false;
    private const float _timeToExist = 5f;
    private enum BonusType { SpeedUp, Invincibility}
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == Player)
        {
            ApplyBonus();
        }
    }
    private void OnEnable()
    {
        _hasBeenTaken = false;
        StartCoroutine(DestroyIfNotTaken());
    }
    private IEnumerator DestroyIfNotTaken()
    {
        yield return new WaitForSeconds(_timeToExist);
        if (!_hasBeenTaken)
            PoolManager.ReturnObjectToPool(gameObject);
        yield break;
    }
    private void ApplyBonus()
    {
        switch(_bonustype)
        {
            case BonusType.SpeedUp:
                {
                    Player.GetComponent<PlayerMovement>().IncreaseSpeed(_bonusAmount);
                    break;
                }
            case BonusType.Invincibility:
                {
                    Player.GetComponent<PlayerHealth>().IsInvincible = true;
                    break;
                }
        }
        StartCoroutine(BonusCoroutine());
        GetRidOfTheBonus();
    }
    private void LiftBonus()
    {
        switch (_bonustype)
        {
            case BonusType.SpeedUp:
                {
                    Player.GetComponent<PlayerMovement>().DecreaseSpeed(_bonusAmount);
                    break;
                }
            case BonusType.Invincibility:
                {
                    Player.GetComponent<PlayerHealth>().IsInvincible = false;
                    break;
                }
        }
    }
    private void GetRidOfTheBonus()
    {
        transform.position = new(10000f, 10000f, 10000f);
        _hasBeenTaken = true;
    }
    private IEnumerator BonusCoroutine()
    {
        yield return new WaitForSeconds(_bonusTime);
        LiftBonus();
        PoolManager.ReturnObjectToPool(gameObject);
    }
}

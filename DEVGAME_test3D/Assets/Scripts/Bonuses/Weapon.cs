using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject Player;
    public PlayerShooting.WeaponType WeaponType;
    [SerializeField] private float _timeToExist;

    private void OnEnable()
    {
        StartCoroutine(DestroyIfNotTaken());
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == Player)
        {
            GiveWeapon();
        }
    }
    private void GiveWeapon()
    {
        PlayerShooting PlayerComponent = Player.GetComponent<PlayerShooting>();
        PlayerComponent.CurrentWeapon = WeaponType;
        PlayerComponent.DecideNextShootInterval();
        PoolManager.ReturnObjectToPool(gameObject);
    }
    private IEnumerator DestroyIfNotTaken()
    {
        yield return new WaitForSeconds(_timeToExist);
        PoolManager.ReturnObjectToPool(gameObject);
        yield break;
    }
}

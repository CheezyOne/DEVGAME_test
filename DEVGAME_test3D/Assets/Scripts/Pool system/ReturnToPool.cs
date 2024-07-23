using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnToPool : MonoBehaviour
{
    [SerializeField] private float _retrunToPoolTimer;
    private void OnEnable()
    {
        StartCoroutine(ReturnToPoolCoroutine());
    }
    private IEnumerator ReturnToPoolCoroutine()
    {
        yield return new WaitForSeconds(_retrunToPoolTimer);
        PoolManager.ReturnObjectToPool(gameObject);
    }
}

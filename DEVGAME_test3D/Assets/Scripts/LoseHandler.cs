using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseHandler : MonoBehaviour
{
    [SerializeField] private float _slowdownFactor = 0.01f; 
    [SerializeField] private GameObject _loseUI; 
    private CanvasGroup _canvasGroup;
    private void OnEnable()
    {
        Time.timeScale = 1f;
        PlayerHealth.onPlayerDeath += StartSlowingDown;
    }
    private void OnDisable()
    {
        PlayerHealth.onPlayerDeath -= StartSlowingDown;
    }
    private void Awake()
    {
        _canvasGroup = _loseUI.GetComponent<CanvasGroup>();
    }
    private void StartSlowingDown()
    {
        _loseUI.SetActive(true); 
        StartCoroutine(Slowdown());
    }

    IEnumerator Slowdown()
    {
        if (Time.timeScale <= 0)
            yield break;
        if (Time.timeScale - _slowdownFactor <= 0)
        {
            Time.timeScale = 0;
            yield break;
        }
        Time.timeScale -= _slowdownFactor;
        _canvasGroup.alpha += _slowdownFactor;
        yield return new WaitForSecondsRealtime(0.05f);//Change this later to DOTween
        yield return Slowdown();
    }
}

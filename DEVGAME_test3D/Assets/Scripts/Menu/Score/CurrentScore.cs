using System;
using TMPro;
using UnityEngine;

public class CurrentScore : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    public int currentScore = 0;
    private void OnEnable()
    {
        EnemyHealth.onEnemyDeath += AddScore;
    }
    private void OnDisable()
    {
        EnemyHealth.onEnemyDeath -= AddScore;
    }
    private void AddScore(int Score)
    {
        currentScore += Score;
        _text.text = currentScore.ToString();
    }
}

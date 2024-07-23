using UnityEngine;

public class BestScore : MonoBehaviour
{
    [SerializeField] private CurrentScore _currentScore;
    [SerializeField] private GameObject _NewBestScoreText;
    private void OnEnable()
    {
        PlayerHealth.onPlayerDeath += GetScore;
    }
    private void OnDisable()
    {
        PlayerHealth.onPlayerDeath -= GetScore;
    }
    private void GetScore()
    {
        if (PlayerPrefs.GetInt("Score") < _currentScore.currentScore)//Later change to json, for obvious reasons
            SetNewRecord();
    }
    private void SetNewRecord()
    {
        PlayerPrefs.SetInt("Score", _currentScore.currentScore);
        _NewBestScoreText.SetActive(true);
    }
}

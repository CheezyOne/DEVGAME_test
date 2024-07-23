using TMPro;
using UnityEngine;

public class MenuScoreShow : MonoBehaviour
{
    [SerializeField] private TMP_Text _bestScoreText;
    private void Start()
    {
        int BestScore = PlayerPrefs.GetInt("Score");
        _bestScoreText.text = BestScore.ToString();
    }
}

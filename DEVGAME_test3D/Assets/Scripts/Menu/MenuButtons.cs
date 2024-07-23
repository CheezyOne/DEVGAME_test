using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    [SerializeField] private string _sceneName;
    public void LoadScene()
    {
        SceneManager.LoadScene(_sceneName);
    }
}

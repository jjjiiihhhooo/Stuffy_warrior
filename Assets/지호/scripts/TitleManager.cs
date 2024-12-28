using UnityEngine;

public class TitleManager : MonoBehaviour
{
    [SerializeField] private string mainSceneName;
    public void GameStart()
    {
        LoadingSceneManager.LoadScene(mainSceneName);
    }

    public void Exit()
    {
        Application.Quit();
    }
}

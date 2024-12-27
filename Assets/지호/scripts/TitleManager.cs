using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleManager : MonoBehaviour
{
    [SerializeField] private string mainSceneName;
    public void GameStart()
    {
        LoadingSceneManager.LoadScene(mainSceneName);
    }
}

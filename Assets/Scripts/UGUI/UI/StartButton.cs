using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class StartButton : MonoBehaviour
{
    private SaveNLoad theSaveNLoad;
    #region  SingleTon
    private static StartButton instance;
    public static StartButton Instance
    {
        get
        {
            if (instance == null)
                return null;
            return instance;
        }
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }
#endregion
    public void StartGame(string SceneName)
    {
        loadingSceneManager.LoadScene(SceneName);
    }
    public void LoadGame(string SceneName)
    {
        StartCoroutine(LoadCoroutine(SceneName));
    }
    IEnumerator LoadCoroutine(string SceneName)
    {
        loadingSceneManager.LoadScene(SceneName);
        while (!loadingSceneManager.isdone)
        {
            yield return null;
        }
        theSaveNLoad = FindObjectOfType<SaveNLoad>();
        theSaveNLoad.LoadData();
        gameObject.SetActive(false);
    }
}

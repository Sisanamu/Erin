using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class StartButton : MonoBehaviour
{
    public string sceneName;
    public GameObject noSaveData;
    public SaveNLoad theSaveNLoad;
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
    private void Update()
    {
        theSaveNLoad = FindObjectOfType<SaveNLoad>();
    }
    public void StartGame(string SceneName)
    {
        StartCoroutine(LoadScene(SceneName));
    }
    public void LoadGame()
    {
        theSaveNLoad.LoadData();
        sceneName = theSaveNLoad.getSceneName;
        if (File.Exists(theSaveNLoad.File_Path))
            StartCoroutine(LoadCoroutine(sceneName));
        else
            StartCoroutine(NotHaveSave());
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
    IEnumerator LoadScene(string SceneName)
    {
        loadingSceneManager.LoadScene(SceneName);
        while (!loadingSceneManager.isdone)
        {
            yield return null;
        }
        GameManager.Instance.Init();
        gameObject.SetActive(false);
    }
    IEnumerator NotHaveSave()
    {
        noSaveData.SetActive(true);
        yield return new WaitForSeconds(1f);
        noSaveData.SetActive(false);
    }
}
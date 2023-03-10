using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class loadingSceneManager : MonoBehaviour
{
    public static string nextScene;
    public static bool isdone = false;
    [SerializeField] protected Image loadingBar;
    [SerializeField] protected GameObject loading;
    private void Start()
    {
        isdone = false;
        StartCoroutine(LoadScene());
    }

    public static void LoadScene(string sceneName)
    {
        nextScene = sceneName;
        SceneManager.LoadScene("loading");
    }

    IEnumerator LoadScene()
    {
        AsyncOperation AO = SceneManager.LoadSceneAsync(nextScene);
        AO.allowSceneActivation = false;

        float timer = 0.0f;
        loadingBar.fillAmount = 0f;
        while (!AO.isDone)
        {
            timer += Time.deltaTime * 2f;
            loadingBar.fillAmount = Mathf.Lerp(loadingBar.fillAmount, 1f, timer);
            if(loadingBar.fillAmount <= 1f)
            {
                yield return new WaitForSeconds(0.1f);
            }
            if(loadingBar.fillAmount == 1f)
            {
                isdone = true;
                AO.allowSceneActivation = true;
                Debug.Log(loadingSceneManager.isdone);
                yield break;
            }
        }
    }
}
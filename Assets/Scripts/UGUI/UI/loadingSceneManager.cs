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
        isdone = false;
        AO.allowSceneActivation = false;

        float timer = 0.0f;
        while (!AO.isDone)
        {
            timer += Time.deltaTime;
            if (AO.progress < 0.9f)
            {
                loadingBar.fillAmount = Mathf.Lerp(loadingBar.fillAmount, AO.progress, timer);
                if (loadingBar.fillAmount >= AO.progress) { timer = 0f; }
            }
            else
            {
                loadingBar.fillAmount = Mathf.Lerp(loadingBar.fillAmount, 1f, timer);
                if (loadingBar.fillAmount == 1.0f)
                {
                    AO.allowSceneActivation = true;
                    yield break;

                }
            }
        }
    }
}

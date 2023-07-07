using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControler: MonoBehaviour
{
    public static SceneControler sceneControler;
    [SerializeField] Animator FadeEffect;

    int fadeTriggerHash = Animator.StringToHash("Fade");

    private void Awake()
    {
        if (sceneControler)
        {
            if (sceneControler != this) Destroy(gameObject);
        }
        else
        {
            sceneControler = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
    }

    public void LoadScene(int sceneID)
    {
        StartCoroutine(FadeToScene(sceneID));
    }

    IEnumerator FadeToScene(int sceneID)
    {
        FadeEffect.SetTrigger(fadeTriggerHash);
        yield return new WaitForSeconds(1.4f);
        SceneManager.LoadScene(sceneID);
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        FadeEffect.SetTrigger(fadeTriggerHash);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneBehavior : MonoBehaviour
{
    public Animator animController;
    public void NextScene(string scene)
    {
        animController.enabled = true;
        StartCoroutine(WaitforScene(1.0f, scene));
    }

    IEnumerator WaitforScene(float waitTime, string sceneName)
    {
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene(sceneName);
    }
}

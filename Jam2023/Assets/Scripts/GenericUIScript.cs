using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GenericUIScript : MonoBehaviour
{
    public GameObject targetUI;
    public int targetScene;

    public void QuitApplication()
    {
        // Quit the application
        Debug.Log("Quit");
        Application.Quit();

        // Note that in the Unity Editor, this function may not work.
        // It typically works in standalone builds.
    }

    public void EnableTargetUI()
    {
        targetUI.SetActive(true);
    }
    public void LoadSceneByBuildIndex()
    {
        SceneManager.LoadScene(targetScene);
    }
}

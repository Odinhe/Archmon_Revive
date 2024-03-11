using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public string sceneName;

    // Function to switch to the specified scene
    public void SwitchScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}

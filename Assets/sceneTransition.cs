using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneTransition : MonoBehaviour
{

    public void Load(string scene)
    {
        Debug.Log("HURR");
        SceneManager.LoadScene(scene);
    }

    public void UHHH()
    {
        Debug.Log("UH");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneTransition : MonoBehaviour
{
    public string loadScene;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("t")) Load();
    }

    public void Load()
    {
        Debug.Log("HURR");
        SceneManager.LoadScene(loadScene);
    }
}

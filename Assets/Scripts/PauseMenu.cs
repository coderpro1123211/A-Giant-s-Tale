using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    PauseMenuManager m;

    private void Awake()
    {
        m = FindObjectOfType<PauseMenuManager>();
    }

    public void Exit()
    {
        SceneManager.LoadScene(0);
    }

    public void Resume()
    {
        m.UnPause();
    }
}

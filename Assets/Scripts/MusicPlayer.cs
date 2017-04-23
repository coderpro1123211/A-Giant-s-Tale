using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour {
	void Start ()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.activeSceneChanged += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene s, Scene m)
    {
        if (m.buildIndex == 0)
        {
            Destroy(gameObject);
        }
    }
}

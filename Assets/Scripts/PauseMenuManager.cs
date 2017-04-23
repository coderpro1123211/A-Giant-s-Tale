using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuManager : MonoBehaviour {

    bool paused;
    GameObject pauseScreen;
    Player p;

	// Use this for initialization
	void Start ()
    {
        pauseScreen = FindObjectOfType<PauseMenu>().gameObject;
        pauseScreen.SetActive(false);

        p = FindObjectOfType<Player>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
	}

    public void Pause()
    {
        paused = true;
        pauseScreen.SetActive(true);
        p.disableMovement = true;
    }

    public void UnPause()
    {
        paused = false;
        pauseScreen.SetActive(false);
        p.disableMovement = false;
    }

    public void TogglePause()
    {
        if (paused) UnPause();
        else Pause();
    }
}

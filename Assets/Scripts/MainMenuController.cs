using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour {

    [HideInInspector]
    public bool showingLevelSelectScreen;

    Animator anim;

	void Start ()
    {
        anim = GetComponent<Animator>();
	}

    public void ShowLevelSelectScreen()
    {
        showingLevelSelectScreen = true;
        anim.SetTrigger("up");
    }

    public void HideLevelSelectScreen()
    {
        showingLevelSelectScreen = false;
        anim.SetTrigger("down");
    }

    public void Exit()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}

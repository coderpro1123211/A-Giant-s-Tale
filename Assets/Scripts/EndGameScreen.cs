using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameScreen : MonoBehaviour {

    public void Show()
    {
        GetComponent<Animator>().SetTrigger("show");
        FindObjectOfType<Player>().disableMovement = true;
        //TODO: goto the right scene after this
        Invoke("m", 15);
    }

    void m()
    {
        SceneManager.LoadScene(0);
    }
}

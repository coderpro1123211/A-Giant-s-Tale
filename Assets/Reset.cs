using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Reset : MonoBehaviour {

    public void ResetData()
    {
        PlayerPrefs.DeleteAll();

        PlayerPrefs.Save();

        Invoke("a", 0.2f);
    }

    void a()
    {
        SceneManager.LoadScene(0);
    }
}

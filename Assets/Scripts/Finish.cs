using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour {

    //public int level;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("YAY!");
        if (collision.gameObject.transform.root.gameObject.CompareTag("Player"))
        {
            PlayerPrefs.SetInt("l" + SceneManager.GetActiveScene().buildIndex, 1);
            PlayerPrefs.Save();

            print("BuildIndex: " + SceneManager.GetActiveScene().buildIndex + " | ToLoad: " + SceneManager.GetActiveScene().buildIndex + 1 + " | SceneCount: " + SceneManager.sceneCountInBuildSettings);

            if (SceneManager.GetActiveScene().buildIndex + 1 < SceneManager.sceneCountInBuildSettings - 1)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            else
            {
                SceneManager.LoadScene(SceneManager.sceneCountInBuildSettings - 1);
            }
        }
    }
}

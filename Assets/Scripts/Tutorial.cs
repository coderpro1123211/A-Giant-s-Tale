using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour {

    public a[] lines;
    int C;

    Animator anim;
    public Player player;

	// Use this for initialization
	void Start ()
    {
        C = 0;
        player.gameObject.SetActive(false);
        anim = GetComponent<Animator>();
        StartTutorial();
	}

    void StartTutorial()
    {
        DialougeManager.ShowDialougeBox(lines[C].lines, null, Next);
    }

    void Next()
    {
        //print("LASJDFAIOWGREPBÖ");
        if (C+1 >= lines.Length)
        {
            player.gameObject.SetActive(true);
            Destroy(gameObject);
            return;
        }
        C++;
        anim.SetTrigger("next");
        DialougeManager.ShowDialougeBox(lines[C].lines, null, Next);
    }
}

[System.Serializable]
public struct a
{
    public string[] lines;
    public Sprite[] expressions;

    public a(string[] lines, Sprite[] expressions)
    {
        this.lines = lines;
        this.expressions = expressions;
    }
}

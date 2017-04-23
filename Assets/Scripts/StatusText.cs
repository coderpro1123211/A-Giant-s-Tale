using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusText : MonoBehaviour {

    public Player p;
    Text t;

	// Use this for initialization
	void Start () {
        p = FindObjectOfType<Player>();
        t = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        if (p == null)
        {
            t.text = "";
            return;
        }

        if (p.canInteract)
        {
            t.text = "Press \"e\" to interact";
        }
        else if(p.canJump)
        {
            t.text = "Press space to jump";
        }
        else
        {
            t.text = "";
        }
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicChecker : MonoBehaviour {

    public GameObject go;

	void Awake ()
    {
        if (FindObjectOfType<MusicPlayer>() == null) Instantiate(go);
	}
}

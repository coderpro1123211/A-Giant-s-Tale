using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Scaler : MonoBehaviour {

    public Transform a;
    public Transform b;
    public Transform toScale;
    //BoxCollider2D c;
    //public Vector2 offset;
    //public float cScale;

	// Use this for initialization
	void Start () {
        //c = GetComponent<BoxCollider2D>();
	}
	
	// Update is called once per frame
	void Update () {
        //if(c == null)
            //c = GetComponent<BoxCollider2D>();
        toScale.localPosition = Vector2.Lerp(a.localPosition, b.localPosition, 0.5f);
        toScale.localScale = new Vector3(1, 2.7f*a.localPosition.y);
        //c.offset = Vector2.Lerp(a.localPosition, b.localPosition, 0.5f) + offset * a.localPosition.y * cScale;
        //c.size = new Vector2(c.size.x, cScale * a.localPosition.y);
	}
}

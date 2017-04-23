using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public Transform target;
    public Transform overrideTarget;
    public float speed;
    public float smoothTime;

    float os;

    Camera c;

    Vector3 vel;
    UnityEngine.Bounds bounds;

	void Start ()
    {
        c = GetComponent<Camera>();
        bounds = FindObjectOfType<Bounds>().GetComponent<BoxCollider2D>().bounds;
        os = c.orthographicSize;
	}

	void Update ()
    {
        transform.position = Vector3.SmoothDamp(transform.position, (overrideTarget == null ? target.position : overrideTarget.position) + Vector3.back * 10, ref vel, smoothTime, speed, Time.smoothDeltaTime);

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, bounds.min.x + c.orthographicSize * c.aspect, bounds.max.x - c.orthographicSize * c.aspect), Mathf.Clamp(transform.position.y, bounds.min.y + c.orthographicSize, bounds.max.y - c.orthographicSize), -10);
	}
}

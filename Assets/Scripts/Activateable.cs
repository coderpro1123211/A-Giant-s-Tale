using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activateable : MonoBehaviour {

    public virtual void Activate()
    {
        FindObjectOfType<Player>().disableMovement = true;
        Camera.main.GetComponent<CameraController>().overrideTarget = transform;
    }

    public virtual void ActivateComplete()
    {
        FindObjectOfType<Player>().disableMovement = false;
        Camera.main.GetComponent<CameraController>().overrideTarget = null;
    }
}

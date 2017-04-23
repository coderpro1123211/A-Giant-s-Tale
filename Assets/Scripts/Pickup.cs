using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Pickup : Interactable
{

    public override void OnActivation()
    {
        base.OnActivation();
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("YAY!");
        if (collision.gameObject.transform.root.gameObject.CompareTag("Player"))
        {
            Activate(true);
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Activateable {

    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public override void Activate()
    {
        Invoke("lel", 1);
        base.Activate();
    }

    void lel()
    {
        Invoke("ActivateComplete", 1.0f);
        anim.SetTrigger("activate");
    }

    public override void ActivateComplete()
    {
        base.ActivateComplete();
    }
}

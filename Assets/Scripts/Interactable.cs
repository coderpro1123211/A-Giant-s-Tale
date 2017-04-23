using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Interactable : MonoBehaviour
{
    public Activateable toActivate;
    bool canActivate = true;

    public virtual void OnActivation()
    {
        print("OnAct");
        toActivate.Activate();
    }

    public void Activate(bool blockFurther)
    {
        print("Act");
        if (!canActivate) return;
        OnActivation();
        canActivate = !blockFurther;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorAlert : MonoBehaviour
{
    DualPlayerController controller;
    PugilistPlayerController pugilist;
    SorcererPlayerController sorcerer;

    private void Start()
    {
        controller = ServiceLocator.Get<DualPlayerController>();
        pugilist = ServiceLocator.Get<PugilistPlayerController>();
    }

    public void AlertEndOfFirstRoll()
    {
        controller.AlertEndOfFirstRoll();
    }

    public void AlertEndOfSecondRoll()
    {
        controller.AlertEndOfSecondRoll();
    }

    public void AlertEndOfFirstPunch()
    {
        pugilist.AlertEndOfFirstPunch();
    }

    public void AlertEndOfSecondPunch()
    {
        pugilist.AlertEndOfSecondPunch();
    }

    public void AlertEndOfPunchCombo()
    {
        pugilist.AlertEndOfPunchCombo();
    }
}

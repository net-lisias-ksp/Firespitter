/*
Firespitter /L
Copyright 2013-2018, Andreas Aakvik Gogstad (Snjo)
Copyright 2018-2020, LisiasT

    Developers: LisiasT, Snjo

    This file is part of Firespitter.
*/
using UnityEngine;

/// <summary>
/// For single buttons in a part, like switches.
/// Assign this to a collider with isTrigger.
/// Activates the function assigned to mouseDownFunction.
/// </summary>
class FSgenericButtonHandler : MonoBehaviour
{
    public delegate void MouseDownFunction();
    /// <summary>
    /// A function in the form buttonClick()
    /// </summary>
    public MouseDownFunction mouseDownFunction;
    public void OnMouseDown()
    {
        mouseDownFunction();
    }
}


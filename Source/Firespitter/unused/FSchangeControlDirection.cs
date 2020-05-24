/*
Firespitter /L
Copyright 2013-2018, Andreas Aakvik Gogstad (Snjo)
Copyright 2018-2020, LisiasT

    Developers: LisiasT, Snjo

    This file is part of Firespitter.
*/
using UnityEngine;

using Log = Firespitter.Log;

class FSchangeControlDirection : PartModule
{
    [KSPAction("Control From Here")]
    public void refForward(KSPActionParam param)
    {
        Log.info("Setting vessel reference transform");
        vessel.SetReferenceTransform(part);
    }
}
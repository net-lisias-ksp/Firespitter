/*
Firespitter /L
Copyright 2013-2018, Andreas Aakvik Gogstad (Snjo)
Copyright 2018-2020, LisiasT

    Developers: LisiasT, Snjo

    This file is part of Firespitter.
*/
using System;
using System.Linq;
using UnityEngine;

// This is the Damned aerospace's basicpropspinner, only with a check for active vessel.
// This class has been deprecated in favor of FSplanePropellerSpinner

[Obsolete("Use FSplanePropellerSpinner instead",false)]
public class FSthrottlePropSpinner : PartModule 
{
    [KSPField]
    public string rotorparent = "Spinner";
    [KSPField]
    public float rotationSpeed = -800f; // in RPM

    public override void OnUpdate()
    {
        if (!HighLogic.LoadedSceneIsFlight || !vessel.isActiveVessel) return; 

        var engine = part.Modules.OfType<ModuleEngines>().FirstOrDefault();

        bool engineActive = engine && engine.getIgnitionState && !engine.getFlameoutState;

        if (engineActive && vessel != null)
        {
            Transform RotorParent = part.FindModelTransform(rotorparent);
            RotorParent.transform.Rotate(Vector3.forward * ((rotationSpeed * 6) * TimeWarp.deltaTime * FlightInputHandler.state.mainThrottle));
        }
    }
}

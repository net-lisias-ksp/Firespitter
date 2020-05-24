/*
Firespitter /L
Copyright 2013-2018, Andreas Aakvik Gogstad (Snjo)
Copyright 2018-2020, LisiasT

    Developers: LisiasT, Snjo

    This file is part of Firespitter.
*/
using UnityEngine;

class FSclimbRate : InternalModule
{
    [KSPField]
    public string needleName = "needle";
    [KSPField]
    public float range = 2000f;
    [KSPField]
    public float maxAngle = 170f;
    [KSPField]
    public bool useFeetPerMin = true;

    private Transform needle;
    private float verticalSpeed = 0f;
    private float degreeRange;
    private float needleAngle = 0f;
    private float anglesPerUnit = 0f;
    private float toFeetperMin = 196.850394f;    

    public void Start()
    {
        if (HighLogic.LoadedSceneIsFlight)
        {
            needle = base.internalProp.FindModelTransform(needleName);
            degreeRange = maxAngle * 2f;
            anglesPerUnit = maxAngle / range;
        }
    }

    public override void OnUpdate()
    {
        if (HighLogic.LoadedSceneIsFlight)
        {
            if (vessel != null)
            {
                verticalSpeed = (float)vessel.verticalSpeed;
                if (useFeetPerMin)
                    verticalSpeed *= toFeetperMin;
                needleAngle = Mathf.Clamp(verticalSpeed * anglesPerUnit, -maxAngle, maxAngle);
                needle.localRotation = Quaternion.Euler(0f, 0f, -needleAngle);
            }
        }
    }
}

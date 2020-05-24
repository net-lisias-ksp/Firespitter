/*
Firespitter /L
Copyright 2013-2018, Andreas Aakvik Gogstad (Snjo)
Copyright 2018-2020, LisiasT

    Developers: LisiasT, Snjo

    This file is part of Firespitter.
*/
using UnityEngine;

using Log = Firespitter.Log;

public class FSanimateManually : PartModule
{
    [KSPField]
    public string targetObject;
    [KSPField]
    public int allowInvert = 0;
    [KSPField]
    public string animationName = "Retract";

    [KSPField(guiActive = false, guiName = "invertMotion", isPersistant = true)]    
    private bool invertMotion = false;
    [KSPField(guiActive = false, guiName = "invertSet", isPersistant = true)]
    private bool invertSet = false;

    [KSPField]
    public Vector2 startAndEndTime = new Vector2(0f, 1f);

    private Transform objectTransform;
    private Transform originalTransform;
    private Transform endTransform;
    private Transform endTransformInverted;
    Animation anim;
    private bool animationExists = false;
    private bool objectExists = false;    
    private bool animatingForward = false;
    private float currentTime;
    private float oldTime = -1; // default to -1 so that the position update is forced the firste time it's run

    public override void OnStart(PartModule.StartState state)
    {
        base.OnStart(state);
        //get the starting rotations           
        objectTransform = part.FindModelTransform(targetObject);
        originalTransform = new GameObject().transform;
        endTransform = part.FindModelTransform(targetObject + "End");
        endTransformInverted = part.FindModelTransform(targetObject + "EndInverted");
        if (!objectTransform)
        {
            Log.warn("FSanimateManually: No such object, {0}", targetObject);
            objectExists = false;
        }
        else
        {
            Log.dbg("FSanimateManually: Found object {0}", targetObject);
            originalTransform.localPosition = objectTransform.localPosition;
            originalTransform.localRotation = objectTransform.localRotation;
            //originalTransform = objectTransform;
            objectExists = true;
        }

        Log.dbg("FSanimateManually: looking for anim");
        anim = part.GetComponentInChildren<Animation>();
        if (anim != null)
        {            
            Log.dbg("FSanimateManually: Found anim {0} / {1}", animationName, anim.name);
            animationExists = true;
        }
        else
        {
            Log.warn("FSanimateManually: no animation. ");
        }
        //set the rotation based on flip when in the VAB/SPH, or at launch  
        // NOPE, can't run the update code in the sph, because there is no vessel object yet, so no finding left/right
        // maybe some world coords?
        
    }

    public override void OnUpdate()
    {
        if (!HighLogic.LoadedSceneIsFlight) return;
        if (!invertSet) //run only the first time the craft is loaded
        {
            //check if the part is on the left or right side of the ship
            if (Vector3.Dot(objectTransform.position.normalized, vessel.ReferenceTransform.right) < 0) // below 0 means the engine is on the left side of the craft
            {
                invertMotion = true;
                Log.dbg("FSanimateManually: Inverting left side Gear");
            }
           invertSet = true;
        }

        if (animationExists && objectExists)
        {            
            //check the animation time, normalize
            currentTime = anim[animationName].normalizedTime;
            float useTime = currentTime;
            if (currentTime != 0f)
            {
                animatingForward = (currentTime > oldTime);
            }
            if (animatingForward && currentTime == 0f)
                useTime = 1f;                 

            //set the rotation if the animation has progressed
            //if (useTime != oldTime)
            //{
                if (invertMotion && allowInvert == 1)
                {
                    //add translation when needed
                    objectTransform.localRotation = Quaternion.Lerp(endTransformInverted.localRotation, originalTransform.localRotation, useTime);                    
                }
                else
                {
                    //add translation when needed
                    objectTransform.localRotation = Quaternion.Slerp(endTransform.localRotation, originalTransform.localRotation, useTime);
                }
            //}  

                oldTime = currentTime;
        }
        else
        {
            Log.warn("FSanimateManually: Error, missing object {0}: {1} / missing animation {2}: {3}" + targetObject, objectExists, animationName, animationExists);
        }
    }
}

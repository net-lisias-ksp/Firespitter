/*
Firespitter /L
Copyright 2013-2018, Andreas Aakvik Gogstad (Snjo)
Copyright 2018-2020, LisiasT

    Developers: LisiasT, Snjo

    This file is part of Firespitter.
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class FSremoteControl : PartModule //This is just a testing class for various ideas right now
{

    //public bool isEnabled;
    public bool passActiongGroups;
    public float fogDensity = 0.005f;

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (FlightGlobals.fetch.vesselTargetMode != VesselTargetModes.None)
        {
            Vessel target = (Vessel)FlightGlobals.fetch.VesselTarget;
            //target.ctrlState.mainThrottle = 1f;

            foreach (Part part in target.Parts)
            {
                ControlSurface ctrlsurf = part.Modules.OfType<ControlSurface>().FirstOrDefault();
                if (ctrlsurf != null)
                {
                    ctrlsurf.ActivatesEvenIfDisconnected = true;
                    ctrlsurf.inputVector = new Vector3(vessel.ctrlState.X, vessel.ctrlState.Y, vessel.ctrlState.Z);
                }
            }

            target.ctrlState.mainThrottle = vessel.ctrlState.Z;
            target.ctrlState.pitch = vessel.ctrlState.X;
            target.ctrlState.roll = vessel.ctrlState.Z;
            //Vessel targetVessel = (Vessel)target;
            Log.dbg(target);
            BaseFieldList test = this.part.Fields;
            foreach (BaseField field in test)
            {
                field.guiActive = false;
            }

            fogDensity -= 0.0001f;
            if (fogDensity <= 0f)
            {
                fogDensity = 0.01f;
            }

            RenderSettings.fog = true;
            RenderSettings.fogColor = Color.white;
            RenderSettings.fogDensity = fogDensity;
            RenderSettings.fogStartDistance = -50f;
            RenderSettings.fogMode = FogMode.ExponentialSquared;
            //Camera.main.clearFlags = CameraClearFlags.SolidColor;
            //Camera.main.backgroundColor = new Color(1f,1f,1f,0.1f);
        }
        else
        {
            
            RenderSettings.fog = false;
            RenderSettings.fogColor = new Color(0.439f, 0.859f, 1.000f, 0.0f); ;
            RenderSettings.fogDensity = 1.5E-05f;
            RenderSettings.fogMode = FogMode.ExponentialSquared;
            Camera.main.clearFlags = CameraClearFlags.Depth;
            Camera.main.backgroundColor = new Color(0f,0f,0f,0.02f);  
            
            Log.dbg("rsFog {0}", RenderSettings.fog);
            Log.dbg("fogC {0}", RenderSettings.fogColor);
            Log.dbg("fDens {0}", RenderSettings.fogDensity);
            Log.dbg("fMode {0}", RenderSettings.fogMode);
            Log.dbg("camCF {0}", Camera.main.clearFlags);
            Log.dbg("camBGc {0}", Camera.main.backgroundColor);
        }
    }
}
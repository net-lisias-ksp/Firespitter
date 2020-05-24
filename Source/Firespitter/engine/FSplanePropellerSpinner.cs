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
using UnityEngine;

namespace Firespitter.engine
{
    public class FSplanePropellerSpinner : PartModule
    {
        [KSPField]
        public string engineID;
        [KSPField]
        public string propellerName = "propeller";
        [KSPField]
        public float rotationSpeed = -320f; // in RPM
        [KSPField]
        public int useRotorDiscSwap = 0;
        [KSPField]
        public float rotorDiscSpeed = -30f;
        [KSPField]
        public string rotorDiscName = "rotorDisc";
        [KSPField]
        public float rotorDiscFadeInStart = 0.5f;
        [KSPField]
        public float rotorDiscFadeInEnd = 0.95f; // not currently used
        [KSPField]
        public float windmillMinAirspeed = 30.0f;
        [KSPField]
        public float windmillRPM = 0.1f;
        [KSPField]
        public float spinUpTime = 10f; // divide engineResponseSpeed by this amount for dramatic effect
        [KSPField]
        public float thrustRPM = 0f; // added to rotationSpeed
        [KSPField]
        public string blade1 = "";
        [KSPField]
        public string blade2 = "";
        [KSPField]
        public string blade3 = "";
        [KSPField]
        public string blade4 = "";
        [KSPField]
        public string blade5 = "";
        [KSPField]
        public string blade6 = "";
        [KSPField]
        public bool usesDeployAnimation = false;
        [KSPField]
        public bool deployAnimationStartsDeployed = true;
        [KSPField]
        public bool duplicatedBlades = false;

        private Firespitter.engine.FSengineWrapper engine;
        private Transform propeller;
        private Transform rotorDisc;
        private float targetRPM = 0f;
        private float currentRPM = 0f;
        private float maxThrust = 0f;
        private float smoothedThrustRPM = 0f;
        private List<GameObject> bladeObjects = new List<GameObject>();
        private Transform[] blurObjects;
        private List<String> bladeNames = new List<string>();
        private FSanimateGeneric deployAnimation = new FSanimateGeneric();

        private void setBladeRendererState(bool newState)
        {
            for (int i = 0; i < bladeObjects.Count; i++)
            {
                bladeObjects[i].GetComponent<Renderer>().enabled = newState;                
            }
            if (duplicatedBlades)
            {
                for (int i = 0; i < blurObjects.Length; i++)
                {
                    blurObjects[i].gameObject.GetComponent<Renderer>().enabled = !newState;                    
                }
            }
            else
            {
                rotorDisc.GetComponent<Renderer>().enabled = !newState;
            }
        }

        public override void OnStart(PartModule.StartState state)
        {
            base.OnStart(state);

            if (engineID == String.Empty || engineID == "")
                engine = new Firespitter.engine.FSengineWrapper(part);
            else
                engine = new Firespitter.engine.FSengineWrapper(part, engineID);

            if (engine.type == FSengineWrapper.EngineType.FSengine)
            {
                rotationSpeed = engine.fsengine.maxRPM;
            }

            propeller = part.FindModelTransform(propellerName);

            if (engine != null)
            {
                maxThrust = engine.maxThrust;
            }

            if (maxThrust <= 0f) // to avoid division by zero
            {
                maxThrust = 50f;            
            }

            //assign blade meshes so they gan be hidden
            if (useRotorDiscSwap == 1)
            {
                if (duplicatedBlades)
                {
                    blurObjects = part.FindModelTransforms(rotorDiscName);
                    Transform[] bladeTransforms = part.FindModelTransforms(blade1);
                    for (int i = 0; i < bladeTransforms.Length; i++)
                    {
                        bladeObjects.Add(bladeTransforms[i].gameObject);
                    }
                }
                else
                {
                    rotorDisc = part.FindModelTransform(rotorDiscName);
                    if (rotorDisc != null)
                    {
                        rotorDisc.gameObject.GetComponent<Renderer>().enabled = false;

                        if (blade1 != "")
                            bladeNames.Add(blade1);
                        if (blade2 != "")
                            bladeNames.Add(blade2);
                        if (blade3 != "")
                            bladeNames.Add(blade3);
                        if (blade4 != "")
                            bladeNames.Add(blade4);
                        if (blade5 != "")
                            bladeNames.Add(blade5);
                        if (blade6 != "")
                            bladeNames.Add(blade6);

                        for (int i = 0; i < bladeNames.Count; i++)
                        {
                            try
                            {
                                bladeObjects.Add(part.FindModelTransform(bladeNames[i]).gameObject);
                            }
                            catch
                            {
                                Log.info("FSplanePropellerSpinner: Unable to find blade called {0}, disabling swap", bladeNames[i]);
                                useRotorDiscSwap = 0;
                            }
                        }
                    }
                    else
                    {
                        Log.info("FSplanePropellerSpinner: Unable to find rotor disc {0}, disabling swap", rotorDiscName);
                        useRotorDiscSwap = 0;
                    } 
                }

                setBladeRendererState(true);
            }

            if (usesDeployAnimation)
            {
                deployAnimation = part.Modules.OfType<FSanimateGeneric>().FirstOrDefault();
            }
        }

        //public override void OnFixedUpdate()    
        public override void OnUpdate()
        {
            if (!HighLogic.LoadedSceneIsFlight) return; // || !vessel.isActiveVessel

            if (engine != null)
            {

                if (usesDeployAnimation)
                {
                    if (deployAnimationStartsDeployed && deployAnimation.animTime > 0)
                        engine.EngineIgnited = false;
                    else if (!deployAnimationStartsDeployed && deployAnimation.animTime < 1)
                    {
                        engine.EngineIgnited = false;
                    }
                }

                if (engine.type == Firespitter.engine.FSengineWrapper.EngineType.FSengine)
                {
                    rotationSpeed = engine.fsengine.maxRPM;
                    currentRPM = engine.fsengine.RPM / engine.fsengine.maxRPM;
                    Log.dbg("Fsengine found");
                }
                else
                {
                    //check if the engine is running, or the airplane is moving through the air
                    if (!engine.getIgnitionState || engine.getFlameoutState)
                    {                        
                        if (FlightGlobals.ship_srfSpeed > windmillMinAirspeed && vessel.atmDensity > 0.1f)
                            targetRPM = windmillRPM + (windmillRPM * FlightInputHandler.state.mainThrottle); //spins depending on the blade angle
                        else
                            targetRPM = 0f;
                    }
                    else
                    {
                        targetRPM = 1f;
                    }
                    currentRPM = Mathf.Lerp(currentRPM, targetRPM, engine.engineAccelerationSpeed / spinUpTime * TimeWarp.deltaTime);
                }

                if (currentRPM != 0f)
                {
                    Log.dbg("RPM not 0, prop null: {0}, disc null: {1}", (propeller == null), (rotorDisc == null));

                    float finalRotationSpeed = rotationSpeed;
                    if (thrustRPM != 0f)
                    {
                        float normalizedThrustRPM = (engine.finalThrust / maxThrust);
                        smoothedThrustRPM = Mathf.Lerp(smoothedThrustRPM, normalizedThrustRPM, 0.1f);
                        finalRotationSpeed += (thrustRPM * normalizedThrustRPM);
                    }

                    if (useRotorDiscSwap == 1)
                    {
                        // if you are using duplicated blades, you don't use a rotor disc, but blur sections for each blade, and need to slow down the rotor itself.
                        if (!duplicatedBlades && rotorDisc != null) 
                        {
                            if (currentRPM > rotorDiscFadeInStart)
                            {
                                setBladeRendererState(false);
                                rotorDisc.Rotate(Vector3.forward * ((rotorDiscSpeed * 6) * TimeWarp.deltaTime));
                            }
                            else
                            {
                                setBladeRendererState(true);
                                propeller.transform.Rotate(Vector3.forward * ((finalRotationSpeed * 6) * TimeWarp.deltaTime * currentRPM));   
                            }
                        }
                        else
                        {
                            if (currentRPM > rotorDiscFadeInStart)
                            {
                                setBladeRendererState(false);
                                propeller.Rotate(Vector3.forward * ((rotorDiscSpeed * (1 + (5 * engine.fsengine.RPMnormalized))) * TimeWarp.deltaTime));
                            }
                            else
                            {
                                setBladeRendererState(true);
                                propeller.transform.Rotate(Vector3.forward * ((finalRotationSpeed * 6) * TimeWarp.deltaTime * currentRPM));                                
                            }
                        }
                    }
                    else
                    {
                        propeller.transform.Rotate(Vector3.forward * ((finalRotationSpeed * 6) * TimeWarp.deltaTime * currentRPM));
                    }
                }

                
            }
        }
    }
}
/*
Firespitter /L
Copyright 2013-2018, Andreas Aakvik Gogstad (Snjo)
Copyright 2018-2020, LisiasT

    Developers: LisiasT, Snjo

    This file is part of Firespitter.
*/
using UnityEngine;

namespace Firespitter.animation
{
    public class FSmimickRotation : PartModule
    {
        [KSPField]
        public string sourceObjectName = "source";
        [KSPField]
        public string targetObjectName = "target";

        private Transform source;
        private Transform[] targets;

        private bool initialized = false;

        public override void OnStart(PartModule.StartState state)
        {
            if (HighLogic.LoadedSceneIsFlight)
            {
                source = part.FindModelTransform(sourceObjectName);
                targets = part.FindModelTransforms(targetObjectName);

                if (source != null && targets.Length > 0)
                    initialized = true;
            }
        }

        public override void OnUpdate()
        {
            if (initialized)
            {
                for (int i = 0; i < targets.Length; i++)
                {
                    targets[i].localRotation = source.localRotation;
                }
            }
        }
    }
}

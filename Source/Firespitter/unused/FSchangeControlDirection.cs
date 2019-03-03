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
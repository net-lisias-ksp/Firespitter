/*
Firespitter /L
Copyright 2013-2018, Andreas Aakvik Gogstad (Snjo)
Copyright 2018-2020, LisiasT

    Developers: LisiasT

    This file is part of Firespitter.
*/
using UnityEngine;

namespace Firespitter
{
	[KSPAddon(KSPAddon.Startup.Instantly, true)]
	internal class Startup : MonoBehaviour
	{
		private void Start()
		{
			Log.init();
			Log.log("Version {0} running on KSP {1}", Version.Text, KSPe.Util.KSP.Version.Current.ToString());

			try
			{
				KSPe.Util.Compatibility.Check<Startup>(typeof(Version), typeof(Configuration));
				KSPe.Util.Installation.Check<Startup>("Firespitter");
			}
			catch (KSPe.Util.InstallmentException e)
			{
				Log.err(e.ToShortMessage());
				KSPe.Common.Dialogs.ShowStopperAlertBox.Show(e);
			}
		}
	}
}

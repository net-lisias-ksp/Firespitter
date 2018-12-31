/*
Firespitter /L
Copyright 2013-2018, Andreas Aakvik Gogstad (Snjo)
Copyright 2018, LisiasT

    Developers: LisiasT

    This file is part of Firespitter.
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using UnityEngine;
using KSP.UI.Screens;

namespace Firespitter
{
    [KSPAddon(KSPAddon.Startup.MainMenu , true)]
    public class CategoryFilter : MonoBehaviour
    {
        private void addIIfilter()
        {
            //Loading Textures
            Texture2D unselected = new Texture2D(32, 32);
            Texture2D selected = new Texture2D(32, 32);

			try
			{ 
				{
					string fn = KSPe.IO.File<CategoryFilter>.Asset.Solve("Icons/normal.png");
					unselected.LoadImage(File.ReadAllBytes(fn));
				}
				{
					string fn = KSPe.IO.File<CategoryFilter>.Asset.Solve("Icons/selected.png");
					selected.LoadImage(File.ReadAllBytes(fn));
				}
	            RUI.Icons.Selectable.Icon filterIcon = new RUI.Icons.Selectable.Icon("FS_filter_icon", unselected, selected); //Defining filterIcon
	
	            PartCategorizer.Category filter = PartCategorizer.AddCustomFilter(Constants.PLUGIN_ID, Constants.MANUFACTURER_NAME, filterIcon, Color.white);
	
	            //filters for all II parts
				PartCategorizer.AddCustomSubcategoryFilter(filter, "AllParts", string.Format("All {0} Parts", Constants.MANUFACTURER_NAME), filterIcon, o => o.manufacturer == Constants.MANUFACTURER_NAME && !o.title.Contains("(LEGACY)"));
				PartCategorizer.AddCustomSubcategoryFilter(filter, "CommandPods", "Command Pods", filterIcon, o => o.manufacturer == Constants.MANUFACTURER_NAME && o.category.ToString() == "Pods" && !o.title.Contains("(LEGACY)"));
				PartCategorizer.AddCustomSubcategoryFilter(filter, "Control", "Control", filterIcon, o => o.manufacturer == Constants.MANUFACTURER_NAME && o.category.ToString().Contains("Control") && !o.title.Contains("(LEGACY)"));
				PartCategorizer.AddCustomSubcategoryFilter(filter, "Tanks", "Tanks", filterIcon, p => p.resourceInfos.Exists(q => q.resourceName == "Fuel" || q.resourceName == "Oxidizer" || q.resourceName == "Monoprolellent") && p.manufacturer == Constants.MANUFACTURER_NAME && !p.title.Contains("(LEGACY)"));
				PartCategorizer.AddCustomSubcategoryFilter(filter, "Electrical", "Electrical", filterIcon, o => o.manufacturer == Constants.MANUFACTURER_NAME && o.category.ToString() == "Electrical" && !o.title.Contains("(LEGACY)"));
				PartCategorizer.AddCustomSubcategoryFilter(filter, "Engines", "Engines", filterIcon, r => r.title.Contains("Fusion Engine") && r.manufacturer == Constants.MANUFACTURER_NAME);
				PartCategorizer.AddCustomSubcategoryFilter(filter, "Floaters", "Floaters", filterIcon, r => r.title.Contains("Fusion Engine") && r.manufacturer == Constants.MANUFACTURER_NAME);
			}
			catch (Exception e)
			{
				Log.ex(this, e);
			}
		}

        private void Awake()
        {
            GameEvents.onGUIEditorToolbarReady.Add(addIIfilter);
        }
    }
}

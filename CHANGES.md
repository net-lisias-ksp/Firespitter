# Firespitter :: Changes

* 2019-0825 : 7.13.0.1 (Lisias) for KSP >= 1.3.1
	+ Certified to work down to 1.3.1 ! #hurray!
	+ Applying upstream fixes
		- Duplicated resource fields on FSSwitch. 
	+ Fixes from release 7.12.0.1 not approved yet into upstream's.
* 2019-0611 : 7.12.0.1 (Lisias) for KSP >= 1.4.0
	+ Certified to work up to KSP 1.7.1 with MH and BG
	+ Applying proposed pull requests to upstream, not approved yet
		- Adding missing bulkheadProfiles
		- Making firespitter compatible with Principia and other mods that relies on `Part.AddForcesAtPosition`
	+ Using KSPe logging facilities 
	+ Bumping up version to match upstream's
* 2019-0222 : 7.9.0.2 (Lisias) for KSP >= 1.4.0
	+ Certified for KSP from 1.4.0 to 1.6.1
	+ Fixed a dumb mistake on reading positions for the FS3WL Water Launch System 
* 2018-0801 : 7.9.0.1 (Lisias) Unofficial
	+ Moving PluginData back to <KSP_ROOT> where it belongs
	+ Converting some WAV files from ADPCM to PCM, as Unity doesn't support this format.
	+ Ressurecting the FSMoveCraftAtLaunch module (and the respective part, fsmovecraftgadget/FS3WL Water Launch System). #hurray :)
	+ Dirty hack to prevent Null Pointer Exceptions on FSEngine at craft destroy or recovering.
	+ Parts fixed (needs rebalancing however, IMHO):
		-  FSstrutConnectorWire/FS4SW Biplane wire strut connector (Legacy)
		-  FSstrutConnectorWood/FS4SD Biplane wooden beam connector(legacy)
		-  FSdropTank/FS3FD Fuel Drop Tank
	+ Temporary hack to allow the plugin to work while I try to solve an issue with custom/missing shaders

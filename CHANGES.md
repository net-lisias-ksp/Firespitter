# Firespitter :: Changes

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

# Firespitter :: TODO list

* Solve texture problems on some parts:
	+ `[ERR 22:28:41.066] PartCompiler: Cannot replace texture as cannot find texture 'biPlane' to replace`
	+ `[ERR 22:28:41.067] PartCompiler: Cannot replace texture as cannot find texture 'wing_normal_NRM' to replace` 
	+ See KSP.log for details
* Fix the deprecated reference to `ModuleLandingGear` on 'Firespitter/Parts/Wheel/FS_apacheLandingGearFlip/part/FSapacheLandingGearFlip'
* Rebalance Parts
	+ Open Cockpits with 3400ºK of heat resistance? :-D
* Figure out a way to fix the FSWheel.
	+ It kinda works, except on spawn:
		+ `Physics.ClosestPoint can only be used with a BoxCollider, SphereCollider, CapsuleCollider and a convex MeshCollider.`
		+ `[FireSpitter Test 1]: ground contact! - error. Moving Vessel  up 998.158m`
	+ Note: It's something on FSWheel for sure.
* Implement Aircraft attitude on FSmoveCraftAtLaunch (FS3WL Water Launch System)

/*
Firespitter /L
Copyright 2013-2018, Andreas Aakvik Gogstad (Snjo)
Copyright 2018-2020, LisiasT

    Developers: LisiasT

    This file is part of Firespitter.
*/
namespace Firespitter
{
	public static class Configuration
	{
    #if UNITY_2019
        public static readonly int[] Unity = { 2019 };
		public static readonly int[] KSP_Min = { 1, 8, 0 };
		public static readonly int[] KSP_Max = { 999, 999, 999 };
    #elif UNITY_2017
        public static readonly int[] Unity = { 2017 };
		public static readonly int[] KSP_Min = { 1, 4, 0 };
		public static readonly int[] KSP_Max = { 1, 7, 999 };
    #else
        public static readonly int[] Unity = { 5 };
		public static readonly int[] KSP_Min = { 0, 0, 0 };
		public static readonly int[] KSP_Max = { 1, 2, 999 };
    #endif
	}
}

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

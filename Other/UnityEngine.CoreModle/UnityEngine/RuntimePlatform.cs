using System;

namespace UnityEngine
{
	public enum RuntimePlatform
	{
		OSXEditor = 0,
		OSXPlayer = 1,
		WindowsPlayer = 2,
		[Obsolete("WebPlayer export is no longer supported in Unity 5.4+.", true)]
		OSXWebPlayer = 3,
		[Obsolete("Dashboard widget on Mac OS X export is no longer supported in Unity 5.4+.", true)]
		OSXDashboardPlayer = 4,
		[Obsolete("WebPlayer export is no longer supported in Unity 5.4+.", true)]
		WindowsWebPlayer = 5,
		WindowsEditor = 7,
		IPhonePlayer = 8,
		[Obsolete("Xbox360 export is no longer supported in Unity 5.5+.")]
		XBOX360 = 10,
		[Obsolete("PS3 export is no longer supported in Unity >=5.5.")]
		PS3 = 9,
		Android = 11,
		[Obsolete("NaCl export is no longer supported in Unity 5.0+.")]
		NaCl = 12,
		[Obsolete("FlashPlayer export is no longer supported in Unity 5.0+.")]
		FlashPlayer = 0xF,
		LinuxPlayer = 13,
		LinuxEditor = 0x10,
		WebGLPlayer = 17,
		[Obsolete("Use WSAPlayerX86 instead")]
		MetroPlayerX86 = 18,
		WSAPlayerX86 = 18,
		[Obsolete("Use WSAPlayerX64 instead")]
		MetroPlayerX64 = 19,
		WSAPlayerX64 = 19,
		[Obsolete("Use WSAPlayerARM instead")]
		MetroPlayerARM = 20,
		WSAPlayerARM = 20,
		[Obsolete("Windows Phone 8 was removed in 5.3")]
		WP8Player = 21,
		[Obsolete("BlackBerryPlayer export is no longer supported in Unity 5.4+.")]
		BlackBerryPlayer = 22,
		[Obsolete("TizenPlayer export is no longer supported in Unity 2017.3+.")]
		TizenPlayer = 23,
		[Obsolete("PSP2 is no longer supported as of Unity 2018.3")]
		PSP2 = 24,
		PS4 = 25,
		[Obsolete("PSM export is no longer supported in Unity >= 5.3")]
		PSM = 26,
		XboxOne = 27,
		[Obsolete("SamsungTVPlayer export is no longer supported in Unity 2017.3+.")]
		SamsungTVPlayer = 28,
		[Obsolete("Wii U is no longer supported in Unity 2018.1+.")]
		WiiU = 30,
		tvOS = 0x1F,
		Switch = 0x20,
		Lumin = 33
	}
}

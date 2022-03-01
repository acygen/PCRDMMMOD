namespace UnityEngine.Rendering
{
	internal static class CameraEventUtils
	{
		private const CameraEvent k_MinimumValue = CameraEvent.BeforeDepthTexture;

		private const CameraEvent k_MaximumValue = CameraEvent.AfterHaloAndLensFlares;

		public static bool IsValid(CameraEvent value)
		{
			return value >= CameraEvent.BeforeDepthTexture && value <= CameraEvent.AfterHaloAndLensFlares;
		}
	}
}

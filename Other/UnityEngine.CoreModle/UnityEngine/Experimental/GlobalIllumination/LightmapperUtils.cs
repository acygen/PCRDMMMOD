using System;

namespace UnityEngine.Experimental.GlobalIllumination
{
	public static class LightmapperUtils
	{
		public static LightMode Extract(LightmapBakeType baketype)
		{
			return baketype switch
			{
				LightmapBakeType.Realtime => LightMode.Realtime, 
				LightmapBakeType.Mixed => LightMode.Mixed, 
				_ => LightMode.Baked, 
			};
		}

		public static LinearColor ExtractIndirect(Light l)
		{
			return LinearColor.Convert(l.color, l.intensity * l.bounceIntensity);
		}

		public static float ExtractInnerCone(Light l)
		{
			return 2f * Mathf.Atan(Mathf.Tan(l.spotAngle * 0.5f * ((float)Math.PI / 180f)) * 46f / 64f);
		}

		public static void Extract(Light l, ref DirectionalLight dir)
		{
			dir.instanceID = l.GetInstanceID();
			dir.mode = LightMode.Realtime;
			dir.shadow = l.shadows != LightShadows.None;
			dir.direction = l.transform.forward;
			dir.color = LinearColor.Convert(l.color, l.intensity);
			dir.indirectColor = ExtractIndirect(l);
			dir.penumbraWidthRadian = 0f;
		}

		public static void Extract(Light l, ref PointLight point)
		{
			point.instanceID = l.GetInstanceID();
			point.mode = LightMode.Realtime;
			point.shadow = l.shadows != LightShadows.None;
			point.position = l.transform.position;
			point.color = LinearColor.Convert(l.color, l.intensity);
			point.indirectColor = ExtractIndirect(l);
			point.range = l.range;
			point.sphereRadius = 0f;
			point.falloff = FalloffType.Legacy;
		}

		public static void Extract(Light l, ref SpotLight spot)
		{
			spot.instanceID = l.GetInstanceID();
			spot.mode = LightMode.Realtime;
			spot.shadow = l.shadows != LightShadows.None;
			spot.position = l.transform.position;
			spot.orientation = l.transform.rotation;
			spot.color = LinearColor.Convert(l.color, l.intensity);
			spot.indirectColor = ExtractIndirect(l);
			spot.range = l.range;
			spot.sphereRadius = 0f;
			spot.coneAngle = l.spotAngle * ((float)Math.PI / 180f);
			spot.innerConeAngle = ExtractInnerCone(l);
			spot.falloff = FalloffType.Legacy;
		}

		public static void Extract(Light l, ref RectangleLight rect)
		{
			rect.instanceID = l.GetInstanceID();
			rect.mode = LightMode.Realtime;
			rect.shadow = l.shadows != LightShadows.None;
			rect.position = l.transform.position;
			rect.orientation = l.transform.rotation;
			rect.color = LinearColor.Convert(l.color, l.intensity);
			rect.indirectColor = ExtractIndirect(l);
			rect.range = l.range;
			rect.width = 0f;
			rect.height = 0f;
		}

		public static void Extract(Light l, ref DiscLight disc)
		{
			disc.instanceID = l.GetInstanceID();
			disc.mode = LightMode.Realtime;
			disc.shadow = l.shadows != LightShadows.None;
			disc.position = l.transform.position;
			disc.orientation = l.transform.rotation;
			disc.color = LinearColor.Convert(l.color, l.intensity);
			disc.indirectColor = ExtractIndirect(l);
			disc.range = l.range;
			disc.radius = 0f;
		}
	}
}

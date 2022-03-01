using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	[NativeHeader("Runtime/Export/LineUtility.bindings.h")]
	public sealed class LineUtility
	{
		public static void Simplify(List<Vector3> points, float tolerance, List<int> pointsToKeep)
		{
			if (points == null)
			{
				throw new ArgumentNullException("points");
			}
			if (pointsToKeep == null)
			{
				throw new ArgumentNullException("pointsToKeep");
			}
			GeneratePointsToKeep3D(points, tolerance, pointsToKeep);
		}

		public static void Simplify(List<Vector3> points, float tolerance, List<Vector3> simplifiedPoints)
		{
			if (points == null)
			{
				throw new ArgumentNullException("points");
			}
			if (simplifiedPoints == null)
			{
				throw new ArgumentNullException("simplifiedPoints");
			}
			GenerateSimplifiedPoints3D(points, tolerance, simplifiedPoints);
		}

		public static void Simplify(List<Vector2> points, float tolerance, List<int> pointsToKeep)
		{
			if (points == null)
			{
				throw new ArgumentNullException("points");
			}
			if (pointsToKeep == null)
			{
				throw new ArgumentNullException("pointsToKeep");
			}
			GeneratePointsToKeep2D(points, tolerance, pointsToKeep);
		}

		public static void Simplify(List<Vector2> points, float tolerance, List<Vector2> simplifiedPoints)
		{
			if (points == null)
			{
				throw new ArgumentNullException("points");
			}
			if (simplifiedPoints == null)
			{
				throw new ArgumentNullException("simplifiedPoints");
			}
			GenerateSimplifiedPoints2D(points, tolerance, simplifiedPoints);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("LineUtility_Bindings::GeneratePointsToKeep3D", IsThreadSafe = true)]
		internal static extern void GeneratePointsToKeep3D(object pointsList, float tolerance, object pointsToKeepList);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("LineUtility_Bindings::GeneratePointsToKeep2D", IsThreadSafe = true)]
		internal static extern void GeneratePointsToKeep2D(object pointsList, float tolerance, object pointsToKeepList);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("LineUtility_Bindings::GenerateSimplifiedPoints3D", IsThreadSafe = true)]
		internal static extern void GenerateSimplifiedPoints3D(object pointsList, float tolerance, object simplifiedPoints);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("LineUtility_Bindings::GenerateSimplifiedPoints2D", IsThreadSafe = true)]
		internal static extern void GenerateSimplifiedPoints2D(object pointsList, float tolerance, object simplifiedPoints);
	}
}

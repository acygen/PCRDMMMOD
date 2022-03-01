using System;

namespace UnityEngine.Experimental.Rendering
{
	[Flags]
	public enum SortFlags
	{
		None = 0x0,
		SortingLayer = 0x1,
		RenderQueue = 0x2,
		BackToFront = 0x4,
		QuantizedFrontToBack = 0x8,
		OptimizeStateChanges = 0x10,
		CanvasOrder = 0x20,
		RendererPriority = 0x40,
		CommonOpaque = 0x3B,
		CommonTransparent = 0x17
	}
}

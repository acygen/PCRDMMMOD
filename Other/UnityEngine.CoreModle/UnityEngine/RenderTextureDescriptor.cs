using UnityEngine.Rendering;

namespace UnityEngine
{
	public struct RenderTextureDescriptor
	{
		private int _depthBufferBits;

		private static int[] depthFormatBits = new int[3] { 0, 16, 24 };

		private RenderTextureCreationFlags _flags;

		public int width { get; set; }

		public int height { get; set; }

		public int msaaSamples { get; set; }

		public int volumeDepth { get; set; }

		public RenderTextureFormat colorFormat { get; set; }

		public int depthBufferBits
		{
			get
			{
				return depthFormatBits[_depthBufferBits];
			}
			set
			{
				if (value <= 0)
				{
					_depthBufferBits = 0;
				}
				else if (value <= 16)
				{
					_depthBufferBits = 1;
				}
				else
				{
					_depthBufferBits = 2;
				}
			}
		}

		public TextureDimension dimension { get; set; }

		public ShadowSamplingMode shadowSamplingMode { get; set; }

		public VRTextureUsage vrUsage { get; set; }

		public RenderTextureCreationFlags flags => _flags;

		public RenderTextureMemoryless memoryless { get; set; }

		public bool sRGB
		{
			get
			{
				return (_flags & RenderTextureCreationFlags.SRGB) != 0;
			}
			set
			{
				SetOrClearRenderTextureCreationFlag(value, RenderTextureCreationFlags.SRGB);
			}
		}

		public bool useMipMap
		{
			get
			{
				return (_flags & RenderTextureCreationFlags.MipMap) != 0;
			}
			set
			{
				SetOrClearRenderTextureCreationFlag(value, RenderTextureCreationFlags.MipMap);
			}
		}

		public bool autoGenerateMips
		{
			get
			{
				return (_flags & RenderTextureCreationFlags.AutoGenerateMips) != 0;
			}
			set
			{
				SetOrClearRenderTextureCreationFlag(value, RenderTextureCreationFlags.AutoGenerateMips);
			}
		}

		public bool enableRandomWrite
		{
			get
			{
				return (_flags & RenderTextureCreationFlags.EnableRandomWrite) != 0;
			}
			set
			{
				SetOrClearRenderTextureCreationFlag(value, RenderTextureCreationFlags.EnableRandomWrite);
			}
		}

		public bool bindMS
		{
			get
			{
				return (_flags & RenderTextureCreationFlags.BindMS) != 0;
			}
			set
			{
				SetOrClearRenderTextureCreationFlag(value, RenderTextureCreationFlags.BindMS);
			}
		}

		internal bool createdFromScript
		{
			get
			{
				return (_flags & RenderTextureCreationFlags.CreatedFromScript) != 0;
			}
			set
			{
				SetOrClearRenderTextureCreationFlag(value, RenderTextureCreationFlags.CreatedFromScript);
			}
		}

		internal bool useDynamicScale
		{
			get
			{
				return (_flags & RenderTextureCreationFlags.DynamicallyScalable) != 0;
			}
			set
			{
				SetOrClearRenderTextureCreationFlag(value, RenderTextureCreationFlags.DynamicallyScalable);
			}
		}

		public RenderTextureDescriptor(int width, int height)
			: this(width, height, RenderTextureFormat.Default, 0)
		{
		}

		public RenderTextureDescriptor(int width, int height, RenderTextureFormat colorFormat)
			: this(width, height, colorFormat, 0)
		{
		}

		public RenderTextureDescriptor(int width, int height, RenderTextureFormat colorFormat, int depthBufferBits)
		{
			this = default(RenderTextureDescriptor);
			this.width = width;
			this.height = height;
			volumeDepth = 1;
			msaaSamples = 1;
			this.colorFormat = colorFormat;
			this.depthBufferBits = depthBufferBits;
			dimension = TextureDimension.Tex2D;
			shadowSamplingMode = ShadowSamplingMode.None;
			vrUsage = VRTextureUsage.None;
			_flags = RenderTextureCreationFlags.AutoGenerateMips | RenderTextureCreationFlags.AllowVerticalFlip;
			memoryless = RenderTextureMemoryless.None;
		}

		private void SetOrClearRenderTextureCreationFlag(bool value, RenderTextureCreationFlags flag)
		{
			if (value)
			{
				_flags |= flag;
			}
			else
			{
				_flags &= ~flag;
			}
		}
	}
}

using UnityEngine.Internal;

namespace UnityEngine
{
	public class TouchScreenKeyboard
	{
		public enum Status
		{
			Visible,
			Done,
			Canceled,
			LostFocus
		}

		public string text
		{
			get
			{
				return string.Empty;
			}
			set
			{
			}
		}

		public static bool hideInput
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		public bool active
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		public bool done => true;

		public bool wasCanceled => false;

		public Status status => Status.Done;

		private static Rect area => default(Rect);

		private static bool visible => false;

		public static bool isSupported => false;

		public bool canGetSelection => false;

		public bool canSetSelection => false;

		public RangeInt selection
		{
			get
			{
				return new RangeInt(0, 0);
			}
			set
			{
			}
		}

		public int characterLimit
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		public static TouchScreenKeyboard Open(string text, [DefaultValue("TouchScreenKeyboardType.Default")] TouchScreenKeyboardType keyboardType, [DefaultValue("true")] bool autocorrection, [DefaultValue("false")] bool multiline, [DefaultValue("false")] bool secure, [DefaultValue("false")] bool alert, [DefaultValue("\"\"")] string textPlaceholder, [DefaultValue("0")] int characterLimit)
		{
			return null;
		}

		[ExcludeFromDocs]
		public static TouchScreenKeyboard Open(string text, TouchScreenKeyboardType keyboardType, bool autocorrection, bool multiline, bool secure, bool alert, string textPlaceholder)
		{
			return null;
		}

		[ExcludeFromDocs]
		public static TouchScreenKeyboard Open(string text, TouchScreenKeyboardType keyboardType, bool autocorrection, bool multiline, bool secure, bool alert)
		{
			return null;
		}

		[ExcludeFromDocs]
		public static TouchScreenKeyboard Open(string text, TouchScreenKeyboardType keyboardType, bool autocorrection, bool multiline, bool secure)
		{
			return null;
		}

		[ExcludeFromDocs]
		public static TouchScreenKeyboard Open(string text, TouchScreenKeyboardType keyboardType, bool autocorrection, bool multiline)
		{
			return null;
		}

		[ExcludeFromDocs]
		public static TouchScreenKeyboard Open(string text, TouchScreenKeyboardType keyboardType, bool autocorrection)
		{
			return null;
		}

		[ExcludeFromDocs]
		public static TouchScreenKeyboard Open(string text, TouchScreenKeyboardType keyboardType)
		{
			return null;
		}

		[ExcludeFromDocs]
		public static TouchScreenKeyboard Open(string text)
		{
			return null;
		}
	}
}

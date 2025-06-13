using System;
using UnityEngine;

namespace Utils
{
	public enum EColor
	{
		None,
		Red,
		Green,
		DarkGreen,
		Blue,
		Magenta,
		Cyan,
		White,
		Black,
		Yellow,
		Orange,
		Violet
	}

	[AttributeUsage(AttributeTargets.Field)]
	public class FieldColorAttribute : PropertyAttribute
	{
		public readonly Color? LabelFont;
		public readonly Color? LabelBg;
		public readonly Color? Content;
		public readonly Color? ContentBg;

		public FieldColorAttribute(EColor color, bool colorContent = false)
		{
			LabelBg = GetColor(color);
			LabelFont = Color.black;
			ContentBg = colorContent ? LabelBg : null;
			Content = Color.white;

			if (LabelBg.HasValue)
			{
				var col = LabelBg.Value;
				col.a = 0.7f;
				LabelBg = col;
			}
		}

		private static Color? GetColor(EColor color)
		{
			switch (color)
			{
				case EColor.None:
					return null;
				case EColor.Red:
					return new Color(1f, 0.2f, 0f);
				case EColor.Orange:
					return new Color(1f, 0.5f, 0);
				case EColor.Green:
					return Color.green;
				case EColor.DarkGreen:
					return new Color(0, 0.6f, 0);
				case EColor.Violet:
					return new Color(0.6f, 0.7f, 1f);
				case EColor.Blue:
					return new Color(0, 0.5f, 1f);
				case EColor.Yellow:
					return Color.yellow;
				case EColor.Magenta:
					return Color.magenta;
				case EColor.Cyan:
					return Color.cyan;
				case EColor.White:
					return Color.white;
				case EColor.Black:
					return Color.black;
				default:
					throw new Exception("No color for " + color);
			}
		}
	}
}
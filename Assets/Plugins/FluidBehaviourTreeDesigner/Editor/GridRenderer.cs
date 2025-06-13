using UnityEngine;

namespace FluidBehaviourTreeDesigner {

	public class GridRenderer {
		private Texture2D _gridTex;

		private static int width => 120;
		private static int height => 120;
		public static Vector2 step => new Vector2(width / 10, height / 10);

		// Generates a single tile of the grid texture
		private void GenerateGrid() {
			_gridTex = new Texture2D(width, height)
			{
				hideFlags = HideFlags.DontSave
			};

			var bg = new Color(0.64f, 0.64f, 0.64f);

			var dark = Color.Lerp(bg, Color.black, 0.15f);
			var darkIntersection = Color.Lerp(bg, Color.black, 0.2f);

			var light = Color.Lerp(bg, Color.black, 0.05f);
			var lightIntersection = Color.Lerp(bg, Color.black, 0.1f);
			
			for (var x = 0; x < width; x ++) {

				for (var y = 0; y < height; y ++) {
					
					// Left Top edge, dark intersection color
					if (x == 0 && y == 0)
						_gridTex.SetPixel(x, y, darkIntersection);

					// Left and Top edges, dark color
					else if (x == 0 || y == 0)
						_gridTex.SetPixel(x, y, dark);

					// Finer grid intersection color
					else if (x % step.x == 0 && y % step.y == 0)
						_gridTex.SetPixel(x, y, lightIntersection);

					// Finer grid color
					else if (x % step.x == 0 || y % step.y == 0)
						_gridTex.SetPixel(x, y, light);

					// Background
					else
						_gridTex.SetPixel(x, y, bg);
				}

			}
			
			_gridTex.Apply();
		}
		
		public void Draw(Vector2 scrollPoint, Rect canvas) {
			if (!_gridTex) GenerateGrid ();
			
			var yOffset = scrollPoint.y % _gridTex.height;
			var yStart = scrollPoint.y - yOffset;
			var yEnd = scrollPoint.y + canvas.height + yOffset;
			
			var xOffset = scrollPoint.x % _gridTex.width;
			var xStart = scrollPoint.x - xOffset;
			var xEnd = scrollPoint.x + canvas.width + xOffset;
			
			for (var x = xStart; x < xEnd; x += _gridTex.width) {
				for (var y = yStart; y < yEnd; y += _gridTex.height) {
					GUI.DrawTexture(new Rect(x, y, _gridTex.width, _gridTex.height), _gridTex);
				}
			}
		}

	}
	
}
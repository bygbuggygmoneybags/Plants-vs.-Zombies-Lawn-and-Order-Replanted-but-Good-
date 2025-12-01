using Godot;
using System;

namespace pvzlawnandorder
{
	public partial class Tiles : TileMapLayer
	{
		[Export] private int tileSourceId = 0;
		[Export] private Vector2I tileAtlasCoords = new Vector2I(0,0);
		[Export] private int gridWidth = 9;
		[Export] private int gridHeight = 5;

		[Export] private TileSet tileSetResource = "res://TileSet.tres";



		public override void _Ready()
		{
			if (TileSet == null)
			{
				if (tileSetResource != null)
			{
					TileSet = tileSetResource;
				} else 
				{
					GD.PrintErr("No Tile set assigned to TileMapLayer");
				}
			}

			for (int x = 0; x < gridWidth; x++)
			{
				for (int y = 0; y < gridHeight; y++)
				{
					Vector2I cellCoords = new Vector2I(x, y);
					SetCell(cellCoords, tileSourceId, tileAtlasCoords);
					TileData tileData = GetCellTileData(coords);
					if (tileData != null)
					{
						tileData.SetCustomData("plantable", true);
					}
				}
			}
		}
	}
}

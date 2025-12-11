using Godot;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;

namespace pvzlawnandorder
{
	public partial class Tiles : TileMapLayer
	{
		[Export] private int tileSourceId;
		[Export] private Vector2I tileAtlasCoords = new Vector2I(0,0);
		[Export] private int gridWidth = 9;
		[Export] private int gridHeight = 5;
		private GameManager gameManager;

		[Export] private TileSet tileSetResource;
		public HashSet<Vector2I> PlantableCells { get; private set; } = new();


		public override void _UnhandledInput(InputEvent @event)
		{
			if(@event is InputEventMouseButton mouseButtonEvent && mouseButtonEvent.ButtonIndex == MouseButton.Left && mouseButtonEvent.Pressed)
			{
				Vector2 mousePos = GetViewport().GetMousePosition();
				Vector2 worldPos = GetGlobalMousePosition();
				Vector2I cell = LocalToMap(worldPos);

				gameManager.TryPlacePlant(cell);
			}
		}
		public override void _Ready()
		{
			gameManager = GetParent<GameManager>();
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
					tileAtlasCoords = (x + y) % 2 == 0 ? new Vector2I(0, 0) : new Vector2I(1, 0);
					SetCell(cellCoords, tileSourceId, tileAtlasCoords);
					TileData tileData = GetCellTileData(cellCoords);
					PlantableCells.Add(cellCoords);
				}
			}
		}
	}
}

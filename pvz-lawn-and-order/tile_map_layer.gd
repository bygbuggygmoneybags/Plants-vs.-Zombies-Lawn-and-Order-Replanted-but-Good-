extends TileMapLayer

@export var tile_source_id: int = 0 
@export var tile_atlas_coords: Vector2i = Vector2i(0, 0) 
@export var grid_width: int = 9
@export var grid_height: int = 5

func _ready():
	if not tile_set:
		tile_set = preload("res://TileSet.tres")
	
	for x in range(grid_width):
		for y in range(grid_height):
			set_cell(Vector2i(x,y), tile_source_id, tile_atlas_coords)
			if (tile_source_id == 1):
				tile_source_id = 0
			else:
				tile_source_id = 1

extends Node2D

@onready var oneDashOne = get_node("One-One")
@onready var menu = get_node("MenuButton")

# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	menu.pressed.connect(backToMenu)

func backToMenu():
	get_tree().change_scene_to_file("res://Scenes/menu.tscn")

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta: float) -> void:
	pass

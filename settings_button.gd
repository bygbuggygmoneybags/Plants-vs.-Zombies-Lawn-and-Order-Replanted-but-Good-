extends Control

signal Pressed()
@onready var menu = get_node('Accounts')
# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	menu.pressed.connect(on_account_pressed)

func  on_account_pressed():
	get_tree().change_scene_to_file("res://Account_Menu.tscn")
	

extends Node2D
@onready var account = get_node("Camera2D/AccountButton")
@onready var adventure = get_node("Camera2D/AdventureButton")
@onready var minigame = get_node("Camera2D/Mini-GameButton")
@onready var puzzle = get_node("Camera2D/PuzzleButton")
@onready var survival = get_node("Camera2D/SurvivalButton")
@onready var options = get_node("Camera2D/OptionsButton")
@onready var error = get_node("Camera2D/error")
@onready var optionMenu = get_node("Camera2D/MenuBar")


# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	account.pressed.connect(account_pressed)
	adventure.pressed.connect(adventure_pressed)
	minigame.pressed.connect(minigme_pressed)
	puzzle.pressed.connect(puzzle_pressed)
	survival.pressed.connect(survival_pressed)
	options.pressed.connect(options_pressed)

func account_pressed():
	error_popup()


func adventure_pressed():
	get_tree().change_scene_to_file("res://Scenes/level_select.tscn")


func minigme_pressed():
	error_popup()


func puzzle_pressed():
	error_popup()


func survival_pressed():
	error_popup()

func options_pressed():
	optionMenu.set("visiblity", true)


func error_popup():
	error.popup()
	#error.set_text("This isn't implemented. Sorry for the inconvience. :(")

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta: float) -> void:
	pass

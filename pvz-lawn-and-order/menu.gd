extends Node2D
@onready var account = get_node("MainMenu/AccountButton")
@onready var adventure = get_node("MainMenu/AdventureButton")
@onready var minigame = get_node("MainMenu/Mini-GameButton")
@onready var puzzle = get_node("MainMenu/PuzzleButton")
@onready var survival = get_node("MainMenu/SurvivalButton")
@onready var options = get_node("MainMenu/OptionsButton")
@onready var error = get_node("MainMenu/error")
@onready var optionMenu = get_node("MainMenu/GridContainer/MenuBar")
@onready var back = get_node("MainMenu/GridContainer/MenuBar/Back")
@onready var music = get_node("MainMenu/GridContainer/MenuBar/Music")
@onready var sound = get_node("MainMenu/GridContainer/MenuBar/Sound")
@onready var accel = get_node("MainMenu/GridContainer/MenuBar/3d Acceleration")
@onready var fullscreen = get_node("MainMenu/GridContainer/MenuBar/Fullscreen")


# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	account.pressed.connect(account_pressed)
	adventure.pressed.connect(adventure_pressed)
	minigame.pressed.connect(minigme_pressed)
	puzzle.pressed.connect(puzzle_pressed)
	survival.pressed.connect(survival_pressed)
	options.pressed.connect(options_pressed)
	back.pressed.connect(back_pressed)
	music.changed.connect(music_slider)
	sound.changed.connect(sound_slider)
	accel.pressed.connect(accel_checked)
	fullscreen.pressed.connect(fullscreen_checked)
	fullscreen.pressed.connect(fullscreen_unchecked)
	optionMenu.set("visible",false)

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

func options_pressed() -> void:
	optionMenu.set("visible", true)


func error_popup():
	error.popup()


func back_pressed():
	set("visible",false)
	
func music_slider():
	pass
func sound_slider():
	pass
func accel_checked():
	pass
func fullscreen_checked():
	DisplayServer.window_set_mode(DisplayServer.WINDOW_MODE_FULLSCREEN)
	pass
func fullscreen_unchecked():
	DisplayServer.window_set_mode(DisplayServer.WINDOW_MODE_WINDOWED)

extends MenuBar


# Called when the node enters the scene tree for the first time.
@onready var back = get_node("Back")
@onready var music = get_node("Music")
@onready var sound = get_node("Sound")
@onready var accel = get_node("3d Acceleration")
@onready var fullscreen = get_node("Fullscreen")
func _ready() -> void:
	back.pressed.connect(back_pressed)
	music.changed.connect(music_slider)
	sound.changed.connect(sound_slider)
	accel.pressed.connect(accel_checked)
	fullscreen.pressed.connect(fullscreen_checked)
	pass # Replace with function body.

func back_pressed():
	set("visible",false)
	
func music_slider():
	pass
func sound_slider():
	pass
func accel_checked():
	pass
func fullscreen_checked():
	pass

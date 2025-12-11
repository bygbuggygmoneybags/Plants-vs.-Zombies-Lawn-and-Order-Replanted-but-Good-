extends Area2D

var end_y
var zf
var vel = Vector2(0.3, -6)
var score = 0
@onready var tween = create_tween()
signal score_changed(new_score: int)


# Called when the node enters the scene tree for the first time.
func _ready():
	pass


func init_sky():
	global_position.x = randi_range(0, 550)
	global_position.y = -20
	end_y = randi_range(0, 300)
	self.tween.tween_property(self, "global_position:y", end_y, end_y / 100)


func throw():
	self.position.x += vel.x
	self.position.y += vel.y
	vel.y += 0.3
	if self.global_position.y - end_y >= 4 && vel.y > 0:
		self.tween.kill()


func init_sunflower(x, y):
	end_y = y + 40
	global_position.x = x
	zf = 1 if randi() % 2 else -1
	self.vel.x = zf * self.vel.x
	tween = tween.set_loops()
	self.tween.tween_callback(self.throw).set_delay(0.01)

func collected_success():
	score+=25
	queue_free()
	score_changed.emit(score)


func _on_input_event(_viewport, event, _shape_idx):
	if event is InputEventMouseButton && event.pressed:
		self.tween.kill()
		self.tween = create_tween()
		self.tween.tween_callback(self.collected_success)



func _on_disappeartimer_timeout():
	queue_free()

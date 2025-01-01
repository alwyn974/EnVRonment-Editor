extends Node


# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	pass # Replace with function body.


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta: float) -> void:
	pass
	
func _release(data) -> void:
	print(data.mode)
	
func _selected(data) -> void:
	print(data.mode)

func _dragged(data) -> void:
	print(data.mode)

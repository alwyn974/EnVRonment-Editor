extends Node3D

@export var gizmo: Gizmo3D

func _unhandled_input(event: InputEvent) -> void:
	if gizmo.hovering or gizmo.editing:
		return

	if event is InputEventMouseButton and event.button_index == MOUSE_BUTTON_LEFT and event.pressed:
		var camera = get_viewport().get_camera_3d()
		var dir = camera.project_ray_normal(event.position)
		var from = camera.project_ray_origin(event.position)
		var result = get_world_3d().direct_space_state.intersect_ray(PhysicsRayQueryParameters3D
			.create(from, from + dir * 1000.0))

		if result.is_empty():
			return

		var collider = result["collider"]
		gizmo.target = collider.get_parent()

using Godot;
using System;
using Gizmo3DPlugin;

public partial class NewScript : Node
{
	[Export]
	Gizmo3D gizmo;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		gizmo.GizmoDragged += OnGizmoDragged;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void OnGizmoDragged(Gizmo3D.EditData data)
	{
		GD.Print("Updated position: " + data.TargetGlobal);
	}
}

using EnVRonmentEditor;
using Godot;

namespace Gizmo3DPlugin;

public partial class GizmoSelect : Node3D
{
    [Export]
    public Gizmo3DCSharp Gizmo { get; private set; }

    public override void _UnhandledInput(InputEvent @event)
    {
        // Prevent object picking is user is interacting with the gizmo
        if (Gizmo.Hovering || Gizmo.Editing)
            return;
        if (@event is InputEventMouseButton button && button.ButtonIndex == MouseButton.Left && button.Pressed)
        {
            var camera = GetViewport().GetCamera3D();
            var dir = camera.ProjectRayNormal(button.Position);
            var from = camera.ProjectRayOrigin(button.Position);
            var result = GetWorld3D().DirectSpaceState.IntersectRay(new PhysicsRayQueryParameters3D()
            {
                From = from,
                To = from + dir * 1000.0f
            });
            // TODO: handle deselection
            if (result.Count == 0)
            {
                // Gizmo.Target = null;
                return;
            }
            Node collider = (Node) result["collider"];
            Gizmo.Target = collider.GetParent<Node3D>();
            // Gizmo.Target = GetInspectableNode(collider);
        }
    }
    
    public static Node3DInspectable GetInspectableNode(Node node)
    {
        if (node is Node3DInspectable inspectable)
            return inspectable;
        if (node.GetParent() == null)
            return null;
        return GetInspectableNode(node.GetParent());
    }
}
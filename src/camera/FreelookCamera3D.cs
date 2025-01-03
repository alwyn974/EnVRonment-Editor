using Gizmo3DPlugin;
using Godot;

namespace EnVRonmentEditor.camera;

[GlobalClass]
public partial class FreelookCamera3D : Camera3D
{
    [Export(PropertyHint.Range, "0,10,0.01")]
    public float Sensitivity { get; set; } = 3;

    [Export(PropertyHint.Range, "0,1000,0.1")]
    public float DefaultVelocity { get; set; } = 5;

    [Export(PropertyHint.Range, "0,10,0.01")]
    public float SpeedScale { get; set; } = 1.17f;

    [Export(PropertyHint.Range, "1,100,0.1")]
    public float BoostSpeedMultiplier { get; set; } = 3.0f;

    [Export] public float MaxSpeed { get; set; } = 1000;

    [Export] public float MinSpeed { get; set; } = 0.2f;

    [Export]
    public Gizmo3DCSharp Gizmo { get; private set; }
    [Export]
    public Label Message { get; private set; }
    
    private float _velocity;
    private Camera3D _mainCam;

    public override void _Ready()
    {
        _velocity = DefaultVelocity;
        _mainCam = GetViewport().GetCamera3D();
    }

    public override void _Process(double delta)
    {
        if (!Current)
        {
            GlobalPosition = _mainCam.GlobalPosition;
            GlobalRotation = _mainCam.GlobalRotation;
            return;
        }

        var direction = new Vector3(
            Input.IsPhysicalKeyPressed(Key.D) ? 1 : 0 - (Input.IsPhysicalKeyPressed(Key.A) ? 1 : 0),
            Input.IsPhysicalKeyPressed(Key.E) ? 1 : 0 - (Input.IsPhysicalKeyPressed(Key.Q) ? 1 : 0),
            Input.IsPhysicalKeyPressed(Key.S) ? 1 : 0 - (Input.IsPhysicalKeyPressed(Key.W) ? 1 : 0)
        ).Normalized();

        if (Input.IsPhysicalKeyPressed(Key.Shift))
            Translate(direction * _velocity * (float)delta * BoostSpeedMultiplier);
        else
            Translate(direction * _velocity * (float)delta);
        
        if (Message == null)
            return;
        Message.Visible = Gizmo.Editing;
        if (!Gizmo.Editing)
            return;
        Message.Position = GetViewport().GetMousePosition() + new Vector2(16, 16);
        Message.Text = Gizmo.Message;
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if (Input.GetMouseMode() == Input.MouseModeEnum.Captured)
        {
            if (@event is InputEventMouseMotion motionEvent)
            {
                Rotation = new Vector3(
                    Mathf.Clamp(Rotation.X - motionEvent.Relative.Y / 1000 * Sensitivity, -Mathf.Pi / 2, Mathf.Pi / 2),
                    Rotation.Y - motionEvent.Relative.X / 1000 * Sensitivity,
                    Rotation.Z
                );
            }
        }

        if (@event is InputEventMouseButton buttonEvent)
        {
            switch (buttonEvent.ButtonIndex)
            {
                case MouseButton.Right:
                    Input.SetMouseMode(buttonEvent.Pressed ? Input.MouseModeEnum.Captured : Input.MouseModeEnum.Visible);
                    Gizmo.SetProcessUnhandledInput(!buttonEvent.Pressed);
                    break;
                case MouseButton.WheelUp: // increase fly velocity
                    _velocity = Mathf.Clamp(_velocity * SpeedScale, MinSpeed, MaxSpeed);
                    break;
                case MouseButton.WheelDown: // decrease fly velocity
                    _velocity = Mathf.Clamp(_velocity / SpeedScale, MinSpeed, MaxSpeed);
                    break;
            }
        }

        // Toggle cameras
        // if (@event is InputEventKey keyEvent && keyEvent.Pressed)
        // {
        //     if (keyEvent.Keycode == Key.Minus)
        //     {
        //         _mainCam.Current = !_mainCam.Current;
        //         Current = !_mainCam.Current;
        //     }
        // }
    }
}
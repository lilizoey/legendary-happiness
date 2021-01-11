using Godot;
using System;
using System.Linq;

public class Player : Node2D
{
    [Export]
    private float _velocity = 12.0f;
    [Export]
    private NodePath _navigation2DPath = "";
    private Navigation2D _currentNavigation;
    private Vector2[] _currentPath;
    private Vector2 _start;
    private Vector2 _destination;
    private bool _moving = false;

    public override void _Ready()
    {
        _currentNavigation = GetNodeOrNull<Navigation2D>(_navigation2DPath);
        _destination = GlobalPosition;
        _start = GlobalPosition;
    }

    public bool MoveTo(Vector2 dest) 
    {
        _start = GlobalPosition;
        _currentPath = _currentNavigation?.GetSimplePath(_start, dest);
        _destination = _currentPath[0];
        _moving = true;
        GD.Print("Moving from: ", _start, " to, ", _destination, " with target: ", dest);
        return true;
    }

    public override void _Input(InputEvent inputEvent)
    {
        if (inputEvent is InputEventMouseButton)
        {
            MoveTo(GetGlobalMousePosition());
        }
    }

    public override void _Process(float delta)
    {
        if (_moving)
        {
            GlobalPosition += (_destination - _start).Normalized() * _velocity * delta;
        }

        if (_moving && (_destination - _start).Length() <= (GlobalPosition - _start).Length())
        {
            if (_currentPath.Length >= 1)
            {
                _destination = _currentPath[0];
                _currentPath = _currentPath.Skip(1).ToArray();
            }
            else
            {
                _moving = false;
            }
        }
    }
}

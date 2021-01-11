using Godot;
using System;

public class SetToOrigin : Navigation2D
{
    public override void _Ready()
    {
        GlobalPosition = new Vector2(0,0);
    }
}

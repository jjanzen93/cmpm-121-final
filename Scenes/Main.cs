using Godot;
using System;

public partial class Main : Node2D
{
	private float _speed = 5;

	private Sprite2D _sprite;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_sprite = GetNode<Sprite2D>("Sprite2D");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (Input.IsKeyPressed(Key.W)) {
			_sprite.Position += new Vector2(0, -_speed);
		}
		if (Input.IsKeyPressed(Key.S)) {
			_sprite.Position += new Vector2(0, _speed);
		}
		if (Input.IsKeyPressed(Key.A)) {
			_sprite.Position += new Vector2(-_speed, 0);
		}
		if (Input.IsKeyPressed(Key.D)) {
			_sprite.Position += new Vector2(_speed, 0);
		}
	}
}

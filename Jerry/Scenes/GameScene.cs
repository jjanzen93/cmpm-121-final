using Godot;
using System;

public partial class GameScene : Node2D
{
	[Export]
	public Label pointlabel {get;set;}
	public int points = 0;
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	public void increase_points(int num){
		points += num;
		pointlabel.Text = "points: "+ points.ToString();
	}
}

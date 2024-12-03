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

	public override void _Process(double delta)
	{
	}
	public void increase_points(int num){
		points += num;
		pointlabel.Text = "points: "+ points.ToString();
		if (points >= 10){
			pointlabel.Text = "you win!";
		}
	}
}

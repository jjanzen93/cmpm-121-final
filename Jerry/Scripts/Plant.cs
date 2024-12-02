using Godot;
using System;

public partial class Plant : Node2D
{
	private int waterRequired;
	private int sunRequired;
	private bool adjacentNeeded;
	private int adjacentType;
	private int type;
	[Export]
	public Sprite2D sprite {get; set; }
	[Export]
	public Label label {get; set; }
	private int growLevel = 1;
	private int maxGrowLevel = 3;
	private bool adjacentSatisfied;
	// Called when the node enters the scene tree for the first time.
	
	public void constructor(int sun_required, int water_required, bool adjacent_needed, int adjacent_type, int type, string newSprite){
		this.sunRequired = sun_required;
			this.waterRequired = water_required;
			this.adjacentNeeded = adjacent_needed;
			this.adjacentType = adjacent_type;
			this.type = type;
			sprite.Texture = GD.Load<Texture2D>(newSprite);
			this.adjacentSatisfied = !adjacent_needed;
			
	}
	public override void _Ready()
	{

	}
	
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	public void update_sun(int sun, int water){
		GD.Print(this.adjacentSatisfied);
		if(sun >= this.sunRequired && water >= this.waterRequired && this.adjacentSatisfied){
			grow();
		}
		label.Text = $"sun:{sun}, water:{water}";
	}
	private void grow(){
		if (this.growLevel >= this.maxGrowLevel){
			return;
		}
		this.growLevel += 1;
		this.sprite.Scale = new Vector2(growLevel/(1.0f * maxGrowLevel),growLevel/(1.0f * maxGrowLevel));	}
	public bool is_grown(){
		return this.growLevel == this.maxGrowLevel;
	}
	public void check_adjacency(int x, int y, SpriteTileMap.Plot[,] cells){
		//these may return null, wirte an exception for it
		var north = cells[x,y-1].get_plant() != null && cells[x,y-1].get_plant().return_plant_type() == this.type;
		var south = cells[x,y+1].get_plant() != null && cells[x,y+1].get_plant().return_plant_type() == this.type;
		var west = cells[x-1,y].get_plant() != null && cells[x-1,y].get_plant().return_plant_type() == this.type;
		var east = cells[x+1,y].get_plant() != null && cells[x+1,y].get_plant().return_plant_type() == this.type;
		if (north || south || west || east){
			adjacentSatisfied = true;
		}
	}
	public int return_plant_type(){
		return this.type;
	}
}

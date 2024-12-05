using Godot;
using System;
using System.ComponentModel;
using System.Globalization;

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
	
	public void constructor(int sun_required, int water_required, bool adjacent_needed, int adjacent_type, int type, string newSprite){
		this.sunRequired = sun_required;
			this.waterRequired = water_required;
			this.adjacentNeeded = adjacent_needed;
			this.adjacentType = adjacent_type;
			this.type = type;
			sprite.Texture = GD.Load<Texture2D>(newSprite);
			this.adjacentSatisfied = !adjacent_needed;
			
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
		this.sprite.Scale = new Vector2(growLevel/(1.0f * maxGrowLevel),growLevel/(1.0f * maxGrowLevel));
	}
	public bool is_grown(){
		return this.growLevel == this.maxGrowLevel;
	}
	public void check_adjacency(int x, int y, SpriteTileMap.Plot[,] cells){
		//checks if adjacent tiles match the needed planttype to satisfy growth conditions
		//system 
		var north = false;
		var south = false;
		var west = false;
		var east = false;
		if (y - 1 > 0){
			north = cells[x,y-1].get_plant() != null && cells[x,y-1].get_plant().return_plant_type() == this.type;
		}
		if (y + 1 < cells.GetLength(1)){
			south = cells[x,y+1].get_plant() != null && cells[x,y+1].get_plant().return_plant_type() == this.type;
		}
		if (x- 1 > 0){
			west = cells[x-1,y].get_plant() != null && cells[x-1,y].get_plant().return_plant_type() == this.type;
		}
		if (x + 1 < cells.GetLength(0)){
			east = cells[x+1,y].get_plant() != null && cells[x+1,y].get_plant().return_plant_type() == this.type;
		}
		if (north || south || west || east){
			adjacentSatisfied = true;
		}
	}
	public int return_plant_type(){
		return this.type;
	}
	public int return_plant_growth(){
		return this.growLevel;
	}
	public void set_growth(int num){
		this.growLevel = num;
		this.sprite.Scale = new Vector2(growLevel/(1.0f * maxGrowLevel),growLevel/(1.0f * maxGrowLevel));
	}
	
}

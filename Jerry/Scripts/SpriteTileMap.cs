using Godot;
using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

public partial class SpriteTileMap : TileMap
{
	
	public struct Plot{
		public Plant plant;
		public int water;
		public Plot(){
			this.plant = null;
			this.water = 0;
		}
		public void add_plant(Plant plant){
			this.plant = plant;
		}
		public void add_water(int water){
			this.water += water;
			
			if (this.plant != null){
				//this.plant.update_water(this.water);
			}
		}
		
		public void add_sun(int sun){

			if (this.plant != null){
				this.plant.update_sun(sun, this.water);
			}
			
		}
        public override string ToString()
        {
            return $"{this.plant}, {this.water}";
        }
		public Plant get_plant(){
			return this.plant;
		}
		public void check_adjacency(int x, int y, Plot[,] cells){
			if (this.plant != null){
				plant.check_adjacency(x,y,cells);
			}
		}
    }
	private Plot[,] cells;
	// Called when the node enters the scene tree for the first time.
	private int cellsWidth = 7;
	private int cellsHeight = 3;
	public override void _Ready()
	{
		cells = new Plot[10,6];
		for (var i = 0; i < cellsWidth; i++){
			for (var j = 0; j < cellsHeight; j++){
				var temp = new Vector2I(i,j);
				if (GetCellSourceId(0, temp) == 0){
					cells[temp[0],temp[1]] = new Plot();
				}
			}
		}
		time_passes();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	public void time_passes(){
		
		for (var i = 0; i <= cellsWidth; i++){
			for (var j = 0; j <= cellsHeight; j++){
				cells[i,j].check_adjacency(i,j,cells);
				cells[i,j].add_water((int)GD.RandRange(0,2));
				cells[i,j].add_sun( (int)GD.RandRange(0,10));
			}
		}
	}
	public void sow_seed(Plant plant,int x, int y){
		cells[x,y].add_plant(plant);
	}

	public Plant return_plant(int x, int y){
		return cells[x,y].get_plant();
	}
	public bool cut_plant(int x, int y){
		var isGrown = cells[x,y].get_plant().is_grown();
		cells[x,y].get_plant().QueueFree();
		sow_seed(null, x,y);
		return isGrown;
	}
	public bool is_within_bounds(int x, int y){
		if (x < 0 || x > cellsWidth || y < 0 || y > cellsHeight){
			return false;
		}
		return true;
	}
}

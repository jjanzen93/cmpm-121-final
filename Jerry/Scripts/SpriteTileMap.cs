using Godot;
using System;
using System.Collections.Generic;
using System.Globalization;
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
	private int cellsWidth = 7;
	private int cellsHeight = 3;
	[Export]
	public CharacterBody2D player {get;set;}
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
	}
	public void time_passes(int seed){
		GD.Seed((ulong)seed);
		for (var i = 0; i <= cellsWidth; i++){
			for (var j = 0; j <= cellsHeight; j++){
				cells[i,j].check_adjacency(i,j,cells);
				cells[i,j].add_water((int)GD.RandRange(0,2));
				cells[i,j].add_sun((int)GD.RandRange(0,10));
			}
		}
	}
	public void time_regresses(int seed){
		GD.Seed((ulong)seed);
		for (var i = 0; i <= cellsWidth; i++){
			for (var j = 0; j <= cellsHeight; j++){
				cells[i,j].check_adjacency(i,j,cells);
				cells[i,j].add_water(-(int)GD.RandRange(0,2));
			}
		}
	}
	public void sow_seed(Plant plant,int x, int y){
		cells[x,y].add_plant(plant);
	}

	public Plant return_plant(int x, int y){
		if (x < 0 || x > cellsWidth || y < 0 || y > cellsHeight){
			return null;
		}
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
	public List<int> return_cells_for_undo(){
		List<int> ints = new List<int>();
		for (var i = 0; i <= cellsWidth; i++){
			for (var j = 0; j <= cellsHeight; j++){
				if(cells[i,j].plant == null){
					ints.Add(0);
				}
				else{
					ints.Add(cells[i,j].plant.return_plant_growth() * 10 + cells[i,j].plant.return_plant_type());
				}
			}
		}
		GD.Print(ints.ToString());
		GD.Print("size of cells " + ints.Count * 4);
		return ints;
	}
	public void parse_gamestate(int[] gamestate){
		//ignore the first 4 entries of gamestate
		int offset = 4;
		for (int i = 0; i <= cellsWidth; i++){
			for(int j = 0; j <= cellsHeight; j++){
				int currentCell = gamestate[i * (cellsHeight+1) + j + offset];
				
				if (cells[i,j].get_plant() != null){
					GD.Print(cells[i,j].get_plant());
					cells[i,j].get_plant().QueueFree();
					cells[i,j].plant = null;
					GD.Print("this happens");
				}
				if(currentCell == 0){
					continue;
				}
				
				GD.Print(cells[i,j].get_plant());
				GD.Print("sneaked past");
				GD.Print(i + " " + j);
				Plant newPlant = player.plantScene.Instantiate() as Plant;
				AddChild(newPlant);
				newPlant.GlobalPosition = new Vector2(i * 128 + 128, j * 128 + 128);
				var newPlantType = player.plantTypes[currentCell % 10];
				newPlant.constructor(newPlantType.sunRequired, newPlantType.waterRequired, newPlantType.adjacentNeeded, newPlantType.adjacentType, newPlantType.type, player.plantSprites[newPlantType.type]);
				sow_seed(newPlant, i,j);
				newPlant.set_growth(currentCell/10);
			}
		}
	}
}

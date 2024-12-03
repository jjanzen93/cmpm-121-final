using Godot;
using System;
using System.Threading.Tasks;

public partial class CharacterBody2D : Godot.CharacterBody2D
{
	[Export]
	public TileMap ExportedTileMap { get; set; }
	[Export]
	public SpriteTileMap PlotTileMap { get; set; }
	[Export]
	public GameScene GameController { get; set; }
	private int TILE_SIZE;
	private Vector2 currentLocation = new Vector2(3,2);
	private bool mayMove = true;
	private PackedScene plantScene = ResourceLoader.Load("res://Jerry/Scenes/Plant.tscn") as PackedScene;
	public PlantType[] plantTypes = {new PlantType(5,5,true, 1,0 ), new PlantType(0,10, false, -1, 1), new PlantType(8,0,false, -1, 2)};
	public string[] plantSprites = {"Jerry/assets/PlantA.png", "Jerry/assets/PlantB.png", "Jerry/assets/PlantC.png"};
	public struct PlantType
	{
		public int sunRequired;
		public int waterRequired;
		public bool adjacentNeeded;
		public int adjacentType;
		public int type;
		public PlantType(int sun_required, int water_required, bool adjacent_needed, int adjacent_type, int type){
			this.sunRequired = sun_required;
			this.waterRequired = water_required;
			this.adjacentNeeded = adjacent_needed;
			this.adjacentType = adjacent_type;
			this.type = type;

		}
	}
	public override void _Ready()
	{
		TILE_SIZE = ExportedTileMap.TileSet.TileSize[0];
		GlobalPosition = ExportedTileMap.GlobalPosition + currentLocation * TILE_SIZE;
	}
	
	public override void _Process(double delta)
	{
		check_pressed();
	}
	private void check_pressed(){
		if (mayMove){
			if (Input.IsActionJustPressed("ui_right")){
				move_character_in_tilemap(1,0);
				performs_action();
			}
			else if (Input.IsActionJustPressed("ui_left")){
				move_character_in_tilemap(-1,0);
				performs_action();
			}
			else if (Input.IsActionJustPressed("ui_up")){
				move_character_in_tilemap(0,-1);
				performs_action();
			}
			else if (Input.IsActionJustPressed("ui_down")){
				move_character_in_tilemap(0,1);
				performs_action();
			}
			else if (Input.IsActionJustPressed("plant_seed")){
				var temp = PlotTileMap.return_plant((int)currentLocation[0],(int)currentLocation[1]);
				if (temp == null){
					if (PlotTileMap.is_within_bounds((int)currentLocation[0],(int)currentLocation[1])){
						Plant newPlant = plantScene.Instantiate() as Plant;
						PlotTileMap.AddChild(newPlant);
						newPlant.GlobalPosition = this.GlobalPosition;
						var newPlantType = plantTypes[GD.Randi() % 3];
						newPlant.constructor(newPlantType.sunRequired, newPlantType.waterRequired, newPlantType.adjacentNeeded, newPlantType.adjacentType, newPlantType.type, plantSprites[newPlantType.type]);
						PlotTileMap.sow_seed(newPlant, (int) currentLocation[0],(int) currentLocation[1]);
					}
				}
				else{
					//cutplant
					GD.Print("cut plant");
					if (PlotTileMap.cut_plant((int)currentLocation[0],(int)currentLocation[1])){
						GameController.increase_points(1);
					}
				}

				performs_action();
			}
			
		}
	}
	//call when the player performs an action to pass time
	private void performs_action(){
		PlotTileMap.time_passes();
		GD.Print("time passes");
	}
	private void move_character_in_tilemap(int x, int y){
		currentLocation += new Vector2(x,y);
		update_location();
	}
	private async Task update_location(){
		Tween tween = GetTree().CreateTween();
		mayMove = false;
		tween.TweenProperty(this, "position", ExportedTileMap.GlobalPosition + currentLocation * TILE_SIZE, 0.1f);
		await ToSignal(tween, "finished");
		GlobalPosition = ExportedTileMap.GlobalPosition + currentLocation * TILE_SIZE;
		mayMove = true;
	}
}

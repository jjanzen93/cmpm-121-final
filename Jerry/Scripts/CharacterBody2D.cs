using Godot;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

public partial class CharacterBody2D : Godot.CharacterBody2D
{
	[Export]
	public TileMap ExportedTileMap { get; set; }
	[Export]
	public SpriteTileMap PlotTileMap { get; set; }
	[Export]
	public GameScene GameController { get; set; }
	[Export]
	public Main main {get;set;}
	[Export]
	public GameScene GameScene {get; set;}
	private int TILE_SIZE;
	public Vector2 currentLocation = new Vector2(3,2);
	private bool mayMove = true;
	public PackedScene plantScene = ResourceLoader.Load("res://Jerry/Scenes/Plant.tscn") as PackedScene;
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
		GD.Seed(100);
	}
	
	public override void _Process(double delta)
	{
		check_pressed();
	}
	private void check_pressed(){
		if (mayMove){
			if (Input.IsActionJustPressed("ui_right")){
				move_character_in_tilemap(1,0);
				performs_action(0);
			}
			else if (Input.IsActionJustPressed("ui_left")){
				move_character_in_tilemap(-1,0);
				performs_action(1);
			}
			else if (Input.IsActionJustPressed("ui_up")){
				move_character_in_tilemap(0,-1);
				performs_action(2);
			}
			else if (Input.IsActionJustPressed("ui_down")){
				move_character_in_tilemap(0,1);
				performs_action(3);
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
						performs_action((newPlant.return_plant_type()+1) * 10 + newPlant.return_plant_growth());

					}
				}
				else{
					//cutplant
					GD.Print("cut plant");
					Plant plantvar = PlotTileMap.return_plant((int)currentLocation[0],(int)currentLocation[1]);
					if (PlotTileMap.cut_plant((int)currentLocation[0],(int)currentLocation[1])){
						GameController.increase_points(1);
					}
					GD.Print("CUT");
					GD.Print(plantvar.return_plant_type());
					GD.Print(plantvar.return_plant_growth());

					performs_action(100 + ( plantvar.return_plant_type()+1) * 10 + plantvar.return_plant_growth());
				}

				
			}
			
		}
	}
	//call when the player performs an action to pass time
	private void performs_action(int num){
		PlotTileMap.time_passes(GameScene.turn);
		GD.Print("time passes");
		GameScene.add_action();
		GameScene.increment_turn();
		GameScene.save_data();
		main.autosave(GameScene.return_save_string());
	}
	private void move_character_in_tilemap(int x, int y){
		currentLocation += new Vector2(x,y);
		update_location();
	}
	public async Task update_location(){
		Tween tween = GetTree().CreateTween();
		mayMove = false;
		tween.TweenProperty(this, "position", ExportedTileMap.GlobalPosition + currentLocation * TILE_SIZE, 0.1f);
		await ToSignal(tween, "finished");
		GlobalPosition = ExportedTileMap.GlobalPosition + currentLocation * TILE_SIZE;
		mayMove = true;
	}
	
}

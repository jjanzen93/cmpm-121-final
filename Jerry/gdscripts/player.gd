extends CharacterBody2D

@export var playerMoveMap : TileMap;
@export var plotTileMap : TileMap;
@export var gameController : Node2D;
@export var main : Node;
@export var actions_taken := 0;
@export var plants_cut := 0;
@export var plants_planted := 0;
@export var spaces_moved := 0;
var TILE_SIZE : int;
var currentLocation : Vector2 = Vector2(0,0);
var mayMove : bool = true;
var plantScene = preload("res://Jerry/Scenes/Plant.tscn");
var plantTypes = [
{"name": "sunflower",
"sprite" : "Jerry/assets/PlantC.png",
"checkGrowth": func checkGrowth(plant : Plant, water : int, sun : int, cells : Array):
	if sun >= 8:
		plant.grow();
	plant.label.text = str("sun:", sun," water:",water);
},
{"name": "greenplant",
"sprite": "Jerry/assets/PlantB.png",
"checkGrowth": func checkGrowth(plant : Plant, water : int, sun : int, cells : Array):
	if water > 10:
		plant.grow();
	plant.label.text = str("sun:", sun," water:",water);
},
{"name": "pickygrower",
"sprite": "Jerry/assets/PlantA.png",
"checkGrowth": func checkGrowth(plant : Plant, water : int, sun : int, cells : Array):
	var north = false;
	var south = false;
	var west = false;
	var east = false;
	var y = plant.currentLocation[1];
	var x = plant.currentLocation[0];
	var adjacentType = "pickygrower";
	if y - 1 >= 0:
		north = cells[x][y-1].get_plant() != null && cells[x][y-1].get_plant().return_plant_name() == adjacentType;
	if y + 1 < cells[0].size():
		south = cells[x][y+1].get_plant() != null && cells[x][y+1].get_plant().return_plant_name() == adjacentType;
	if x - 1 >= 0:
		west = cells[x-1][y].get_plant() != null && cells[x-1][y].get_plant().return_plant_name() == adjacentType;
	if x + 1 < cells.size():
		east = cells[x+1][y].get_plant() != null && cells[x+1][y].get_plant().return_plant_name() == adjacentType;
	if (north || south || west || east) && sun >= 5 && water >= 5:
		plant.grow();
	plant.label.text = str("sun:", sun," water:",water);
}];

var plantSprites : Array = ["Jerry/assets/PlantA.png", "Jerry/assets/PlantB.png", "Jerry/assets/PlantC.png"];

func _ready():
	TILE_SIZE = plotTileMap.tile_set.tile_size[0];
	global_position = playerMoveMap.global_position + currentLocation * TILE_SIZE;

func _process(_delta):
	check_pressed();
	
func check_pressed():
	if !mayMove:
		return;
	var direction = Input.get_vector("ui_left", "ui_right", "ui_up","ui_down");
	if direction[0] != direction[1] && direction[0] == 0 || direction[1] == 0:
		if Input.is_action_just_pressed("move_buttons"):
			move_character_in_tilemap(direction);
			performs_action(true);
	if Input.is_action_just_pressed("plant_seed"):
		plant_seed_pressed();
	

func move_character_in_tilemap(vector2):
	spaces_moved += 1;
	currentLocation += vector2;
	update_location();
	pass
func update_location():
	var tween = get_tree().create_tween();
	mayMove = false;
	tween.tween_property(self, "global_position", playerMoveMap.global_position + currentLocation * TILE_SIZE, 0.1);
	await tween.finished;
	global_position = playerMoveMap.global_position + currentLocation * TILE_SIZE;
	mayMove = true;
func plant_seed_pressed():
	#if plant does exist, cut plant,
	var temp = plotTileMap.return_plant(currentLocation[0],currentLocation[1]);
	if temp != null:
		plants_cut += 1;
		if plotTileMap.cut_plant(currentLocation[0],currentLocation[1]):
			gameController.increase_points(1); 
	else:
		#if plant doesnt exist, sow new plant.
		plants_planted += 1;
		if plotTileMap.is_within_bounds(currentLocation[0],currentLocation[1]):
			var newPlant = plantScene.instantiate();
			plotTileMap.add_child(newPlant);
			newPlant.global_position = global_position;
			var randNum = randi_range(0,plantTypes.size()-1);
			var newPlantType = plantTypes[randNum];
			newPlant.constructor(newPlantType.checkGrowth, newPlantType.name, newPlantType.sprite,currentLocation, randNum);
			plotTileMap.sow_seed(newPlant, currentLocation[0],currentLocation[1]);
	performs_action(false);

func performs_action(movement):
	actions_taken += 1;
	plotTileMap.time_passes(gameController.turn);
	print("time passes");
	gameController.add_action();
	gameController.increment_turn();
	main.autosave(gameController.return_save_string());
	pass

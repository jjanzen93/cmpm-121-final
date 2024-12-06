extends CharacterBody2D

@export var playerMoveMap : TileMap;
@export var plotTileMap : TileMap;
@export var gameController : Node2D;
@export var main : Node;
var TILE_SIZE : int;
var currentLocation : Vector2 = Vector2(0,0);
var mayMove : bool = true;
var plantScene = preload("res://Jerry/Scenes/Plant.tscn");
var plantTypes = [PlantType.new(5,5,1,0), PlantType.new(0,10,-1,1), PlantType.new(8,0,-1,2)]; #public PlantType[] plantTypes = {new PlantType(5,5,true, 1,0 ), new PlantType(0,10, false, -1, 1), new PlantType(8,0,false, -1, 2)};
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
			performs_action();
	if Input.is_action_just_pressed("plant_seed"):
		plant_seed_pressed();
	

func move_character_in_tilemap(vector2):
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
		if plotTileMap.cut_plant(currentLocation[0],currentLocation[1]):
			gameController.increase_points(1); 
	else:
		#if plant doesnt exist, sow new plant.
		if plotTileMap.is_within_bounds(currentLocation[0],currentLocation[1]):
			var newPlant = plantScene.instantiate();
			plotTileMap.add_child(newPlant);
			newPlant.global_position = global_position;
			
			var newPlantType = plantTypes[randi_range(0,2)];
			newPlant.constructor(newPlantType.sunRequired, newPlantType.waterRequired, newPlantType.adjacentType, newPlantType.type, plantSprites[newPlantType.type]);
			plotTileMap.sow_seed(newPlant, currentLocation[0],currentLocation[1]);
	performs_action();

func performs_action():
	plotTileMap.time_passes(gameController.turn);
	print("time passes");
	gameController.add_action();
	gameController.increment_turn();
	#main.autosave(gameController.return_save_string());
	pass

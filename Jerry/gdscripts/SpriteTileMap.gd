extends TileMap

@export var player : CharacterBody2D;
@export var main : Node;
@export var sun_min := 0;
@export var sun_max := 10;
@export var water_min := 0;
@export var water_max := 2;
@export var time_passed := 0;
@export var sun_accumulates := false;
@export var water_accumulates := true;

var cells : Array;
var cellsWidth := 8;
var cellsHeight := 4;


	
func _ready():
	cells = [];
	for i in cellsWidth:
		var temparr = [];
		for j in cellsHeight:
			var temp = Vector2(i,j);
			if get_cell_source_id(0, temp) == 0:
				temparr.append(Plot.new());
		cells.append(temparr);

func time_passes(seed : int):
	time_passed += 1;
	var rng = RandomNumberGenerator.new();
	print(str(sun_max));
	rng.seed = seed;
	for i in cellsWidth:
		for j in cellsHeight:
			cells[i][j].update_plot(rng.randi_range(water_min,water_max), rng.randi_range(sun_min,sun_max), cells);
			cells[i][j].set_sun_accumulation(sun_accumulates);
			cells[i][j].set_water_accumulation(water_accumulates);

func time_regresses(seed : int):
	time_passed -= 1;
	var rng = RandomNumberGenerator.new();
	rng.seed = seed;
	for i in cellsWidth:
		for j in cellsHeight:
			cells[i][j].update_plot(-rng.randi_range(water_min,water_max), 0, cells);
	
func sow_seed(plant : Plant, x : int, y : int):
	cells[x][y].add_plant(plant);

func return_plant(x : int, y : int):
	if x < 0 || x >= cellsWidth || y < 0 || y >= cellsHeight:
		return null;
	
	return cells[x][y].get_plant();
	
func cut_plant(x : int, y : int) -> bool:
	var isGrown = cells[x][y].get_plant().is_grown();
	cells[x][y].get_plant().queue_free();
	sow_seed(null, x,y);
	
	return isGrown;
	
func is_within_bounds(x : int, y : int) -> bool:
	if x < 0 || x >= cellsWidth || y < 0 || y >= cellsHeight:
		return false;
	return true;
	
func return_cells_for_undo() -> Array:
	print(check_currently_growing());
	var arr = []
	for i in cellsWidth:
		for j in cellsHeight:
			if cells[i][j].plant == null:
				arr.append(0);
			else:
				arr.append(cells[i][j].plant.return_plant_growth() * 10 + cells[i][j].plant.type);
	return arr;

func parse_gamestate(gamestate : Array) -> void:
	#ignore the first 4 entries of gamestate
	var offset = 4;
	for i in cellsWidth:
		for j in cellsHeight:
			var currentCell = gamestate[i * (cellsHeight) + j + offset];
			if cells[i][j].get_plant() != null:
				cells[i][j].get_plant().queue_free();
				cells[i][j].plant = null;
			
			if currentCell == 0:
				continue;
			
			var newPlant : Plant = player.plantScene.instantiate();
			add_child(newPlant);
			newPlant.global_position = Vector2(i * 128 + 128, j * 128 + 128);
			var newPlantType = player.plantTypes[(currentCell % 10)];
			newPlant.constructor(newPlantType.checkGrowth, newPlantType.name, newPlantType.sprite,Vector2(i,j), currentCell % 10);
			sow_seed(newPlant, i,j);
			newPlant.set_growth(currentCell/10);

func check_currently_growing() -> int:
	var currentlyGrowing := 0;
	for i in cellsWidth:
		for j in cellsHeight:
			if cells[i][j].get_plant() != null:
				currentlyGrowing += 1;
	
	return currentlyGrowing;

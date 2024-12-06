extends TileMap

@export var player : CharacterBody2D;
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
	var rng = RandomNumberGenerator.new();
	rng.seed = seed;
	for i in cellsWidth:
		for j in cellsHeight:
			cells[i][j].check_adjacency(i,j,cells);
			cells[i][j].add_water(rng.randi_range(0,2));
			cells[i][j].add_sun(rng.randi_range(0,10));

func time_regresses(seed : int):
	var rng = RandomNumberGenerator.new();
	rng.seed = seed;
	for i in cellsWidth:
		for j in cellsHeight:
			cells[i][j].check_adjacency(i,j,cells);
			cells[i][j].add_water(-rng.randi_range(0,2));
	
func sow_seed(plant : Plant, x : int, y : int):
	cells[x][y].add_plant(plant);

func return_plant(x : int, y : int):
	if x < 0 || x > cellsWidth || y < 0 || y > cellsHeight:
		return null;
	
	return cells[x][y].get_plant();
	
func cut_plant(x : int, y : int):
	var isGrown = cells[x][y].get_plant().is_grown();
	cells[x][y].get_plant().queue_free();
	sow_seed(null, x,y);
	
	return isGrown;
	
func is_within_bounds(x : int, y : int):
	if x < 0 || x > cellsWidth || y < 0 || y > cellsHeight:
		return false;
	return true;
	
func return_cells_for_undo() -> Array:
	var arr = []
	for i in cellsWidth:
		for j in cellsHeight:
			if cells[i][j].plant == null:
				arr.append(0);
			else:
				arr.append(cells[i][j].plant.return_plant_growth() * 10 + cells[i][j].plant.return_plant_type());
	return arr;

func parse_gamestate(gamestate : Array):
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
			newPlant.constructor(newPlantType.sunRequired, newPlantType.waterRequired, newPlantType.adjacentType, newPlantType.type, player.plantSprites[newPlantType.type]);
			sow_seed(newPlant, i,j);
			newPlant.set_growth(currentCell/10);


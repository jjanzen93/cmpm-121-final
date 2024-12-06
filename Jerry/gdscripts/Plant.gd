class_name Plant;
extends Node2D;

@export var sprite : Sprite2D;
@export var label : Label;
var waterRequired : int;
var sunRequired : int;
var adjacentType : int;
var type : int;
var growLevel := 1;
var maxGrowLevel := 3;
var adjacentSatisfied : bool;

func constructor(sun_required, water_required, adjacent_type, newtype, newSprite):
	sunRequired = sun_required;
	waterRequired = water_required;
	adjacentType = adjacent_type;
	type = newtype;
	sprite.texture = load(newSprite);
	adjacentSatisfied = adjacent_type < 0;

func update_sun(sun, water):
	
	
	if(sun >= sunRequired && water >= waterRequired && adjacentSatisfied):
		
		grow();
	label.text = str("sun:", sun," water:",water);
	
func grow():
	if (growLevel >= maxGrowLevel):
		return;
	growLevel += 1;
	sprite.scale = growLevel/(1.0 * maxGrowLevel) * Vector2(1,1);
	
func is_grown():
	return growLevel == maxGrowLevel;

func check_adjacency(x : int, y : int, cells : Array):
	#checks if adjacent tiles match the needed planttype to satisfy growth conditions
	var north = false;
	var south = false;
	var west = false;
	var east = false;
	if y - 1 > 0:
		north = cells[x][y-1].get_plant() != null && cells[x][y-1].get_plant().return_plant_type() == adjacentType;
	
	if y + 1 < cells[0].size():
		south = cells[x][y+1].get_plant() != null && cells[x][y+1].get_plant().return_plant_type() == adjacentType;
	
	if x - 1 > 0:
		west = cells[x-1][y].get_plant() != null && cells[x-1][y].get_plant().return_plant_type() == adjacentType;
	
	if x + 1 < cells.size():
		east = cells[x+1][y].get_plant() != null && cells[x+1][y].get_plant().return_plant_type() == adjacentType;
	if north || south || west || east:
		adjacentSatisfied = true;
	
func return_plant_type() -> int:
	return type;

func return_plant_growth() -> int:
	return growLevel;

func set_growth(num : int) -> void:
	growLevel = num;
	sprite.scale = growLevel/(1.0 * maxGrowLevel) * Vector2(1,1);



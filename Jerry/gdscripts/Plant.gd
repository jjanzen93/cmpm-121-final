class_name Plant;
extends Node2D;

@export var sprite : Sprite2D;
@export var label : Label;

var plantName : String;
var growLevel := 1;
var maxGrowLevel := 3;
var checkGrowCondition : Callable;
var currentLocation : Vector2;
var type : int;
func constructor(checkGrowth, newName, newSprite, newLocation, newtype):
	checkGrowCondition = checkGrowth;
	
	plantName = newName;
	sprite.texture = load(newSprite);
	
	currentLocation = newLocation;
	type = newtype;

func checkGrowth(watertotal : int, sun : int, cells : Array):
	checkGrowCondition.call(self, watertotal, sun, cells);
"""
func update_sun(sun, water):
	
	
	if(sun >= sunRequired && water >= waterRequired && adjacentSatisfied):
		
		grow();
	
"""
func grow():
	if (growLevel >= maxGrowLevel):
		return;
	growLevel += 1;
	sprite.scale = growLevel/(1.0 * maxGrowLevel) * Vector2(1,1);
	
func is_grown():
	return growLevel == maxGrowLevel;
"""
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
"""
func return_plant_name() -> String:
	return plantName;

func return_plant_growth() -> int:
	return growLevel;

func set_growth(num : int) -> void:
	growLevel = num;
	sprite.scale = growLevel/(1.0 * maxGrowLevel) * Vector2(1,1);



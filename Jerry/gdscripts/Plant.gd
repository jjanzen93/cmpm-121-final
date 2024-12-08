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

func grow():
	if (growLevel >= maxGrowLevel):
		return;
	growLevel += 1;
	sprite.scale = growLevel/(1.0 * maxGrowLevel) * Vector2(1,1);
	
func is_grown():
	return growLevel == maxGrowLevel;

func return_plant_name() -> String:
	return plantName;

func return_plant_growth() -> int:
	return growLevel;

func set_growth(num : int) -> void:
	growLevel = num;
	sprite.scale = growLevel/(1.0 * maxGrowLevel) * Vector2(1,1);



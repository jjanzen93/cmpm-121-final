class_name Plot;
extends Node

var sun_accumulates := false;
var water_accumulates := true;
var plant : Plant;
var water : int;
var sun : int;

func _init():
	self.plant = null;
	self.water = 0;
	self.sun = 0;
	self.sun_accumulates;
	self.water_accumulates;
	
func add_plant(new_plant : Plant):
	self.plant = new_plant;

func update_plot(newwater: int, newsun: int, cells: Array):
	if water_accumulates:
		water += newwater;
	else:
		water = newwater;
	if sun_accumulates:
		sun += newsun;
	else:
		sun = newsun;
	if plant != null:
		plant.checkGrowth(water, sun, cells);

func set_sun_accumulation(value):
	sun_accumulates = value;

func set_water_accumulation(value):
	water_accumulates = value;
		
func ToString():
	return str(self.plant,",", self.water);

func get_plant():
	return self.plant;

	

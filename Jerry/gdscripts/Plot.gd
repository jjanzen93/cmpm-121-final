class_name Plot;
extends Node

var plant : Plant;
var water : int;
func _init():
	self.plant = null;
	self.water = 0;
	
func add_plant(new_plant : Plant):
	self.plant = new_plant;

func update_plot(newwater: int, sun: int, cells: Array):
	water += newwater;
	if plant != null:
		plant.checkGrowth(water, sun, cells);

	
		
func ToString():
	return str(self.plant,",", self.water);

func get_plant():
	return self.plant;

	

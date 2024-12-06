class_name Plot;
extends Node

var plant : Plant;
var water : int;
func _init():
	self.plant = null;
	self.water = 0;
	
func add_plant(new_plant : Plant):
	self.plant = new_plant;


func add_water(new_water : int):
	self.water += new_water;

	
func add_sun(sun : int):
	if plant != null:
		plant.update_sun(sun, self.water);
	
		
func ToString():
	return str(self.plant,",", self.water);

func get_plant():
	return self.plant;

func check_adjacency(x : int, y : int, cells : Array):
	if self.plant != null:
		plant.check_adjacency(x,y,cells);
	
	

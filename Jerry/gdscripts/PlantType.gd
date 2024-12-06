class_name PlantType
extends Node

var sunRequired : int;
var waterRequired : int;
var adjacentType : int;
var type : int;

func _init(sun_required, water_required, adjacent_type, type):
	self.sunRequired = sun_required;
	self.waterRequired = water_required;
	self.adjacentType = adjacent_type;
	self.type = type;

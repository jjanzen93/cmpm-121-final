extends Node

@export var plotTileMap : TileMap;
@export var player: CharacterBody2D;
@export var game: Node2D;

# Called when the node enters the scene tree for the first time.
func _ready():
	if FileAccess.file_exists("user://external.txt"):
		var file = FileAccess.open("user://external.txt", FileAccess.READ);
		var line = file.get_line();
		var array = line.split(" ");
		print(array);
		if line.left(3) == "val" || line.left(3) == "rul":
			update_value(array[1], array[3].trim_suffix(";"));
		elif line.left(3) == "con":
			array = parse_parenthetical(line);
		line = file.get_line();

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	pass

func save_to_file(data):

	var filename = "save_1.txt";
	var save_num = 1;
	while FileAccess.file_exists("user://save_files/" + filename): 
		save_num += 1;
		filename = str("save_", save_num, ".txt");
	filename = str("user://save_files/save_", save_num, ".txt");
	var file = FileAccess.open(filename, FileAccess.WRITE);
	file.store_string(data);

func autosave(data):
	var file = FileAccess.open("user://save_files/save_0.txt", FileAccess.WRITE);
	file.store_string(data);

func load_from_file(save_num):
	var file = FileAccess.open(str("user://save_files/save_", save_num, ".txt"), FileAccess.READ);
	var data = file.get_as_text();
	return data;

func parse_parenthetical(statement):
	var substring = statement.replace(" ", "");
	substring = substring.get_slice("(", 1);
	substring = substring.trim_suffix(");");
	return substring.split(",");

func update_value(name, value):
	print(name);
	print(value);
	if name == "sun_min":
		plotTileMap.sun_min = int(value);
	elif name == "sun_max":
		print("changing sun_max")
		plotTileMap.sun_max = int(value);
	elif name == "water_min":
		plotTileMap.water_min = int(value);
	elif name == "water_max":
		plotTileMap.water_max = int(value);
	elif name == "sun_accumulates":
		plotTileMap.sun_accumulates = bool(value);
	elif name == "rain_accumulates":
		plotTileMap.rain_accumulates = bool(value);
	else:
		print("Base value not found.");

func check_condition(condition):
	if condition.type == "time_passed" && plotTileMap.time_passed >= condition.value:
		return true;
	if condition.type == "actions_taken" && game.turn >= condition.value:
		return true;
	if condition.type == "plants_cut" && player.plants_cut >= condition.value:
		return true;
	if condition.type == "plants_planted" && player.plants_planted >= condition.value:
		return true;
	if condition.type == "spaces_moved" && player.spaces_moved >= condition.value:
		return true;
	return false;

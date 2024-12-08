extends Node

@export var plotTileMap : TileMap;
@export var player: CharacterBody2D;
@export var game: Node2D;
@export var conditions := [];
@export var events := [];

# Called when the node enters the scene tree for the first time.
func _ready():
	if FileAccess.file_exists("user://external.txt"):
		var file = FileAccess.open("user://external.txt", FileAccess.READ);
		var line = file.get_line();
		var array = line.split(" ");
		var timeout = 0
		while array[0] != "end" && timeout <= 1000:
			timeout += 1;
			if array[0] == "val" || array[0] == "rul":
				print("update value");
				update_value(array[1], array[3].trim_suffix(";"));
			elif array[0] == "con":
				print("build condition");
				var name = array[1];
				array = parse_parenthetical(line);
				for condition in conditions:
					if condition.name == name:
						conditions.erase(condition);
				conditions.append(build_condition(name, array[0], array[1]));
			elif array[0] == "eve":
				print("build event");
				array = parse_parenthetical(line);
				events.append(build_event(array[0], array[1], array[2]));
			line = file.get_line();
			array = line.split(" ");

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
	check_events();

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
	if name == "sun_min":
		plotTileMap.sun_min = int(value);
	elif name == "sun_max":
		plotTileMap.sun_max = int(value);
	elif name == "water_min":
		plotTileMap.water_min = int(value);
	elif name == "water_max":
		plotTileMap.water_max = int(value);
	elif name == "points_earned":
		game.increase_points(int(value));
	elif name == "sun_accumulates":
		if value == "true":
			plotTileMap.sun_accumulates = true;
		elif value == "false":
			plotTileMap.sun_accumulates = false;
		else:
			print("Invalid boolean assignment");
	elif name == "water_accumulates":
		if value == "true":
			plotTileMap.water_accumulates = true;
		elif value == "false":
			plotTileMap.water_accumulates = false;
		else:
			print("Invalid boolean assignment");
	else:
		print("Base value not found.");

func check_condition(condition):
	if condition.trackedValue == "time_passed" && game.turn >= condition.triggerAmount:
		return true;
	if condition.trackedValue == "point_threshold" && game.points >= condition.triggerAmount:
		return true;
	if condition.trackedValue == "currently_planted" && plotTileMap.check_currently_growing() >= condition.triggerAmount:
		return true;
	return false;

func build_condition(name, trackedValue, triggerAmount):
	var newCondition = Condition.new();
	newCondition.name = name;
	newCondition.trackedValue = trackedValue;
	newCondition.triggerAmount = int(triggerAmount);
	return newCondition;

func build_event(conditionName, valueName, changedAmount):
	var newEvent : Event;
	for condition in conditions:
		if condition.name == conditionName:
			newEvent = Event.new();
			newEvent.condition = condition;
			newEvent.valueName = valueName;
			newEvent.changedAmount = changedAmount;
			return newEvent;
	print("Condition not found");
	return null;

func check_undone_events():
	for event in events:
		if event.occurredTurn > game.turn:
			event.occurredTurn = -1;

func check_events():
	for event in events:
		if check_condition(event.condition) && event.occurredTurn == -1:
			update_value(event.valueName, event.changedAmount);
			event.occurredTurn = game.turn;

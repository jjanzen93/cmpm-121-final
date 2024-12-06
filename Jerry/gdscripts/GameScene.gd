extends Node2D

@export var pointLabel : Label;
@export var undoButton : Button;
@export var redoButton : Button;
@export var saveButton : Button;
@export var loadManButton : MenuButton;
@export var loadAutoButton : Button;
@export var main : Node;
@export var player : CharacterBody2D;
@export var plotTileMap : TileMap;
var points := 0;
var turn := 0;
var byteArr : PackedByteArray;
var undoArray := [];
var redoArray := [];
var intsize = 1;
func  _ready():
	save_game_state();
	save_data();
	var temp = return_save_string()
	load_data(temp)
	#string d = AppDomain.CurrentDomain.BaseDirectory + "/save_files";
	#string fn = "save_1.txt";
	#int sn = 1;
	#while (File.Exists(Path.Combine(d, fn))) {
		#loadManButton.GetPopup().AddItem(fn, sn);
		#sn += 1;
		#fn = "save_" + sn.ToString() + ".txt";
	#}
func _on_undo_pressed():
	if undoArray.size() > 1:
		
		redoArray.append(undoArray.pop_back());
		update_game_state(undoArray.back());
		#regress time
		plotTileMap.time_regresses(turn);
func _on_redo_pressed():
	if redoArray.size() > 0:
		undoArray.append(redoArray.pop_back());
		update_game_state(undoArray.back());
		#progress time
		plotTileMap.time_passes(turn);

func update_game_state(arr : Array):
	player.currentLocation = Vector2(arr[0],arr[1]);
	player.update_location();
	#player location parsed
	
	points = arr[2];
	#points parsed
	
	turn = arr[3];
	#turn parsed
	
	plotTileMap.parse_gamestate(arr);

func return_save_string() -> String:
	var newstring = "";
	for byte in byteArr:
		newstring += str(byte);
	print(newstring);
	return newstring;

func save_game_state():
	var inttemparr = [
		player.currentLocation[0],
		player.currentLocation[1],
		points,
		turn,
	]; # takes up 16 bytes of space
	
	inttemparr.append_array(plotTileMap.return_cells_for_undo());
	#adds the cells, takes 128 bytes of space
	
	undoArray.append(inttemparr);
	
func save_data():
	#saves undoArray and RedoArray to byteArray
	byteArr.clear();
	#8 for sizes, size * 128 + size * 128
	
	byteArr.resize(intsize * (2 + undoArray.size() * 36 + redoArray.size() * 36));
	var inttemparr = [
		undoArray.size(),
		redoArray.size(),
	]; #takes 8 bytes of space
	var offset = 0;
	for thisint in inttemparr:
		byteArr.encode_s8(offset,thisint);
		offset += intsize;
	
	for undo in undoArray:
		for thisint in undo:
			byteArr.encode_s8(offset,thisint);
			offset += intsize;
	
	for redo in redoArray:
		for thisint in redo:
			byteArr.encode_s8(offset,thisint);
			offset += intsize;
	print("data saved")
	print(byteArr);

func load_data(str : String):
	var arr = [];
	for byte in str:
		arr.append(int(byte));
	var index = 0;
	var undoArraySize = arr[index];
	index += intsize;
	var redoArraySize = arr[index];
	index += intsize;
	undoArray.clear();
	redoArray.clear();
	print(undoArraySize);
	print(redoArraySize);
	
	for n in undoArraySize:
		var temparr = [];
		for i in 36:
			temparr.append(arr[index]);
			index += intsize;
		undoArray.append(temparr);
	for n in redoArraySize:
		var temparr = [];
		for i in 36:
			temparr.append(arr[index]);
			index += intsize;
		redoArray.append(temparr);
	#readdata unnecessary
func add_action():
	save_game_state();
	save_data();
	redoArray.clear();
	pass
	
func increment_turn():
	turn += 1;

func increase_points(num):
	points += num;
	pointLabel.text = str("points: ", points)

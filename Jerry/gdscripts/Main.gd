extends Node


# Called when the node enters the scene tree for the first time.
func _ready():
	pass # Replace with function body.


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	pass

func save_to_file(data):

	var dir = DirAccess.open("user://save_files");
	if dir == null:
		dir = DirAccess.open("user://");
		dir.make_dir("save_files");
		dir = DirAccess.open("user://save_files");

	dir.list_dir_begin();
	var filename = dir.get_next();
	var save_num = 1;
	if filename == "save_0.txt":
		filename = dir.get_next();
	while filename != "":
		filename = dir.get_next();
		save_num += 1;
	filename = str("user://save_", save_num, ".txt");
	var file = FileAccess.open(filename, FileAccess.WRITE);
	file.store_string(data);

func autosave(data):
	var dir = DirAccess.open("user://save_files");
	if dir == null:
		dir = DirAccess.open("user://");
		dir.make_dir("save_files");
		dir = DirAccess.open("user://save_files");

	var file = FileAccess.open("user://save_0.txt", FileAccess.WRITE);
	file.store_string(data);

func load_from_file(save_num):
	var file = FileAccess.open("user://save_game.dat", FileAccess.READ);
	var data = file.get_as_text();
	return data;


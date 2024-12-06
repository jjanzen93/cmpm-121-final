extends Node


# Called when the node enters the scene tree for the first time.
func _ready():
	pass # Replace with function body.


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


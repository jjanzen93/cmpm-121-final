using Godot;
using System;
using System.IO;

public partial class Main : Node

{
	
	public override void _Ready()
	{
	}

	public override void _Process(double delta)
	{
		
	}
	
	public void save_to_file(string data) 
	{
		string dir = AppDomain.CurrentDomain.BaseDirectory + "/save_files";
		if (!Directory.Exists(dir)) {
			Directory.CreateDirectory(dir);
		}
		File.WriteAllText(Path.Combine(dir, "save.txt"), data);
	}

	public string read_save() {
		string dir = AppDomain.CurrentDomain.BaseDirectory + "/save_files";
		if (!Directory.Exists(dir)) {
			Directory.CreateDirectory(dir);
		}
		return File.ReadAllText(Path.Combine(dir, "save.txt"));
	}

}

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
		string filename = "save_1.txt";
		int save_num = 1;
		while (File.Exists(Path.Combine(dir, filename))) {
			save_num += 1;
			filename = "save_" + save_num.ToString() + ".txt";
		}
		File.WriteAllText(Path.Combine(dir, filename), data);
	}

	public void autosave(string data) {
		string dir = AppDomain.CurrentDomain.BaseDirectory + "/save_files";
		if (!Directory.Exists(dir)) {
			Directory.CreateDirectory(dir);
		}
		string filename = "save_0.txt";
		File.WriteAllText(Path.Combine(dir, filename), data);
	}

	public string read_save(int save_num) {
		string dir = AppDomain.CurrentDomain.BaseDirectory + "/save_files";
		if (!Directory.Exists(dir)) {
			Directory.CreateDirectory(dir);
		}
		string filename = "save_" + save_num.ToString() + ".txt";
		return File.ReadAllText(Path.Combine(dir, filename));
	}

}

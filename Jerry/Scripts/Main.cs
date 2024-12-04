using Godot;
using System;
using System.IO;

public partial class Main : Node

{
	
	public override void _Ready()
	{
		save_to_file("saved");
	}

	public override void _Process(double delta)
	{
		
	}
	
	public void save_to_file(string data) 
	{
		string dir = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments) + "/save_files";
		if (!Directory.Exists(dir)) {
			Directory.CreateDirectory(dir);
		}
		File.WriteAllText(Path.Combine(dir, "save.txt"), data);
	}

}

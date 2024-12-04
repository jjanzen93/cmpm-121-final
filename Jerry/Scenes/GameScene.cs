using Godot;
using Godot.NativeInterop;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web;

public partial class GameScene : Node2D
{
	[Export]
	public Label pointlabel {get;set;}
	[Export]
	public Button undoButton {get;set;}
	[Export]
	public Button redoButton {get;set;}
	[Export]
	public Button saveButton {get;set;}
	[Export]
	public Button loadManButton {get;set;}
	[Export]
	public Button loadAutoButton {get;set;}
	[Export]
	public Main main {get;set;}
	[Export]
	public CharacterBody2D player {get;set;}
	public int points = 10;
	public int turn = 0;
	public List<byte> newarr = new List<byte>();
	public List<int> actionArray = new List<int>();
	public List<int> redoArray = new List<int>();
	public override void _Ready()
	{
		undoButton.Pressed += () => {
			GD.Print("undo pressed");
			if (actionArray.Count > 0){
				GD.Print(actionArray[actionArray.Count-1]);
				var lastIndex = actionArray.Count-1;
				redoArray.Add(actionArray[lastIndex]);
				player.undo_action(actionArray[lastIndex]);
				actionArray.RemoveAt(lastIndex);
			}
		};
		redoButton.Pressed += () => {
			GD.Print("redo pressed");
			if (redoArray.Count > 0){
				var lastIndex = redoArray.Count-1;
				actionArray.Add(redoArray[lastIndex]);
				GD.Print(redoArray[lastIndex]);
				player.undo_action(flip_number(redoArray[lastIndex]));
				redoArray.RemoveAt(lastIndex);
			}
		};
		saveButton.Pressed += () => {
			GD.Print("save pressed");
			save_data();
			main.save_to_file(return_save_string());
		};
		loadManButton.Pressed += () => {
			GD.Print("load man pressed");
			load_data(main.read_save());
		};
		loadAutoButton.Pressed += () => {
			GD.Print("load auto pressed");
			load_data(main.read_save());
		};
		
	}
	private void save_data(){
		newarr.Clear();
		List<int> temparr = new List<int>
        {
            (int)player.currentLocation[0],
            (int)player.currentLocation[1],
            points,
            turn,
			actionArray.Count,
			redoArray.Count,
        };
		foreach (int action in actionArray){
			temparr.Add(action);
		}
		foreach(int redo in redoArray){
			temparr.Add(redo);
		}
		foreach(int index in temparr){
			foreach(byte thisbyte in BitConverter.GetBytes(index)){
				newarr.Add(thisbyte);
			}
		}
	}
	private void load_data(string str){
		//assume this works idk 
		String[] arr = str.Split('-');
		byte[] array = new byte[arr.Length];
		for(int i = 0; i < arr.Length; i++){
			array[i] = Convert.ToByte(arr[i], 16);
		}
		
		read_data(array);
	}
	private void read_data(byte[] bytearr){
		
		player.currentLocation[0] = BitConverter.ToInt32(bytearr,0);
		player.currentLocation[1] = BitConverter.ToInt32(bytearr,4);
		points = BitConverter.ToInt32(bytearr,8);
		turn = BitConverter.ToInt32(bytearr,12);
		var actionArraySize = BitConverter.ToInt32(bytearr,16);
		var redoArraySize = BitConverter.ToInt32(bytearr,20);
		int index = 24;
		for (int i = 0; i < actionArraySize; i++){
			actionArray.Add(BitConverter.ToInt32(bytearr,index));
			index += 4;
		}
		for (int i = 0; i < redoArraySize; i++){
			redoArray.Add(BitConverter.ToInt32(bytearr,index));
			index += 4;
		}
		player.update_location();
	}
	public string return_save_string(){
		return BitConverter.ToString(newarr.ToArray());
	}
	private int flip_number(int num){
		if (num > 10){
			return num;
		}
		return  num % 2 == 0 ? num + 1 : num - 1;
	}
	public void increase_points(int num){
		points += num;
		pointlabel.Text = "points: "+ points.ToString();
		if (points >= 10){
			pointlabel.Text = "you win!";
		}
	}
	public void add_action(int num){
		GD.Print("added");
		GD.Print(num);
		actionArray.Add(num);
		redoArray.Clear();
	}
	public void increment_turn(){
		turn += 1;
	}
}

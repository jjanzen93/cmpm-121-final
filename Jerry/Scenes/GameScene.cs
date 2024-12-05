using Godot;
using Godot.NativeInterop;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
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
	[Export]
	public SpriteTileMap plotmap {get;set;}
	public int points = 10;
	public int turn = 0;
	public List<byte> byteArr = new List<byte>();
	public List<int[]> undoArray = new List<int[]>();
	public List<int[]> redoArray = new List<int[]>();
	public override void _Ready()
	{
		save_game_state();
		save_data();
		GD.Print(undoArray.Count);
		GD.Print(redoArray.Count);
		GD.Print("returning save string\n" + return_save_string());
		undoButton.Pressed += () => {
			GD.Print(undoArray.Count);
			GD.Print("undo pressed");
			if (undoArray.Count > 1){
				
				var lastIndex = undoArray.Count-1;
				redoArray.Add(undoArray[lastIndex]);
				
				update_game_state(undoArray[lastIndex - 1]);
				undoArray.RemoveAt(lastIndex);
				//regress time
				plotmap.time_regresses(turn);
			}
		};
		redoButton.Pressed += () => {
			GD.Print("redo pressed");
			if (redoArray.Count > 0){
				var lastIndex = redoArray.Count-1;
				undoArray.Add(redoArray[lastIndex]);
				//GD.Print(redoArray[lastIndex]);
				update_game_state(redoArray[lastIndex]);
				redoArray.RemoveAt(lastIndex);
				//progress time
				plotmap.time_passes(turn);
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
			update_game_state(undoArray[undoArray.Count-1]);
		};
		loadAutoButton.Pressed += () => {
			GD.Print("load auto pressed");
			load_data(main.read_save());
		};
		
	}
	private void update_game_state(int[] gamestate){
		player.currentLocation = new Vector2(gamestate[0],gamestate[1]);
		player.update_location();
		//player location parsed

		points = gamestate[2];
		//points parsed

		turn = gamestate[3];
		//turn parsed
		plotmap.parse_gamestate(gamestate);
	}
	public string return_save_string(){
		GD.Print(byteArr.Count);
		return BitConverter.ToString(byteArr.ToArray());
	}
	
	private void save_game_state(){
		//appends a string of the current game state to the undoArray, does this through byte array conversion
		List<int> inttemparr = new List<int>{
			(int)player.currentLocation[0],
			(int)player.currentLocation[1],
			points,
			turn,
		}; // takes up 16 bytes of space

		inttemparr.AddRange(plotmap.return_cells_for_undo());
		//adds the cells, takes 128 bytes of space
		undoArray.Add(inttemparr.ToArray());

	}
	private void save_data(){
		//save undo + redo list to data, this is the savefile
		byteArr.Clear();
		int bytecount = 0;
		List<int> inttemparr = new List<int>
        {
			undoArray.Count,
			redoArray.Count,
        }; //takes 8 bytes of space
		foreach (int thisint in inttemparr){
			foreach (byte thisbyte in BitConverter.GetBytes(thisint)){
				byteArr.Add(thisbyte);
				bytecount += 1;
			}
		}
		
		foreach (int[] undo in undoArray){
			foreach (int thisint in undo){
				foreach (byte thisbyte in BitConverter.GetBytes(thisint)){
					byteArr.Add(thisbyte);
					bytecount += 1;
				}
			}
		}
		
		//byte arr
		// insert strings in bytearr (convert) (goofy structs) (converting - into bytes)
		// decode string to more bytes
		// bytes into actual data
		
		foreach (int[] redo in redoArray){
			foreach (int thisint in redo){
				
				foreach (byte thisbyte in BitConverter.GetBytes(thisint)){
					byteArr.Add(thisbyte);
					bytecount += 1;
				}
			}
		}
		GD.Print("bytecount in newarr" + bytecount);
		
		GD.Print(" newarr count " + byteArr.Count);
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
		//takes in a byte array, loops through it to repopulate undoArray and redoArray
		GD.Print("we tryying to read data");
		int index = 0;
		var undoArraySize = BitConverter.ToInt32(bytearr,index);
		index += 4;
		var redoArraySize = BitConverter.ToInt32(bytearr,index);
		index += 4;
		undoArray.Clear();
		redoArray.Clear();
		GD.Print(undoArraySize);
		GD.Print(redoArraySize);
		GD.Print(bytearr.Length);
		for (int i = 0; i < undoArraySize; i++){
			int[] temparr = new int[36];
			for (int j = 0; j < 36; j++){
				GD.Print(index);
				temparr[j] = BitConverter.ToInt32(bytearr,index);
				index += 4;
            }
			undoArray.Add(temparr);
		}
		for (int i = 0; i < redoArraySize; i++){
			int[] temparr = new int[36];
			for (int j = 0; j < 36; j++){
				temparr[j] = BitConverter.ToInt32(bytearr,index);
				index += 4;
			}
			redoArray.Add(temparr);
		}
		
	}
	
	public void increase_points(int num){
		points += num;
		pointlabel.Text = "points: "+ points.ToString();
		if (points >= 10){
			pointlabel.Text = "you win!";
		}
	}
	public void add_action(){
		save_game_state();
		save_data();
		
		GD.Print("returning save string\n" + return_save_string());
		redoArray.Clear();
	}
	public void increment_turn(){
		turn += 1;
	}
	
}

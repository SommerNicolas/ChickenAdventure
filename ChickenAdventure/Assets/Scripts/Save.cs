﻿using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class Save : MonoBehaviour {

	private Player player;
	// Use this for initialization
	void Start () {		
		player = GameObject.Find ("Player").GetComponent<Player> ();
	}
	
	// Update is called once per frame
	public void SaveData () {
		BinaryFormatter binaryF = new BinaryFormatter ();
		FileStream file = File.Create(Application.persistentDataPath + "/playerStats.dat");
		PlayerData data = new PlayerData ();
		data.hp = player.hp;
		data.mp = player.mp;
		data.xp = player.xp;
		data.niveau = player.niveau;
		data.armeEquipe = player.armeEquipe;
		data.oldlevel = player.oldLevel;
		data.posX = player.oldPos.x;
		data.posY = player.oldPos.y;
		binaryF.Serialize (file, data);
		file.Close ();
	}

	public void loadData(){		
		
		if (File.Exists (Application.persistentDataPath + "/playerStats.dat")) {
			BinaryFormatter binaryF = new BinaryFormatter ();
			FileStream file = File.Open(Application.persistentDataPath + "/playerStats.dat", FileMode.Open);
			PlayerData data = (PlayerData)binaryF.Deserialize(file);
			file.Close ();
			player = GameObject.Find("Player").GetComponent<Player>();
			player.hp = data.hp;
			player.mp = data.mp;
			player.xp = data.xp;
			player.niveau = data.niveau;
			player.armeEquipe = data.armeEquipe;
			player.oldLevel = data.oldlevel;
			player.oldPos.x = data.posX;
			player.oldPos.y = data.posY;
			Application.LoadLevel (player.oldLevel);
			player.transform.position = new Vector2 (player.oldPos.x, player.oldPos.y);
			player.ChangeUI ();
		}
	}
}

[Serializable]
class PlayerData {
	public int hp;
	public int mp;
	public int xp;
	public int niveau;
	public Arme armeEquipe;
	public string oldlevel;
	public float posX;
	public float posY;
}
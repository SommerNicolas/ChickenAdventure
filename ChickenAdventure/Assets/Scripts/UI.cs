﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UI : MonoBehaviour {

	public Text textMP;
	public Text textHP;
	public Button Save;
	public Button Load;

	public Image NPCFace;
	public Text NPCText;

	public Image ConvBack;

	public Button Choice1;
	public Button Choice2;
	public Button Choice3;

    public Player Player;

	NPC Npc;
    Chest chest; 

	public Canvas CanvasConv;

	void Awake () {
		DontDestroyOnLoad (transform.gameObject);
	}

	public void StartConversation(NPC Npc, Sprite Face){
		this.Npc = Npc;
		CanvasConv.enabled = true;
		NPCFace.sprite = Face;
        Player.inFight = true;
	}

    public void StartConversation(Chest chest)
    {
        this.chest = chest; 
        CanvasConv.enabled = true;
        Player.inFight = true; 
    }

    public void chestAnswer(int btn)
    {
        chest.answer(btn);
    }

	public void SaySomething(string Said){
		NPCText.text = Said;
	}

	public void SetAnswers (string A, string B, string C){
		Choice1.GetComponentInChildren<Text> ().text = A;
		Choice2.GetComponentInChildren<Text> ().text = B;
		Choice3.GetComponentInChildren<Text> ().text = C;
	}

    public void SetAnswer(string A)
    {
        Choice2.GetComponentInChildren<Text>().text = A;
       
        Choice1.enabled = false;
        Choice3.enabled = false;
    }

	public void Answer(int Btn){
        if(chest != null)
        {
            chest.answer(Btn);
        }
        else if (Npc != null)
        {
            Npc.Answer(Btn);
        }
	}
	public void CloseConversation(){
		CanvasConv.enabled = false;
        Player.inFight = false;
	}

	// Use this for initialization
	void Start () {
		CanvasConv.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
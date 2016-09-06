﻿using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Player : MovingObject {

    private Animator animator;
	private Text textHP;
	private Text textMP;

    public int hp = 100;
    public int mp = 100;
    public int baseAttack = 5;
    public Arme armeEquipe;
    public AudioClip ambiance;
    public Arme[] inventaire = new Arme[10];

	// Use this for initialization
	protected override void Start () {
        animator = GetComponent<Animator>();
        base.Start();
        armeEquipe = new BatonSorcier();

		InitGame ();
	}

    private int calculDmg() {
        return baseAttack + armeEquipe.Dmg;
    }

	void Awake () {
		DontDestroyOnLoad (transform.gameObject);
	}

    protected override void AttemptMove<T>(float xDir, float yDir)
    {
        base.AttemptMove<T>(xDir, yDir);
        return;
    }

    // Update is called once per frame
    void Update () {
        int horizontal = 0;
        int vertical = 0;

        horizontal = (int)Input.GetAxisRaw("Horizontal");
        vertical = (int)Input.GetAxisRaw("Vertical");

        if (horizontal != 0)
        {
            vertical = 0;
        }

        if (horizontal != 0 || vertical != 0)
        {
            AttemptMove<Items>(horizontal/5.0f, vertical/5.0f);

            if(horizontal == -1)
            {
                animator.SetTrigger("PlayerWalkLeftTrig");
            }
            else if(horizontal == 1)
            {
                animator.SetTrigger("PlayerWalkRightTrig");
            }
            else if(vertical == 1)
            {
                animator.SetTrigger("PlayerWalkUpTrig");
            }
            else if(vertical == -1)
            {
                animator.SetTrigger("PlayerWalkDownTrig");
            }
        }
        else
        {
            animator.SetTrigger("IDLE");
        }

    }

	void InitGame()
	{
		textHP = GameObject.Find ("textHP").GetComponent<Text> ();
		textMP = GameObject.Find ("textMP").GetComponent<Text> ();

		textHP.text = "Vie : " + hp;
		textMP.text = "Mana : " + mp;
	}

    protected override void OnCantMove<T>(T component)
    {
        Items item = component as Items;

        int d = item.Id;
        switch (d)
        {
            case 1:
                Chest chest = component as Chest;
                chest.OnUse();
                break;
            case 2:
                Boss boss = component as Boss;
                boss.OnUse();
                break;
            case 3:
                Wall wall = component as Wall;
                wall.OnUse();
                break;
			case 4:
				NPCFemale npcFemale = component as NPCFemale;
				npcFemale.OnUse ();
				break;
            default:
                break;

        }        
    }

    bool RandomFight()
    {
        bool Value;

        int randomValue = Random.Range(0, 9); 

        if(randomValue < 1 )
        {
            return true;
        }

        return false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
       /* if(other.tag == "Fight")
        if(RandomFight())
        {

        }*/
    }
}

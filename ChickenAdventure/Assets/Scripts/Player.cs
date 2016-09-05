﻿using UnityEngine;
using System.Collections;
using System;

public class Player : MovingObject {

    private Animator animator;
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
	}

    private int calculDmg() { return baseAttack + armeEquipe.getDmg(); }

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

    protected override void OnCantMove<T>(T component)
    {
        Items item = component as Items;

        int d = item.getId();

        /*if(d == 1)
        {
            Chest chest = component as Chest;
            chest.onUse();
        }
        else if(d == 2)
        {
            Boss boss = component as Boss;
            boss.onUse(); 
        }
        else if(d == 3)
        {
            Wall wall = component as Wall;
            wall.onUse();
        }*/

        switch (d)
        {
            case 1:
                Chest chest = component as Chest;
                chest.onUse();
                break;
            case 2:
                Boss boss = component as Boss;
                boss.onUse();
                break;
            case 3:
                Wall wall = component as Wall;
                wall.onUse();
                break;
            default:
                break;

        }        
    }
}

﻿using UnityEngine;
using System.Collections;

public abstract class MovingObject : MonoBehaviour {

    public float moveTime = 0.001f;
    public LayerMask blockingLayer;

    private BoxCollider2D boxCollider;
    private Rigidbody2D rb2D;
    private float inverseMoveTime; 


	// Use this for initialization
	protected virtual void Start () {
        boxCollider = GetComponent<BoxCollider2D>();
        rb2D = GetComponent<Rigidbody2D>();
        inverseMoveTime = 1f / 0.5f;
	}

    protected bool Move(float xDir, float yDir, out RaycastHit2D hit)
    {
        Vector2 start = transform.position;
        Vector2 end = start + new Vector2(xDir, yDir);

        boxCollider.enabled = false;
        hit = Physics2D.Linecast(start, end, blockingLayer);
        boxCollider.enabled = true; 

        if(hit.transform == null)
        {
            Vector3 newPosition = Vector3.MoveTowards(rb2D.position, end, inverseMoveTime * Time.deltaTime);
            rb2D.MovePosition(newPosition);
            return true; 
        }
        return false;
    }


    protected virtual void AttemptMove<T>(float xDir, float yDir) where T : Component
    {
        RaycastHit2D hit;
        bool CanMove = Move(xDir, yDir, out hit); 
        if(hit.transform == null)
        {
            return;
        }

        T hitComponent = hit.transform.GetComponent<T>();
        
        if (!CanMove && hitComponent != null)
        {
            OnCantMove(hitComponent);
        }
    }
	
    protected abstract void OnCantMove<T>(T component) where T : Component;

}

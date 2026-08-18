using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;


public class RewindPlayer : MonoBehaviour
{
    [Header("Number of seconds for rewind")]
    public float recordTime = 5f;
    
    //bring the list that contains player position
    List<StoreRewindValue> StoreRewindValue;
    
    private bool inRewindMode = false;
    private int timeContainer = 0;
    
    //unity component
    private Rigidbody2D body;
    private PlayerMovement movement;

    private void Start()
    {
        StoreRewindValue = new List<StoreRewindValue>();
        body = GetComponent<Rigidbody2D>();
        movement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        //to enter rewind mode
        if (Input.GetKeyDown(KeyCode.R)) 
            RewindTime();
    }

    private void FixedUpdate()
    {
        if (inRewindMode)
        { 
            //rewind backward
            if (Input.GetKey(KeyCode.Q))
            {
                RewindBackwards();
            }
            //rewind forward
            else if (Input.GetKey(KeyCode.E))
            {
                RewindForwards();
            }
        }
        if (!inRewindMode)
        {
            //collect player position to be used for rewind mechanic
            Record();
        }
    }
    
    void Record()
    {
        //calculate that the rewind only store this x amount of seconds
        //prevent from player from rewind back to start
        //basically overwrite position if they are x amount of seconds longer
       int maxFrames = Mathf.RoundToInt(recordTime /  Time.fixedDeltaTime);
       if (StoreRewindValue.Count >= maxFrames)
           StoreRewindValue.RemoveAt(StoreRewindValue.Count - 1);
        
        StoreRewindValue.Insert(0, new StoreRewindValue(transform.position, transform.rotation));
    }
    
    private void RewindTime()
    {
        inRewindMode = !inRewindMode;
        
        if(inRewindMode)
            EnterRewindMode();
        else
            ExitRewindMode();
    }

    private void EnterRewindMode()
    {
        //enter rewind mode
        //character freeze in place & movement disable
        body.isKinematic = true;
        body.linearVelocity = Vector2.zero;
        timeContainer = 0;
        
        //disable player movement in rewind mode
        if (movement != null)
            movement.enabled = false;
    }

    private void ExitRewindMode()
    {
        if (timeContainer > 0 && timeContainer < StoreRewindValue.Count)
            StoreRewindValue.RemoveRange(0, timeContainer);
        
        timeContainer = 0;
        body.isKinematic = false;
        
        //enable player movement in rewind mode
        if (movement != null)
            movement.enabled = true;
    }

    private void RewindBackwards()
    {
        if (timeContainer < StoreRewindValue.Count - 1)
        {
            timeContainer++;
            ApplyState(StoreRewindValue[timeContainer]);
        }
    }

    private void RewindForwards()
    {
        if (timeContainer > 0)
        {
            timeContainer--;
            ApplyState(StoreRewindValue[timeContainer]);
        }
    }

    private void ApplyState(StoreRewindValue state)
    {
        //position & rotation value to be used for rewind mechanic
        transform.position = state.position;
        transform.rotation = state.rotation;
    }
}

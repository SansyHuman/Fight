﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class Toxictile : MonoBehaviour
{
    [SerializeField] private Hp P1health;
    [SerializeField] private Hp P2health;
    [SerializeField] private float speed;
    [SerializeField] Vector2 direction;

    private float initHealth = 100;
    
    
    void Start()
    {
        P1health.Init(initHealth, initHealth);
        P2health.Init(initHealth, initHealth);
        StartCoroutine(reFill());
    }

    bool check = true;

    IEnumerator reFill()
    {
        while (check == true)
        {
            check = false;
            if (P1health.MyCurrentValue == 0)
            { 
                P1health.Init(initHealth, initHealth);
            }
            
            if (P2health.MyCurrentValue == 0)
            {
                P2health.Init(initHealth, initHealth);
            }
            yield return new WaitUntil(() => P1health.MyCurrentValue * P2health.MyCurrentValue == 0);
            yield return new WaitForSeconds(3);
            check = true;
        }
    
    }

    void Update()
    {
        Pollute();
    }
    public void Pollute()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    void OnCollisionStay2D(Collision2D col)
    {
        if(col.transform.name == "Stickman P1")
        {
            P1health.MyCurrentValue -= 1;
        }
        if(col.transform.name == "Stickman P2")
        {
            P2health.MyCurrentValue -= 1;
        }

        Vector2 moveVector;
        moveVector.x = Input.GetAxisRaw("Horizontal");
        moveVector.y = Input.GetAxisRaw("Vertical");

        direction = moveVector;
    }
    
}

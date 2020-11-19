﻿using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class EnemyTransport : MonoBehaviour
{
    public float playerDistance;
    public int health = 100;
    public Transform player;
    public Transform SpawnPoint;
    public GameObject[] EnemyType;
    private float timeBtwSpawns;
    public float startTimeBtwSpawns;

  
    public float speed;
    public float stopDist;
    public float retreatDist;


    private float timeBtwShots;
    public float startTimeBtwShots;
    public Transform FirePoint;

    public GameObject projectile;
    //public GameObject deathEffect;
    //public Animator animator;




    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        timeBtwSpawns = startTimeBtwSpawns;
    }

    // Update is called once per frame
    void Update()
    {
		if((Vector2.Distance(transform.position, player.position) <= playerDistance )){
			if (timeBtwSpawns <= 0)
			{
				Instantiate(EnemyType[Random.Range(0,2)], SpawnPoint.position, Quaternion.identity);
				timeBtwSpawns = startTimeBtwSpawns;
			}
			else {
				timeBtwSpawns -= Time.deltaTime;
			}
		}

        if (Vector2.Distance(transform.position, player.position) > stopDist)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            //animator.SetBool("isRunning", true);
        }
        else if (Vector2.Distance(transform.position, player.position) < stopDist && Vector2.Distance(transform.position, player.position) > retreatDist)
        {
            transform.position = this.transform.position;
            //animator.SetBool("isRunning", false);
        }
        else if (Vector2.Distance(transform.position, player.position) < retreatDist)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, -speed * Time.deltaTime);
            //animator.SetBool("isRunning", true);
        }

        if (timeBtwShots <= 0)
        {

            Instantiate(projectile, FirePoint.position, Quaternion.identity);
            timeBtwShots = startTimeBtwShots;

        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }
    }

    public void TakeDamage(int damage) {
        health -= damage;
        if (health <= 0) {

            Destroy();
        
        }
    }

    void Destroy() {
        Destroy(gameObject);    
    }
}
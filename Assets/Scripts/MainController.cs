using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController : MonoBehaviour
{
    public Tank[] tanks;
    public Projectile projectile;

    private int currentPlayer = 0;
    private int totalPlayers;

    float intervalForPowerAddition = 0.1f;
    float timeForPower = 0f;

    private void Awake()
    {
        totalPlayers = tanks.Length;
        projectile.OnCollision += OnProjectileCollided;
    }

    private void OnDestroy()
    {
        projectile.OnCollision -= OnProjectileCollided;
    }

    private void OnProjectileCollided()
    {
        currentPlayer = currentPlayer >= totalPlayers - 1 ? 0 : currentPlayer + 1;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            tanks[currentPlayer].MoveTank(new Vector2(-1f, 0f));
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            tanks[currentPlayer].MoveTank(new Vector2(1f, 0f));
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            tanks[currentPlayer].MoveTurrent(new Vector3(0f, 0f, 1f));
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            tanks[currentPlayer].MoveTurrent(new Vector3(0f, 0f, -1f));
        }

        if(Input.GetKey(KeyCode.Space))
        {
            timeForPower += Time.deltaTime;
            if (timeForPower > intervalForPowerAddition)
            {
                timeForPower = 0;
                projectile.CalculatePower();
            }
        }

        if(Input.GetKeyUp(KeyCode.Space))
        {
            projectile.Shoot(tanks[currentPlayer].projectileSpawn.transform.position, tanks[currentPlayer].FireDirection());
        }
    }
}

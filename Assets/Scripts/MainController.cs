using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainController : MonoBehaviour
{
    public Tank[] tanks;
    public Projectile projectile;
    public UIController uIController;

    private int currentPlayer = 0;
    private int totalPlayers;

    float intervalForPowerAddition = 0.1f;
    float timeForPower = 0f;


    private void Awake()
    {
        totalPlayers = tanks.Length;
    }

    private void Start()
    {
        projectile.OnCollision += OnProjectileCollided;
        uIController.InitializeHPTexts(totalPlayers, tanks);
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }

    private void OnDestroy()
    {
        projectile.OnCollision -= OnProjectileCollided;
    }

    private void OnProjectileCollided(GameObject obj)
    {
        currentPlayer = currentPlayer >= totalPlayers - 1 ? 0 : currentPlayer + 1;
        if (obj.CompareTag("Player"))
        {
            Tank tank = obj.GetComponent<Tank>();
            float health = tank.TakeDamage(projectile.playerDamage);
            uIController.UpdateHealthBar(health, Array.IndexOf(tanks, tank));
        }
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
                uIController.UpdatePower(projectile.CalculatePower().ToString());
            }
        }

        if(Input.GetKeyUp(KeyCode.Space))
        {
            projectile.Shoot(tanks[currentPlayer].projectileSpawn.transform.position, tanks[currentPlayer].FireDirection());
        }
    }
}

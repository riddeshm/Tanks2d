using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainController : MonoBehaviour
{
    public GameObject hpText, canvasObj;
    public Tank[] tanks;
    public Projectile projectile;

    private int currentPlayer = 0;
    private int totalPlayers;

    Text[] hpTexts;

    float intervalForPowerAddition = 0.1f;
    float timeForPower = 0f;

    private void Awake()
    {
        totalPlayers = tanks.Length;
        hpTexts = new Text[totalPlayers];
    }

    private void Start()
    {
        projectile.OnCollision += OnProjectileCollided;
        CreateHPBars();
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }

    void CreateHPBars()
    {
        for (int i = 0; i < totalPlayers; i++)
        {
            GameObject hpTxt = Instantiate(hpText, canvasObj.transform);
            RectTransform rt = hpTxt.GetComponent<RectTransform>();
            Vector3 playerPos = tanks[i].transform.position;
            Vector2 pos = new Vector2(playerPos.x, playerPos.y + 1.5f);
            Vector2 viewportPoint = Camera.main.WorldToViewportPoint(pos);
            rt.anchorMin = viewportPoint;
            rt.anchorMax = viewportPoint;
            hpTexts[i] = hpTxt.GetComponent<Text>();
        }
    }

    private void OnDestroy()
    {
        projectile.OnCollision -= OnProjectileCollided;
    }

    private void OnProjectileCollided(GameObject obj)
    {
        currentPlayer = currentPlayer >= totalPlayers - 1 ? 0 : currentPlayer + 1;
        if(obj.CompareTag("Player"))
        {
            Tank tank = obj.GetComponent<Tank>();
            float health = tank.TakeDamage(projectile.playerDamage);
            if(health > 0)
            {
                hpTexts[Array.IndexOf(tanks, tank)].text = "HP : " + health.ToString();
            }
            else
            {
                Destroy(hpTexts[Array.IndexOf(tanks, tank)].gameObject);
            }

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
                projectile.CalculatePower();
            }
        }

        if(Input.GetKeyUp(KeyCode.Space))
        {
            projectile.Shoot(tanks[currentPlayer].projectileSpawn.transform.position, tanks[currentPlayer].FireDirection());
        }
    }
}

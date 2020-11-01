using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public GameObject hpText, canvasObj;
    public Text powerTxt;

    Text[] hpTexts;


    public void InitializeHPTexts(int totalPlayers, Tank[] tanks)
    {
        hpTexts = new Text[totalPlayers];
        CreateHPBars(totalPlayers, tanks);
    }

    void CreateHPBars(int totalPlayers, Tank[] tanks)
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

    public void UpdatePower(string val)
    {
        powerTxt.text = "Power : " + val; 
    }

    public void UpdateHealthBar(float health, int index)
    {
        if (health > 0)
        {
            hpTexts[index].text = "HP : " + health.ToString();
        }
        else
        {
            Destroy(hpTexts[index].gameObject);
        }
    }
}

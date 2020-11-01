using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController : MonoBehaviour
{
    public Tank[] tanks;
    public Projectile projectile;

    private int currentPlayer = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
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

        if(Input.GetKeyDown(KeyCode.Space))
        {
            projectile.Shoot(tanks[currentPlayer].projectileSpawn.transform.position, tanks[currentPlayer].FireDirection());
        }
    }
}

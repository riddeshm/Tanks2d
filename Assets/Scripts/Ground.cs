using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Ground : MonoBehaviour
{
    public Tilemap terrain;

    public void DamageGround(Vector3 position, float radius)
    {
        //incrementing by 0.2 as the grid was scaled to 0.2
        for(float y = -radius; y < radius; y += 0.2f)
        {
            for (float x = -radius - y; x < radius + y; x += 0.2f)
            {
                Vector3Int tilePos = terrain.WorldToCell(position + new Vector3(x,y,0));
                if(terrain.GetTile(tilePos) != null)
                {
                    terrain.SetTile(tilePos, null);
                }
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Area : MonoBehaviour
{
    public Vector2 top;
    public Vector2 bot;

    // Check enemies in the area
    // If there are 7 enemies in a horizontal line after spawning, then destroy all enemies in the area 
    public void Update()
    {
        Collider2D[] area = Physics2D.OverlapAreaAll(top, bot);
        if (area != null)
        {
            foreach (Collider2D col in area)
            {
                if (area.Length == 7 && col.gameObject.CompareTag("Enemy"))
                {
                    Destroy(col.gameObject);
                    Debug.Log(col.gameObject.name);
                }
            }

        }
    }

    private void OnDrawGizmos()
    {
        AreaDrawLine.DrawRectange(top, bot);
    }
}

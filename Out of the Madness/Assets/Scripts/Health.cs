using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int health;
    public int numberOfLifes;

    [SerializeField] Image[] hearts;
    [SerializeField] Sprite fullHeart;
    [SerializeField] Sprite emptyHeart;

    void Update()
    {
        HealthSystem();
    }

    void HealthSystem()
    {
        if(health > numberOfLifes)
        {
            health = numberOfLifes;
        }

        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
            {               
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }

            if(i < numberOfLifes)
            {              
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }

    
}

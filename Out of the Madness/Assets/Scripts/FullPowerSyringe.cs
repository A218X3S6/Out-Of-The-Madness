using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullPowerSyringe : MonoBehaviour
{

    public static FullPowerSyringe instance;
    [SerializeField] Player playerScript;

    [HideInInspector] public int syringes = 0;

    [SerializeField] Animator animator;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Sprite[] syringeSprite;


    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        animator.enabled = false;
    }
    private void Update()
    {
        ResetFullPower();
        SpriteChanger();
    }

    public void AddSyringe()
    {
        syringes += 1;
        Debug.Log(syringes);
    }

    public void ResetSyringes()
    {
        syringes = 0;
    }

    void SpriteChanger()
    {
        if (syringes == 0)
        {
            spriteRenderer.sprite = syringeSprite[0];
        }
        if (syringes == 1)
        {
            spriteRenderer.sprite = syringeSprite[1];
        }
        if (syringes == 2)
        {
            spriteRenderer.sprite = syringeSprite[2];
            playerScript.fullPowerUsed = false;
        }
        if (syringes == 3)
        {
            spriteRenderer.sprite = syringeSprite[3];
        }
        if (syringes == 4)
        {
            spriteRenderer.sprite = syringeSprite[4];
        }
        if (syringes == 5)
        {
            animator.enabled = true;
            if (playerScript.UsingFullPower == true)
            {
                animator.Play("FullPowerUsed");
            }
            else
            {
                animator.Play("FullPower");
            }
        }
        else
        {
            animator.enabled = false;
        }

    }

    void ResetFullPower()
    {
        if (syringes >= 6)
        {
            ResetSyringes();
            Debug.Log("Number of syringes:" + syringes);
        }
    }


}

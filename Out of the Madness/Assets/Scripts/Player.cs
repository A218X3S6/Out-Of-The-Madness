using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider2D;

    [SerializeField] Animator animator;

    [Header("Scripts")]
    [SerializeField] ScoreCounter scoreCounter;
    [SerializeField] EndGameManager endGameManager;
    [SerializeField] Health healthScript;
    [SerializeField] GameManager gameManager;

    [Header("Audio")]
    [SerializeField] AudioClip gameOverClip;
    [SerializeField] AudioClip getHitClip;
    [SerializeField] AudioClip powerUpClip;

    [HideInInspector] public bool fullPowerUsed = false;
    [HideInInspector] public bool UsingFullPower = false;

    private bool wasHit;  

    [Header("Time")]
    [SerializeField] float fullpowerTime = 6f;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }
     
    private void Update()
    {
        UseFullPower();
    }

    void UseFullPower()
    {
        if (Time.timeScale != 0)
        {
            if (FullPowerSyringe.instance.syringes == 5)
            {
                if (fullPowerUsed == false)
                {
                    if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
                    {
                        AudioController.instance.PlayPowerUpClip(powerUpClip);
                        if (healthScript.health < 2)
                        {
                            healthScript.health++;
                            StartCoroutine(FullPower());
                            fullPowerUsed = true;
                        }
                        else
                        {
                            Debug.Log("Points 5x");
                            StartCoroutine(FullPowerExtra());
                            StartCoroutine(FullPower());
                            fullPowerUsed = true;

                        }
                    }
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if (wasHit == false)
            {
                spriteRenderer.color = new Color32(255, 100, 100, 200);

                if (healthScript.health == 0)
                {
                    gameManager.gameEnd = true;
                    Time.timeScale = 0f;
                    AudioController.instance.ToggleMusic();
                    AudioController.instance.PlayGameOverClip(gameOverClip);
                    StartCoroutine(ExitTime());
                    animator.gameObject.SetActive(true);
                    StartCoroutine(endGameManager.OnGameEnd());
                    Debug.Log("Dead");              
                    //wasHit = true;           
                }
                else
                {                  
                    AudioController.instance.PlayGetHitClip(getHitClip);
                    StartCoroutine(GetHit());
                    Debug.Log("Hit");
                }
            }
        }       
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            spriteRenderer.color = new Color32(255, 255, 255, 255);
        }
    }
    IEnumerator FullPowerExtra()
    {
        scoreCounter.fullPowerPoints = 5f;
        yield return new WaitForSeconds(fullpowerTime);
        scoreCounter.fullPowerPoints = 1f;
    }
    IEnumerator FullPower()
    {
        spriteRenderer.color = new Color32(255, 255, 255, 150);
        boxCollider2D.enabled = false;
        UsingFullPower = true;

        yield return new WaitForSeconds(fullpowerTime);

        UsingFullPower = false;
        boxCollider2D.enabled = true;
        spriteRenderer.color = new Color32(255, 255, 255, 255);
        FullPowerSyringe.instance.ResetSyringes();
    }

    IEnumerator GetHit()
    {
        wasHit = true;
        healthScript.health--;
        yield return new WaitForSeconds(0.5f);
        wasHit = false;
    }

    IEnumerator ExitTime()
    {
        animator.gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(0.9f);
        animator.gameObject.SetActive(false);
    }
}

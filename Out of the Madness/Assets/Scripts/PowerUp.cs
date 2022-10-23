using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PowerUp : MonoBehaviour
{
    [SerializeField] BoxCollider2D box;
    [SerializeField] AudioClip pickUpClip;

    private Animator animator;

    [Header("Floats")]
    [SerializeField] float multiplier = 2f;
    [SerializeField] float timeToDestroy = 1f;
    private float powerUpTime;

    private bool pickedSyringe = false;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }
    void Start()
    {
        animator.enabled = true;
        powerUpTime = Random.Range(2f, 6f);
        StartCoroutine(TimeToGet());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            AudioController.instance.PlayPickUpClip(pickUpClip);
            StartCoroutine(Power(collision));
        }
    }
    IEnumerator Power(Collider2D player)
    {
        Debug.Log("START");
        ScoreCounter scoreCounter = GameObject.Find("ScoreCanvas").GetComponent<ScoreCounter>();
        scoreCounter.pointIncreasedPerSecond *= multiplier;

        pickedSyringe = true;

        this.GetComponent<SpriteRenderer>().enabled = false;
        this.GetComponent<BoxCollider2D>().enabled = false;

        FullPowerSyringe.instance.AddSyringe();

        yield return new WaitForSeconds(powerUpTime);

        Debug.Log("END");
        scoreCounter.pointIncreasedPerSecond /= multiplier;
        Destroy(this.gameObject);
    }

    IEnumerator TimeToGet()
    {     
        yield return new WaitForSeconds(timeToDestroy);

        if (pickedSyringe == false)
        {
            Destroy(this.gameObject);
        }
    }
}

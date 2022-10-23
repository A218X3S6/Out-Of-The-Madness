using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody2D rb;

    // Limits character to max value it can move
    [Header("Bounds")]
    [SerializeField] float xMove;
    [SerializeField] float yMove;

    // Min/max value to move character
    [Header("DashValue")]
    //private float minX = -3.75f, maxX = 3.75f;
    //private float minY = -2.3f, maxY = -1.55f;

    private float minX = -3.75f, maxX = 3.75f;
    private float minY = -2.3f, maxY = -1.55f;

    [Header("Particles")]
    [SerializeField] ParticleSystem leftDashEffect;
    [SerializeField] ParticleSystem rightDashEffect;

    [Header("Audio")]
    [SerializeField] AudioClip clip;
    

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        Bounds();
        Moving();
        Scaling();
    }

    void MoveCharacter(Vector3 difference)
    {
       rb.MovePosition(transform.position + difference);
    }

    
    void Moving()
    {
        if (Time.timeScale == 1) // If game is paused > can`t move
        {
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (transform.position.x > -3.75)
                {
                    AudioController.instance.PlayMovementClip(clip);
                    MoveCharacter(new Vector3(-xMove, 0, 0));
                    leftDashEffect.Play();
                }
            }
            else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (transform.position.x < 3.75)
                {
                    AudioController.instance.PlayMovementClip(clip);
                    MoveCharacter(new Vector3(xMove, 0, 0));
                    rightDashEffect.Play();
                }
            }
            else if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (transform.position.y < -1.55)
                {
                    AudioController.instance.PlayMovementClip(clip);
                    MoveCharacter(new Vector3(0, yMove, 0));
                }
            }
            else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (transform.position.y > -1.55)
                {
                    AudioController.instance.PlayMovementClip(clip);
                    MoveCharacter(new Vector3(0, -yMove, 0));
                }

            }
        }
    }

    void Scaling()
    {
        if(transform.position.y >= -2.2f)
        {
            transform.localScale = new Vector3(0.9f,0.9f,1f);     
        }        
        else
        {
            transform.localScale = new Vector3(1f,1f,1f);
        }
    }
    void Bounds()
    {
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);
        transform.position = pos;
    }
}

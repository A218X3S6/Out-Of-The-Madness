using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEnemy : MonoBehaviour
{
    public static MoveEnemy instance;

    Rigidbody2D rb;
    Animator animator;

    [SerializeField] GameObject target;

    Vector3 directionToTarget;
    Vector3 scale;

    public float moveSpeed = 2f;
    public float scalingSpeed = 1.5f;

    private bool isScaling = true;

    private void Awake()
    {
        instance = this;   

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    void Start()
    {     
        transform.localScale = new Vector3(0.2f, 0.2f, 0);
        scale = new Vector3(0.1f, 0.1f, 0);

        directionToTarget = transform.position;

        animator.SetInteger("EnemyIndex", Random.Range(0, 3));
        animator.SetTrigger("Enemy");
    }

    void Update()
    {
        Scaling();
        Moving();
        DestroyObject();
    }

    void Moving()
    {
        if (target != null)
        {
            directionToTarget = (target.transform.position - transform.position).normalized;
            rb.velocity = new Vector3(directionToTarget.x * moveSpeed, directionToTarget.y * moveSpeed, 0f);
        }
        else
        {
            rb.velocity = Vector3.zero;
        }
    }

    void Scaling()
    {
        if (isScaling)
        {
            transform.localScale += scale * scalingSpeed * Time.deltaTime;
        }

        if (transform.localScale.x >= 0.9)
        {
            isScaling = false;
        }
    }

    void DestroyObject()
    {
        if (transform.position.y <= -5f)
        {
            Destroy(gameObject);
        }
    }
}


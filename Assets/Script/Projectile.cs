using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Animator animator;
    private CircleCollider2D CircleCollider;
    [SerializeField] private float speed = 1f;
    private bool hit;
    private float direction;
    [SerializeField] private float lifeTime = 5f;
    private float lifeTimer;
    [SerializeField] private int damage = 10;

    void Awake()
    {
        animator = GetComponent<Animator>();
        CircleCollider = GetComponent<CircleCollider2D>();
    }

    void Update()
    {
        if (hit) return;
        float movementSpeed = speed * Time.deltaTime * direction;
        transform.Translate(movementSpeed, 0, 0);

        lifeTimer -= Time.deltaTime;
        if (lifeTimer <= 0)
            Deactivate();
    }

    private void OnTriggerEnter2D(Collider2D hitInfor)
    {
        hit = true;
        CircleCollider.enabled = false;
        if (hitInfor.GetComponent<Health>())
            hitInfor.GetComponent<Health>().TakeDamage(damage);
        animator.SetTrigger("explode");

    }
    public void SetDirection(float _direction)
    {
        lifeTimer = lifeTime;
        direction = _direction;
        gameObject.SetActive(true);
        hit = false;
        CircleCollider.enabled = true;
        float localScaleX = transform.localScale.x;

        if (localScaleX != _direction)
            localScaleX = -localScaleX;
        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }
    void Deactivate()
    {
        gameObject.SetActive(false);
    }
}

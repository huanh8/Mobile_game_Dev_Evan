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
    [SerializeField] private float lifeTime = 4f;
    private float lifeTimer;
    [SerializeField] private int damage = 10;
    private LayerMask whatIsEnemy;
    private LayerMask gameObjectLayer;

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
        if (hitInfor.gameObject.layer == gameObjectLayer) return; // same layer will not hit
        hit = true;
        CircleCollider.enabled = false;
        Health health = hitInfor.GetComponent<Health>();
        if (health != null && health.CurrentHealth > 0)
            // check if the hit object is in the whatIsEnemy layer
            if (((1 << hitInfor.gameObject.layer) & whatIsEnemy) != 0)
                hitInfor.GetComponent<Health>().TakeDamage(damage, transform.gameObject);
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
    public void SetLayers(LayerMask _whatIsEnemy, LayerMask _gameObjectLayer)
    {
        whatIsEnemy = _whatIsEnemy;
        gameObjectLayer = _gameObjectLayer;
    }
}

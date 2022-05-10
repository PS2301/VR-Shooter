using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyController : GazePointer
{

    public GameObject ParticleEffect;
    public Animator enemyModel;
    Vector3 endPos;
    [SerializeField] private float speed;

    BulletSpawner bulletSpawner;

    // Start is called before the first frame update
    void Start()
    {
        endPos = 1.5f * (transform.position - Vector3.zero).normalized;
        bulletSpawner = GameObject.FindObjectOfType<BulletSpawner>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, endPos, speed * Time.deltaTime);
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
        PlayerManager.currentScore += 100;
        Death();
        bulletSpawner.ShootBullet();
    }

    public void Death()
    {
        ParticleEffect.SetActive(true);
        ParticleEffect.transform.SetParent(null);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Attack();
        }
        else if (other.CompareTag("Enemy"))
        {
            Death();
        }
    }

    public void Attack()
    {
        enemyModel.SetTrigger("attack");
        PlayerManager.playerHealth -= 0.2f;
    }
}

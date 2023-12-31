using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BulletBehavior : MonoBehaviour
{
    #region STATS
    protected int damage;
    protected float speed;
    #endregion

    #region Avatar
    [SerializeField] protected SpriteRenderer spriteRenderer;
    [SerializeField] protected TrailRenderer trail;
    [SerializeField] protected ParticleSystem destroyFx;
    #endregion

    protected Vector2 direction;
    protected LayerMask obstacleLayer;
    protected float lifeLength = 30f;
    protected float lifeCount = 0f;

    public abstract void BulletHit();

    void FixedUpdate()
    { 
        if (Vector2.Distance(transform.position, Camera.main.transform.position) > 30f)
            Destroy(gameObject);
        if (lifeCount < lifeLength)
            lifeCount += Time.deltaTime * GameManager.Instance.TimeScale;
        else
            BulletHit();

        Move();
        CheckHit();
    }

    public void SetBulletStats(int damage, float startSpeed, float lifeLength, Vector2 startDirection, Color color, string layerName)
    {
        this.damage = damage;
        this.speed = startSpeed;
        this.direction = startDirection.normalized;
        this.lifeLength = lifeLength;

        spriteRenderer.color = color;
        if (trail != null)
        {
            trail.startColor = color - new Color(0f, 0f, 0f, color.a / 3);
            trail.endColor = color - new Color(0f, 0f, 0f, 1f);
        }

        gameObject.layer = LayerMask.NameToLayer(layerName);
        if (layerName == "PlayerBullet") 
            obstacleLayer = LayerMask.GetMask("Enemy", "ItemCrystal", "Wall");
        else if (layerName == "EnemyBullet")
            obstacleLayer = LayerMask.GetMask("Player", "Wall");
    }

    protected void Move()
    {
        float speedScale = 1f;
        // if (Mathf.Abs(direction.x) > 0.5f && Mathf.Abs(direction.y) > 0.5f)
        // {
        //     speedScale = 1.35f;
        // }

        transform.Translate(direction * speed * speedScale * Time.deltaTime * GameManager.Instance.TimeScale, Space.World);
    }

    protected void CheckHit()
    {
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, transform.localScale.x * 0.5f, direction, 0.001f, obstacleLayer);
        if (hit.collider != null)
        { 
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Enemy")
                || hit.collider.gameObject.layer == LayerMask.NameToLayer("Player"))
                hit.transform.GetComponent<CharacterBehavior>().Damaged(Random.Range(damage - 2, damage + 2));
            
            else if (hit.collider.gameObject.layer == LayerMask.NameToLayer("ItemCrystal"))
                hit.transform.GetComponent<ItemCrystal>().Damaged();
                
            BulletHit();
        }
    }
}

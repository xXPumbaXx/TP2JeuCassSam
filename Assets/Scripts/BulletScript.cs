using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private Vector2 targetPos;
    [SerializeField] Sprite blueBullet;
    [SerializeField] Sprite greenBullet;
    [SerializeField] int bulletSpeed = 3;

    private SpriteRenderer spriteRenderer;
    private GameObject bulletSource;
    private GameManager.Equipe bulletTeam;
    private const int MAX_ATTACK_DAMAGE = 5;

    // Start is called before the first frame update
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 vector2 = Vector2.MoveTowards(transform.position, targetPos, 1) * Time.deltaTime * bulletSpeed;
        
        transform.position = new Vector2(transform.position.x + vector2.x, transform.position.y + vector2.y);
    }

    public void SpawnBullet(Vector2 targetPosition, GameManager.Equipe equipe, GameObject source)
    {
        gameObject.SetActive(true);
        targetPos = targetPosition;
        bulletTeam = equipe;

        if (equipe == GameManager.Equipe.BLEU)
        {
            spriteRenderer.sprite = blueBullet;
        }
        else
        {
            spriteRenderer.sprite = greenBullet;
        }

        bulletSource = source;
        transform.position = source.transform.position;
        targetPos = targetPosition;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wizard" && collision.gameObject.GetComponent<WizardManager>().GetTeam() != bulletTeam)
        {
            collision.gameObject.GetComponent<HpManager>().LoseHp(bulletSource, Random.Range(0, MAX_ATTACK_DAMAGE));
            gameObject.SetActive(false);
        }
        if (collision.gameObject.tag == "Tower" && collision.gameObject.GetComponent<TowerManager>().GetTeam() != bulletTeam)
        {
            collision.gameObject.GetComponent<HpManager>().LoseHp(bulletSource, Random.Range(0, MAX_ATTACK_DAMAGE));
            gameObject.SetActive(false);
        }
    }
}

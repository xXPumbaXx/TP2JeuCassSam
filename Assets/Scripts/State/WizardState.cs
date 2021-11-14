using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class WizardState : MonoBehaviour
{
    //Bullet
    [SerializeField] int bulletPoolSize = 5;
    [SerializeField] private GameObject bulletPrefab;
    private GameObject[] bulletArray;

    protected GameManager gameManager;
    protected WizardManager wizardManager;
    protected Transform towerObjectivePosition;
    protected HpManager hpManager;

    private GameObject attackTarget;
    private HpManager targetHpManager;

    protected float speed;
    protected float shootingDelay;
    protected float magicProjectileSpeed;
    protected float regenTime;
    protected float initialRegenTime = 5.0f;
    protected const float INITIAL_SHOOTING_DELAY = 1.0f;
    private const int MAX_ATTACK_DAMAGE = 5;
    protected GameManager.Equipe team;
    private GameManager.Equipe otherTeam;

    // Start is called before the first frame update
    void Awake()
    {
        wizardManager = GetComponent<WizardManager>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        hpManager = GetComponent<HpManager>();
    }

    private void OnEnable()
    {
        otherTeam = (wizardManager.GetTeam() == GameManager.Equipe.BLEU) ? GameManager.Equipe.VERT : GameManager.Equipe.BLEU;
        towerObjectivePosition = gameManager.getTower(otherTeam).transform;
        team = wizardManager.GetTeam();
        regenTime = initialRegenTime;
    }

    public void ChangeTower()
    {
        while (!towerObjectivePosition.gameObject.activeSelf)
        {
            towerObjectivePosition = gameManager.getTower(otherTeam).transform;
        }
    }

    protected void SetRegenTime(float time)
    {
        initialRegenTime = time;
    }

    protected void Regen()
    {
        regenTime -= Time.deltaTime;
        if (regenTime <= 0)
        {
            hpManager.RegenOneHp();
            regenTime = initialRegenTime;
        }
    }

    protected void SwitchAttackTarget(GameObject target)
    {
        attackTarget = target;
        targetHpManager = attackTarget.GetComponent<HpManager>();
    }

    protected void Attack()
    {
        shootingDelay -= Time.deltaTime;

        if (shootingDelay < 0)
        {
            //Rotation
            Vector2 diference = transform.position - attackTarget.transform.position;
            diference.Normalize();

            float rot_z = Mathf.Atan2(diference.y, diference.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rot_z + 90);

            targetHpManager.LoseHp(gameObject, Random.Range(0, MAX_ATTACK_DAMAGE));
            shootingDelay = INITIAL_SHOOTING_DELAY;
            wizardManager.Attack(attackTarget);
        }
    }

    public void ChangeSpeed(float speedChange)
    {
        speed += speedChange;
    }

    public abstract void WizardBehavior();
    public abstract void ManageStateChange();
}

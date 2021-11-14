using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardManager : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;//Do nothing for now - to initialize
    [SerializeField] Sprite blueWizard;
    [SerializeField] Sprite greenWizard;
    [SerializeField] int bulletPoolSize = 5;

    public enum WizardStateToSwitch { Normal, Intrepide, Fuite, Planquer, Sureté, LastStand, Inactif }

    //Component
    private GameObject[] bulletArray;
    private SpriteRenderer spriteRenderer;
    private WizardState wizardState;
    private GameManager.Equipe team;
    private Sprite wizardSprite;//Do nothing for now

    //Variable
    private int kills;
    private int protection = 0;

    private void Start()
    {
        bulletArray = new GameObject[bulletPoolSize];

        for (int i = 0; i < bulletPoolSize; i++)
        {
            bulletArray[i] = Instantiate(bulletPrefab);
            bulletArray[i].SetActive(false);
        }
    }

    void Awake()
    {
        wizardState = GetComponent<WizardState>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        ChangeWizardState(WizardStateToSwitch.Normal);
        kills = 0;
    }
    public void ChangeWizardState(WizardStateToSwitch nextState)
    {
        Destroy(wizardState);

        switch (nextState)
        {
            case WizardStateToSwitch.Normal:
                wizardState = gameObject.AddComponent<WizardStateNormal>() as WizardStateNormal;
                //wizardState = gameObject.GetComponent<WizardStateNormal>();
                break;
            case WizardStateToSwitch.Intrepide:
                wizardState = gameObject.AddComponent<WizardStateIntrepide>() as WizardStateIntrepide;
                //wizardState = gameObject.GetComponent<WizardStateIntrepide>();
                break;
            case WizardStateToSwitch.Fuite:
                wizardState = gameObject.AddComponent<WizardStateFuite>() as WizardStateFuite;
                //wizardState = gameObject.GetComponent<WizardStateFuite>();
                break;
            case WizardStateToSwitch.Planquer:
                wizardState = gameObject.AddComponent<WizardStatePlanquer>() as WizardStatePlanquer;
                //wizardState = gameObject.GetComponent<WizardStatePlanquer>();
                break;
            case WizardStateToSwitch.Sureté:
                wizardState = gameObject.AddComponent<WizardStateSureté>() as WizardStateSureté;
                //wizardState = gameObject.GetComponent<WizardStateSureté>();
                break;
            case WizardStateToSwitch.LastStand:
                break;
            case WizardStateToSwitch.Inactif:
                wizardState = gameObject.AddComponent<WizardStateInactif>();
                break;
            default:
                break;
        }
    }

    public void Attack(GameObject target)
    {
        for (int i = 0; i < bulletPoolSize; i++)
        {
            if (!bulletArray[i].activeSelf)
            {
                //Spawn
                bulletArray[i].GetComponent<BulletScript>().SpawnBullet(target.transform.position, team, this.gameObject);
                return;
            }
        }
        bulletArray[0].GetComponent<BulletScript>().SpawnBullet(target.transform.position, team, this.gameObject);
    }

    public GameManager.Equipe GetTeam()
    {
        return team;
    }

    public void ChangeTeam(GameManager.Equipe newTeam)
    {
        team = newTeam;
        if (newTeam == GameManager.Equipe.BLEU)
        {
            spriteRenderer.sprite = blueWizard;
        }
        else
        {
            spriteRenderer.sprite = greenWizard;
        }
    }
    public void GameOver()
    {
        gameObject.SetActive(false);
    }

    public void GrantKill()
    {
        kills++;
    }

    public int GetKills()
    {
        return kills;
    }

    public int GetProtection()
    {
        return protection;
    }

    public void SetProtection(int protection)
    {
        this.protection = protection;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Collision
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        //Collision
    }
}

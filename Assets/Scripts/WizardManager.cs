using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardManager : MonoBehaviour
{
    //Serialized Field
    [SerializeField] private int STARTING_HP = 100;
    [SerializeField] private GameObject bulletPrefab;//Do nothing for now - to initialize

    public enum WizardStateToSwitch { Normal, Intrepide, Fuite, Planquer, Suret�, LastStand }

    //Component
    private SpriteRenderer spriteRenderer;
    private WizardState wizardState;
    private GameManager.Equipe team;
    private Sprite wizardSprite;//Do nothing for now

    //WizardStats
    private int currentHp;

    void Awake()
    {
        wizardState = GetComponent<WizardState>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = wizardSprite;

        currentHp = STARTING_HP;
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
            case WizardStateToSwitch.Suret�:
                wizardState = gameObject.AddComponent<WizardStateSuret�>() as WizardStateSuret�;
                //wizardState = gameObject.GetComponent<WizardStateSuret�>();
                break;
            case WizardStateToSwitch.LastStand:
                break;
            default:
                break;
        }
    }

    public void Attack()
    {
        //TO DO
    }

    public GameManager.Equipe GetTeam()
    {
        return team;
    }

    public void ChangeSprite(Sprite spriteChange)
    {
        spriteRenderer.sprite = spriteChange;
    }

    public void ChangeTeam(GameManager.Equipe newTeam)
    {
        team = newTeam;
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

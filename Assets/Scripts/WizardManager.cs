using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardManager : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;//Do nothing for now - to initialize
    [SerializeField] Sprite blueWizard;
    [SerializeField] Sprite greenWizard;

    public enum WizardStateToSwitch { Normal, Intrepide, Fuite, Planquer, Sureté, LastStand, Inactif }

    //Component
    private SpriteRenderer spriteRenderer;
    private WizardState wizardState;
    private GameManager.Equipe team;
    private Sprite wizardSprite;//Do nothing for now

    //Variable
    private int kills;

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

    public void Attack()
    {
        // TODO
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
        if (kills >= 3)
        {
            //ChangeWizardState(WizardStateToSwitch.Intrepide);
            Debug.Log("State change: Intrepide");
            kills = 0;
        }
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

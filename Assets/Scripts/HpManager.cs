using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpManager : MonoBehaviour
{
    [SerializeField] private int maxHp;
    [SerializeField] private int presentHp; // La valeur est sérialisée pour voir les changement dans l'inspecteur d'Unity

    private TowerManager towerManager;
    private WizardManager wizardManager;

    private void Awake()
    {
        presentHp = maxHp;
        towerManager = GetComponent<TowerManager>();
        wizardManager = GetComponent<WizardManager>();
    }

    public void LoseHp(GameObject source, int damage)
    {
        int trueDamage = damage;

        if (gameObject.transform.tag == "Wizard")
        {
            trueDamage = trueDamage - wizardManager.GetProtection();
            if (trueDamage < 0) trueDamage = 0;
        }

        presentHp -= trueDamage;
        if (presentHp <= 0)
        {
            if(gameObject.transform.tag == "Tower")
            {
                towerManager.GameOver();
            }
            else
            {
                wizardManager.GameOver();
            }

            // Le Wizard Manager ici n'est pas le même composant que la propriété "wizardManager"
            // et change continuellement selon la source
            source.GetComponent<WizardManager>().GrantKill();
        }
    }

    public void RegenOneHp()
    {
        if(presentHp < maxHp)
        {
            presentHp++;
        }
    }

    public int GetHpPercent()
    {
        return presentHp*100/maxHp;
    }
}

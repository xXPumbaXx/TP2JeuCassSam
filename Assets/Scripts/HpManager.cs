using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpManager : MonoBehaviour
{
    [SerializeField] private int maxHp;
    [SerializeField] private int presentHp; // La valeur est sérialisée pour voir les changement dans l'inspecteur d'Unity

    private void Awake()
    {
        presentHp = maxHp;
    }

    public void LoseOneHp(GameObject source)
    {
        presentHp--;
        if (presentHp <= 0)
        {
            if(gameObject.transform.tag == "Tower")
            {
                GetComponent<TowerManager>().GameOver();
            }
            else
            {
                GetComponent<WizardManager>().GameOver();
            }

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

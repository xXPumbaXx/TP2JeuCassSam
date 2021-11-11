using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpManager : MonoBehaviour
{
    [SerializeField] int lives;
    private int maxLives;

    private void Awake()
    {
        maxLives = lives;
    }

    public void LoseOneHp(GameObject source)
    {
        lives--;
        if (lives <= 0)
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
        if(lives < maxLives)
        {
            lives++;
        }
    }
}

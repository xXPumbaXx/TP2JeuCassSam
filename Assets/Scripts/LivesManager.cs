using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivesManager : MonoBehaviour
{
    [SerializeField] int lives;

    public void LoseALife()
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
        }
    }
}

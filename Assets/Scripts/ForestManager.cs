using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestManager : MonoBehaviour
{
    private const float speedChange = 0.5f;
    private const int protection = 2;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Wizard")
        {
            AddForestChanges(collision);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag == "Wizard")
        {
            RemoveForestChanges(collision);
        }
    }

    public void AddForestChanges(Collider2D collision)
    {
        // Ralentissement
        collision.gameObject.GetComponent<WizardState>().ChangeSpeed(-speedChange);

        // Ajout de protection
        collision.gameObject.GetComponent<WizardManager>().SetProtection(protection);

    }

    public void RemoveForestChanges(Collider2D collision)
    {
        // Ralentissement
        collision.gameObject.GetComponent<WizardState>().ChangeSpeed(speedChange);

        // Ajout de protection
        collision.gameObject.GetComponent<WizardManager>().SetProtection(0);
    }
}

using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;


public class PlayerInteract_LegacyText : MonoBehaviour
{
    public float range = 3;
    public TextMeshProUGUI interactText;

    InteractableObject interactObject;
    SceneChanger changer;
    Ray lookAt;
    RaycastHit hit;

    // Update is called once per frame
    void Update()
    {
        lookAt = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(lookAt, out hit, range))
        {
            if (hit.collider.TryGetComponent<InteractableObject>(out interactObject))
            {
                interactText.text = interactObject.buttonPrompt;
            }
            else if (hit.collider.TryGetComponent<SceneChanger>(out changer))
            {
                interactText.text = changer.DisplayText;
            }
            else
            {
                if (interactText.text != "")
                {
                    interactText.text = "";
                    interactObject = null;
                }

            }
        }
        else
        {
            if (interactText.text != "")
            {
                interactText.text = "";
                interactObject = null;
            }

        }

        if (Input.GetKeyDown(KeyCode.E) && interactObject != null)
        {
            interactObject.PlayerInteract();
        }
    }

}

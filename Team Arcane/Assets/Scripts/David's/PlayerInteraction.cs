using TMPro;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public float range = 3;
    public TMP_Text interactText;

    Ray lookAt;
    RaycastHit hit;

    // Update is called once per frame
    void Update()
    {
        lookAt = Camera.main.ViewportPointToRay(new Vector3(0f, 0f, 0f));

        if (Physics.Raycast(lookAt, out hit, range))
        {
            if (hit.collider.CompareTag("Item"))
            {
                interactText.text = "[E] to pick up";
            } 
        }
        else
        {
            if (interactText.text != "")
            {
                interactText.text = "";
            }

        }
    }
}

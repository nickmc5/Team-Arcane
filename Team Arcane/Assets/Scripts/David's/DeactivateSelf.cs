using UnityEngine;

public class DeactivateSelf : MonoBehaviour
{
    public void Deactivate()
    {
        PlayerInventory.playerInv.Add("Red Cube", null);
        gameObject.SetActive(false);
    }
}

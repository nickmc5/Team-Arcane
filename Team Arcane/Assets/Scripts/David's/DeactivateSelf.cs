using UnityEngine;

public class DeactivateSelf : MonoBehaviour
{
    public void Deactivate()
    {
        PlayerInventory.playerInv.Add("Red Cube");
        gameObject.SetActive(false);
    }
}

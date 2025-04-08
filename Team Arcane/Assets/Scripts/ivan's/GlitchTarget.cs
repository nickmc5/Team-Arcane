using UnityEngine;

public class GlitchTarget : MonoBehaviour
{
    private void OnEnable()
    {
        ProximityGlitch.RegisterGlitchObject(this.gameObject);
    }

    private void OnDisable()
    {
        ProximityGlitch.UnregisterGlitchObject(this.gameObject);
    }
}

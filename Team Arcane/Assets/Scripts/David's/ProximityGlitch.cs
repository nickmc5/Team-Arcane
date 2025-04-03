using UnityEngine;
using UnityEngine.Rendering;
using URPGlitch;

public class ProximityGlitch : MonoBehaviour
{
    public float range = 10f;

    [Range(0f, 1f)]
    public float maxScanLineIntensity = 1f;
    [Range(0f, 1f)]
    public float maxColorDriftIntensity = 1f;

    [SerializeField]
    private Volume volume;

    private AnalogGlitchVolume analogGlitchVolume;
    private DigitalGlitchVolume digitalGlitchVolume;

    private GameObject[] glitchObjects;

    private void Start()
    {
        volume.profile.TryGet<AnalogGlitchVolume>(out analogGlitchVolume);
        volume.profile.TryGet<DigitalGlitchVolume>(out digitalGlitchVolume);

        glitchObjects = GameObject.FindGameObjectsWithTag("Glitch");
    }

    private void Update()
    {
        float minDistance = float.MaxValue;
        foreach (var obj in glitchObjects)
        {
            float distance = Vector3.Distance(transform.position, obj.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
            }
        }

        if (minDistance < range)
        {
            analogGlitchVolume.active = true;

            float intensity = Mathf.InverseLerp(range, 0, minDistance);
            analogGlitchVolume.colorDrift.value = intensity * maxColorDriftIntensity;
            analogGlitchVolume.scanLineJitter.value = intensity * maxScanLineIntensity;

        }
        else
        {
            analogGlitchVolume.active = false;
        }
    }
}

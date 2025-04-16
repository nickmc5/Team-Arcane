using UnityEngine;
using UnityEngine.Rendering;
using URPGlitch;
using System.Collections.Generic;

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

    private static List<GameObject> glitchObjects = new List<GameObject>();

    public static void RegisterGlitchObject(GameObject obj)
    {
        if (!glitchObjects.Contains(obj))
            glitchObjects.Add(obj);
    }

    public static void UnregisterGlitchObject(GameObject obj)
    {
        Debug.Log("Unregistering glitch object: " + obj.name);
        glitchObjects.Remove(obj);
        Debug.Log("Remaining glitch objects: " + glitchObjects.Count);
    }

    private void Start()
    {
        volume.profile.TryGet(out analogGlitchVolume);
        volume.profile.TryGet(out digitalGlitchVolume);
    }

    private void Update()
    {
        //Debug.Log("Updating proximity glitch...");
        
        if (glitchObjects.Count == 0)
        {
            //Debug.Log("No registered glitch objects.");
            analogGlitchVolume.active = false;
            return;
        }
        float minDistance = float.MaxValue;

        foreach (var obj in glitchObjects)
        {
            if (obj == null || !obj.activeInHierarchy) continue;

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

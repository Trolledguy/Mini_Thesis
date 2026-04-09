using UnityEngine;

public class ProbeInitiator : MonoBehaviour
{
    MeshRenderer meshRender;
#if UNITY_EDITOR
    void OnValidate()
    {
        meshRender = gameObject.GetComponent<MeshRenderer>();
        meshRender.probeAnchor = this.transform;
    }
#endif
}
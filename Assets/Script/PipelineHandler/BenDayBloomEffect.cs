using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;


[VolumeComponentMenuForRenderPipeline("Custom/BloomEffect", typeof(UniversalRenderPipeline))]
public class BenDayBloomEffect : VolumeComponent , IPostProcessComponent
{
    public bool IsActive() => true;
    public bool IsTileCompatible() => false;
}

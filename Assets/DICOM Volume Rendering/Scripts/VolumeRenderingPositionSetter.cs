using UnityEngine;

namespace VolumeRendering
{

public enum VolumeRenderingPlane
{
    MinX,
    MaxX,
    MinY,
    MaxY,
    MinZ,
    MaxZ,
}

[ExecuteInEditMode, RequireComponent(typeof(Renderer))]
public class VolumeRenderingPositionSetter : MonoBehaviour
{
    [SerializeField]
    VolumeRenderingPlane plane = VolumeRenderingPlane.MinX;

    [SerializeField]
    Transform target = null;

    void Update()
    {
        if (!target) return;

        var renderer = GetComponent<Renderer>();
        var mat = renderer.sharedMaterial;
        var wpos = target.position;
        var wpos4 = new Vector4(wpos.x, wpos.y, wpos.z, 1f);
        var lpos = transform.worldToLocalMatrix * wpos4;
        lpos += Vector4.one * 0.5f;

        switch (plane)
        {
            case VolumeRenderingPlane.MinX:
                mat.SetFloat("_MinX", lpos.x);
                break;
            case VolumeRenderingPlane.MaxX:
                mat.SetFloat("_MaxX", lpos.x);
                break;
            case VolumeRenderingPlane.MinY:
                mat.SetFloat("_MinY", lpos.y);
                break;
            case VolumeRenderingPlane.MaxY:
                mat.SetFloat("_MaxY", lpos.y);
                break;
            case VolumeRenderingPlane.MinZ:
                mat.SetFloat("_MinZ", lpos.z);
                break;
            case VolumeRenderingPlane.MaxZ:
                mat.SetFloat("_MaxZ", lpos.z);
                break;
            default:
                break;
        }
    }
}

}

using UnityEngine;

namespace VolumeRendering
{

public class VolumeRenderingPositionSetterPlaneDrawer : MonoBehaviour
{
    public enum Plane
    {
        X,
        Y,
        Z
    }

    public Plane plane = Plane.X;
    public float scale = 5f;
}

}

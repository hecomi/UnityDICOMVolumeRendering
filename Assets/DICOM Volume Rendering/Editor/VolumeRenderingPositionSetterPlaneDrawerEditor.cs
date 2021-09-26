using UnityEngine;
using UnityEditor;

namespace VolumeRendering
{

[CustomEditor(typeof(VolumeRenderingPositionSetterPlaneDrawer)), CanEditMultipleObjects]
public class VolumeRenderingPositionSetterPlaneDrawerEditor : Editor
{
    void OnSceneGUI()
    {
        var drawer = target as VolumeRenderingPositionSetterPlaneDrawer;

        var plane = drawer.plane;
        var pos = drawer.transform.position;
        var scale = drawer.scale;
        var x = drawer.transform.right * scale;
        var y = drawer.transform.up * scale;
        var z = drawer.transform.forward * scale;
        var dir1 =
            plane == VolumeRenderingPositionSetterPlaneDrawer.Plane.X ? y :
            plane == VolumeRenderingPositionSetterPlaneDrawer.Plane.Y ? z :
            plane == VolumeRenderingPositionSetterPlaneDrawer.Plane.Z ? y :
            Vector3.zero;
        var dir2 =
            plane == VolumeRenderingPositionSetterPlaneDrawer.Plane.X ? z :
            plane == VolumeRenderingPositionSetterPlaneDrawer.Plane.Y ? x :
            plane == VolumeRenderingPositionSetterPlaneDrawer.Plane.Z ? x :
            Vector3.zero;
        var p1 = pos - dir1 - dir2;
        var p2 = pos - dir1 + dir2;
        var p3 = pos + dir1 + dir2;
        var p4 = pos + dir1 - dir2;
        var color =
            plane == VolumeRenderingPositionSetterPlaneDrawer.Plane.X ? Color.red :
            plane == VolumeRenderingPositionSetterPlaneDrawer.Plane.Y ? Color.green :
            plane == VolumeRenderingPositionSetterPlaneDrawer.Plane.Z ? Color.blue :
            Color.white;
        var preColor = Handles.color;
        Handles.color = color;
        Handles.DrawLine(p1, p2);
        Handles.DrawLine(p2, p3);
        Handles.DrawLine(p3, p4);
        Handles.DrawLine(p4, p1);
        Handles.color = preColor;
    }
}

}

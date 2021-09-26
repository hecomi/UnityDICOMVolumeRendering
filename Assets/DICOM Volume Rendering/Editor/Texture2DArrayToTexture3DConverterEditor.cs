using UnityEngine;
using UnityEditor;

namespace VolumeRendering
{

[CustomEditor(typeof(Texture2DArrayToTexture3DConverter))]
public class Texture2DArrayToTexture3DConverterEditor : Editor
{
    string error;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Convert"))
        {
            error = "";
            Convert();
        }

        if (!string.IsNullOrEmpty(error))
        {
            EditorGUILayout.HelpBox(error, MessageType.Error);
        }
    }

    bool Convert()
    {
        var converter = target as Texture2DArrayToTexture3DConverter;
        var tex2dArray = converter.texture2DArray;
        tex2dArray.Reverse();
        if (tex2dArray.Count == 0)
        {
            error = "no image";
            return false;
        }

        var w = tex2dArray[0].width;
        var h = tex2dArray[0].height;
        var d = tex2dArray.Count;
        var format = tex2dArray[0].format;
        var colors = new Color32[w * h * d];

        for (int i = 0; i < d; ++i)
        {
            var tex2d = tex2dArray[i];
            if (tex2d.width != w || tex2d.height != h)
            {
                error = "texture size error";
                return false;
            }
            if (tex2d.format != format)
            {
                error = "texture format error";
                return false;
            }
            tex2d.GetPixels32().CopyTo(colors, w * h * i);
        }

        var tex3d = new Texture3D(w, h, d, format, false);
        tex3d.SetPixels32(colors);
        tex3d.Apply();

        var path = EditorUtility.SaveFilePanelInProject(
            "Texture3D",
            $"New Texture3D Asset",
            "asset",
            "Save Texture3D");
        AssetDatabase.CreateAsset(tex3d, path);
        AssetDatabase.SaveAssets();

        return true;
    }
}

}

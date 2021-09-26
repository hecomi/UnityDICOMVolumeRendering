using UnityEngine;

using System;

namespace VolumeRendering
{

[UnityEditor.AssetImporters.ScriptedImporter(1, "dcm")]
public class PvmRawImporter2 : UnityEditor.AssetImporters.ScriptedImporter
{
    public override void OnImportAsset(UnityEditor.AssetImporters.AssetImportContext ctx)
    {
        try
        {
            var image = new Dicom.Imaging.DicomImage(ctx.assetPath);
            var tex2d = image.RenderImage().As<Texture2D>();
            ctx.AddObjectToAsset("Volume", tex2d);
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
    }
}

}

#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.IO;
using GameKit;

public class SpriteSplit
{

    [MenuItem(GameKitConfig.EditorToolKitPath + "Sprite Split")]
    public static void DoSplitTexture()
    {
        Texture2D selectedImg = Selection.activeObject as Texture2D;
        string rootPath = Path.GetDirectoryName(AssetDatabase.GetAssetPath(selectedImg));
        string path = rootPath + "/" + selectedImg.name + ".png";
        TextureImporter texImp = AssetImporter.GetAtPath(path) as TextureImporter;
        texImp.isReadable = true;
        texImp.spritePixelsPerUnit = 320;
        AssetDatabase.ImportAsset(path);
        AssetDatabase.CreateFolder(rootPath, selectedImg.name);


        foreach (SpriteMetaData metaData in texImp.spritesheet)
        {

            var width = (int)metaData.rect.width;
            var height = (int)metaData.rect.height;

            Texture2D smallImg = new Texture2D(width, height);

            var pixelStartX = (int)metaData.rect.x;
            var pixelEndX = pixelStartX + width;
            var pixelStartY = (int)metaData.rect.y;
            var pixelEndY = pixelStartY + height;
            Color[] colors = selectedImg.GetPixels((int)metaData.rect.xMin, (int)metaData.rect.yMin, width, height);
            smallImg.SetPixels(0, 0, width, height, colors);

            //  EncodeToPNG兼容
            if (TextureFormat.ARGB32 != smallImg.format && TextureFormat.RGB24 != smallImg.format)
            {
                Texture2D img = new Texture2D(smallImg.width, smallImg.height);
                img.SetPixels(smallImg.GetPixels(0), 0);
                smallImg = img;
            }

            string smallImgPath = rootPath + "/" + selectedImg.name + "/" + metaData.name + ".png";
            File.WriteAllBytes(smallImgPath, smallImg.EncodeToPNG());
            // 刷新资源窗口界面
            AssetDatabase.Refresh();
            TextureImporter smallTextureImp = AssetImporter.GetAtPath(smallImgPath) as TextureImporter;
            TextureImporterSettings texSettings = new TextureImporterSettings();
            smallTextureImp.ReadTextureSettings(texSettings);
            texSettings.spriteAlignment = (int)SpriteAlignment.Custom;
            smallTextureImp.SetTextureSettings(texSettings);
            smallTextureImp.isReadable = true;
            smallTextureImp.spritePivot = Vector2.zero;
            smallTextureImp.spritePixelsPerUnit = 320;
            smallTextureImp.alphaIsTransparency = true;
            smallTextureImp.mipmapEnabled = false;
            AssetDatabase.ImportAsset(smallImgPath);
        }
    }
}
#endif
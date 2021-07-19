using System.IO;
using UnityEditor;
using UnityEngine;

namespace com.tibor0991.gradient
{
    public class GradientTextureEditor : EditorWindow
    {
        private string m_TextureName;
        private int m_TextureResolution;
        private Gradient m_Gradient;
        private string m_FilePath;

        private enum Extension { PNG, JPG, TGA };
        private Extension m_Extension;

        private Gradient neutralGradient => new Gradient()
        {
            colorKeys = new GradientColorKey[2] {
                new GradientColorKey(Color.white, 0),
                new GradientColorKey(Color.white, 1)
            },

            alphaKeys = new GradientAlphaKey[2]
            {
                new GradientAlphaKey(1,0),
                new GradientAlphaKey(1,1)
            }
        };

        [MenuItem("Window/Gradient Strip Exporter")]
        static void Init()
        {
            // Get existing open window or if none, make a new one:
            GradientTextureEditor window = (GradientTextureEditor)EditorWindow.GetWindow(typeof(GradientTextureEditor), true, "Gradient Texture Exporter");
            window.Show();
        }

        private void OnEnable()
        {
            m_FilePath = Application.dataPath;
            m_Extension = Extension.PNG;
            m_Gradient = neutralGradient;
        }

        void OnGUI()
        {
            GUILayout.Label("Settings", EditorStyles.boldLabel);
            m_TextureName = EditorGUILayout.TextField("Name", m_TextureName);

            m_Extension = (Extension)EditorGUILayout.EnumPopup("Extension", m_Extension);

            m_TextureResolution = EditorGUILayout.IntField("Resolution", m_TextureResolution);
            m_Gradient = EditorGUILayout.GradientField("Gradient", m_Gradient);

            GUILayout.Space(20f);
            GUILayout.Label("Export", EditorStyles.boldLabel);
            EditorGUILayout.BeginHorizontal();

            GUILayout.Label("File Path:", GUILayout.ExpandWidth(false));
            m_FilePath = GUILayout.TextField(m_FilePath);

            if (GUILayout.Button("Folder"))
            {
                m_FilePath = EditorUtility.OpenFolderPanel("Choose destination...", m_FilePath, null);
            }

            if (GUILayout.Button("Save Texture"))
            {
                ExportGradientTexture();
            }

            EditorGUILayout.EndHorizontal();
        }

        public void ExportGradientTexture()
        {
            string fullPath = $"{Path.Combine(m_FilePath, m_TextureName)}.{m_Extension.ToString().ToLower()}";

            Texture2D gradientTexture = m_Gradient.ConvertToTextureStrip(m_TextureResolution);

            byte[] encodedTexture;
            switch (m_Extension)
            {
                case Extension.PNG:
                    encodedTexture = gradientTexture.EncodeToPNG();
                    break;
                case Extension.JPG:
                    encodedTexture = gradientTexture.EncodeToJPG();
                    break;
                case Extension.TGA:
                    encodedTexture = gradientTexture.EncodeToTGA();
                    break;
                default:
                    encodedTexture = null;
                    Debug.LogError("This should not happen!");
                    break;
            }
            if (encodedTexture != null)
            {
                File.WriteAllBytes(fullPath, encodedTexture);
                DestroyImmediate(gradientTexture);
                AssetDatabase.Refresh();
            }
        }
    }
}
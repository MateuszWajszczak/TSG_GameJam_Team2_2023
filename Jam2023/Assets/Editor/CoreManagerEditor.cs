using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CoreManager))] // Replace "GameManager" with the actual name of your game manager script
public class GameManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        CoreManager gameManager = (CoreManager)target;

        foreach (var keyValuePair in gameManager.GetKeyStatusDictionary())
        {
            int keyID = keyValuePair.Key;
            bool isCollected = keyValuePair.Value;

            EditorGUILayout.LabelField($"Key ID: {keyID}", $"Collected: {isCollected}");
        }

        foreach (var collectibleValuePair in gameManager.GetCollectibleStatusDictionary())
        {
            int collectibleID = collectibleValuePair.Key;
            bool isCollected = collectibleValuePair.Value;

            EditorGUILayout.LabelField($"Collectible ID: {collectibleID}", $"Collected: {isCollected}");
        }

        // Draw the default Inspector fields (if any)
        DrawDefaultInspector();
    }
}

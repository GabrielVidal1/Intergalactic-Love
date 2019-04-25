using UnityEditor;
using UnityEngine;

public class MyWindow : EditorWindow
{

    Planet planet;

    // Add menu item named "My Window" to the Window menu
    [MenuItem("Window/Planet Orientation Utility")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(MyWindow));
    }

    void OnGUI()
    {

        planet = (Planet)EditorGUILayout.ObjectField("Planet", planet, typeof(Planet), true);
        if (planet)
        {
            if (GUILayout.Button("Orient Selected Objects"))
            {
                GameObject[] selectedObjects = Selection.gameObjects;

                foreach (GameObject obj in selectedObjects)
                {
                    Vector3 localUp = (obj.transform.position - planet.Center).normalized;
                    Quaternion rot = Quaternion.FromToRotation(obj.transform.up, localUp);
                    obj.transform.rotation = rot;
                }
            }
        }
    }
}
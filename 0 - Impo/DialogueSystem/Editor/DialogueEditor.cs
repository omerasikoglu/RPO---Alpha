using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

public class DialogueEditor : EditorWindow
{
    [MenuItem("Window/Dialogue Editor")] //Window tab'ine ekleme
    public static void ShowEditorWindow()
    {
        GetWindow(typeof(DialogueEditor), false, "Dialogue Editor");
    }

    [OnOpenAsset(1)] //ScriptableObject'e çift týklayýnca diyalog penceresinin açýlmasý
    public static bool OnOpenAsset(int instanceID, int line)
    {
        Dialogue dialogue = EditorUtility.InstanceIDToObject(instanceID) as Dialogue;
        if(dialogue != null)
        {
            ShowEditorWindow();
            return true;
        }

        return false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR 
using UnityEditor;
#endif
using UnityEngine.SceneManagement;

public class DebugScripts : EditorWindow
{
    public static DebugScripts winst;
    [MenuItem("Window/CustomWindows/Test")]
    public static void OpenWindow()
    {
        winst = GetWindow<DebugScripts>(false, "Test");
        winst.Focus();
        winst.Show();
    }
    private void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 90, 30), "time x3"))
        {
            Time.timeScale = 3;
        }
        if (GUI.Button(new Rect(100, 10, 90, 30), "time x0.5"))
        {
            Time.timeScale = 0.5f;
        }
        if (GUI.Button(new Rect(190, 10, 90, 30), "time x1"))
        {
            Time.timeScale = 1;
        }
    }
}

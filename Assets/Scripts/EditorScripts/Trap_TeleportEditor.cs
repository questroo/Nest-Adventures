using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Trap_Teleport))]
public class Trap_TeleportEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        Trap_Teleport teleportScript = (Trap_Teleport)target;
        if (GUILayout.Button("Create New Teleport Position"))
        {
            teleportScript.MakeNewTeleportPosition();
        }
    }
}

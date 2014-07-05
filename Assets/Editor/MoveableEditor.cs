using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(Moveable))]
public class MoveableEditor : Editor
{
	public override void OnInspectorGUI ()
	{
		Moveable moveable = (Moveable)target;

		DrawDefaultInspector();

		GUILayout.Label("Set Positions");

		GUILayout.BeginHorizontal();

		if (GUILayout.Button("Set Start"))
		{
			moveable.SetStartPosition();
			EditorUtility.SetDirty(moveable);
		}

		if (GUILayout.Button("Set End"))
		{
			moveable.SetEndPosition();
			EditorUtility.SetDirty(moveable);
		}

		GUILayout.EndHorizontal();

		GUILayout.Label("Show Positions");

		GUILayout.BeginHorizontal();
		
		if (GUILayout.Button("Show Start"))
		{
			moveable.ShowStartPosition();
			EditorUtility.SetDirty(moveable);
		}

		if (GUILayout.Button("Show End"))
		{
			moveable.ShowEndPosition();
			EditorUtility.SetDirty(moveable);
		}
		
		GUILayout.EndHorizontal();
	}
}

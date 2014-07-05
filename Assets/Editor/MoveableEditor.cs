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
		}

		if (GUILayout.Button("Set End"))
		{
			moveable.SetEndPosition();
		}

		GUILayout.EndHorizontal();

		GUILayout.Label("Show Positions");

		GUILayout.BeginHorizontal();
		
		if (GUILayout.Button("Show Start"))
		{
			moveable.ShowStartPosition();
		}
		
		if (GUILayout.Button("Show End"))
		{
			moveable.ShowEndPosition();
		}
		
		GUILayout.EndHorizontal();
	}
}

 #if UNITY_EDITOR
     // Editor specific code here
using UnityEditor;
using UnityEngine;
using System;

public class PolygonCollider2DEditor : EditorWindow {

	[MenuItem("Window/PolygonCollider2D Snap")]
	public static void ShowWindow() {
		EditorWindow.GetWindow (typeof(PolygonCollider2DEditor));
	}

	PolygonCollider2D edge;
	Vector2[] vertices = new Vector2[0];

	void OnGUI()
	{
		GUILayout.Label ("PolygonCollider2D point editor", EditorStyles.boldLabel);
		edge = (PolygonCollider2D) EditorGUILayout.ObjectField("PolygonCollider2D to edit", edge, typeof(PolygonCollider2D), true);
		if (vertices.Length != 0) {
			for (int i = 0; i < vertices.Length; ++i) {
				vertices[i] = (Vector2) EditorGUILayout.Vector2Field("Element "+i, vertices[i]);
			}
		}

		if (GUILayout.Button ("Retrieve")) {
			vertices = edge.points;
		}

		if (GUILayout.Button ("Set")) {
			edge.points = vertices;
		}
	}

/*
	void OnSelectionChange() {
		if (Selection.gameObjects.Length == 1) {
			PolygonCollider2D aux = Selection.gameObjects[0].GetComponent<PolygonCollider2D>();

			if (aux) {
				edge = aux;
				vertices = edge.points;
			}
		}
	}
*/
}
 #endif
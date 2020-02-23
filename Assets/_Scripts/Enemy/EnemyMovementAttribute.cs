using System;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditorInternal;
#endif
public class EmitPrefabAttribute : PropertyAttribute { }
#if UNITY_EDITOR
[CanEditMultipleObjects]
[CustomEditor(typeof(AreaEnemy))]
public class AreaEnemyDrawer : Editor
{
	private ReorderableList RL;
	private SerializedProperty ListProp;

	private void OnEnable()
	{
		ListProp = serializedObject.FindProperty("EnemyList");
		RL = new ReorderableList(serializedObject, ListProp);

		RL.drawHeaderCallback = (rect) =>
		{
			EditorGUI.LabelField(rect, "EnemyMovements");
		};
		RL.drawElementCallback = (rect, index, isActive, isFocused) =>
		{
			var element = ListProp.GetArrayElementAtIndex(index);
			var foldoutRect = new Rect(rect)
			{
				height = EditorGUIUtility.singleLineHeight,
				x = 45
			};
			element.isExpanded = EditorGUI.Foldout(foldoutRect, element.isExpanded, "Element" + index.ToString());
			if (element.isExpanded)
			{
				EditorGUI.PropertyField(rect, element);
			}
		};
		RL.elementHeightCallback = (index) =>
		{
			if (RL.serializedProperty.GetArrayElementAtIndex(index).isExpanded)
				return EditorGUIUtility.singleLineHeight * 11 + 10;
			else
				return EditorGUIUtility.singleLineHeight * 1;
		};
	}
	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();
		serializedObject.Update();
		RL.DoLayoutList();
		serializedObject.ApplyModifiedProperties();
	}
}
[CustomPropertyDrawer(typeof(AreaEnemy.EnemyMovement))]
public class EnemyMovementAttribute : PropertyDrawer
{
	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
	{
		using (new EditorGUI.PropertyScope(position, label, property))
		{
			position.height = EditorGUIUtility.singleLineHeight;
			var ele1 = property.FindPropertyRelative("EnemyReference");
			var ele2 = property.FindPropertyRelative("EnemyBezier");
			var ele3 = property.FindPropertyRelative("SpeedCurve");
			var ele4 = property.FindPropertyRelative("SpeedRatio");
			var ele5 = property.FindPropertyRelative("EnemyPos");
			var ele6 = property.FindPropertyRelative("BezierPos");
			var ele7 = property.FindPropertyRelative("BezierRot");
			var ele8 = property.FindPropertyRelative("InitTime");
			var ele9 = property.FindPropertyRelative("health");

			var ele1Rect = new Rect(position)
			{
				height = position.height,
				y = position.y + EditorGUIUtility.singleLineHeight + 2
			};
			var ele2Rect = new Rect(position)
			{
				height = position.height,
				y = ele1Rect.y + EditorGUIUtility.singleLineHeight + 2
			};
			var ele3Rect = new Rect(position)
			{
				height = position.height * 1,
				y = ele2Rect.y + EditorGUIUtility.singleLineHeight + 2
			};
			var ele4Rect = new Rect(position)
			{
				height = position.height * 1,
				y = ele3Rect.y + EditorGUIUtility.singleLineHeight + 2
			};
			var ele5Rect = new Rect(position)
			{
				height = position.height * 2,
				y = ele4Rect.y + EditorGUIUtility.singleLineHeight + 2
			};
			var ele6Rect = new Rect(position)
			{
				height = position.height * 1,
				y = ele5Rect.y + EditorGUIUtility.singleLineHeight + 2
			};
			var ele7Rect = new Rect(position)
			{
				height = position.height,
				y = ele6Rect.y + EditorGUIUtility.singleLineHeight + 2
			};
			var ele8Rect = new Rect(position)
			{
				height = position.height * 1,
				y = ele7Rect.y + EditorGUIUtility.singleLineHeight + 2
			};
			var ele9Rect = new Rect(position)
			{
				height = position.height * 1,
				y = ele8Rect.y + EditorGUIUtility.singleLineHeight + 2
			};
			EditorGUI.PropertyField(ele1Rect, ele1);
			EditorGUI.PropertyField(ele2Rect, ele2);
			EditorGUI.PropertyField(ele3Rect, ele3);
			EditorGUI.PropertyField(ele4Rect, ele4);
			EditorGUI.PropertyField(ele5Rect, ele5);
			EditorGUI.PropertyField(ele6Rect, ele6);
			EditorGUI.PropertyField(ele7Rect, ele7);
			EditorGUI.PropertyField(ele8Rect, ele8);
			EditorGUI.PropertyField(ele9Rect, ele9);
		}
	}
}
#endif
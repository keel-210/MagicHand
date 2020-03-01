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
	List<bool> isExpandeds = new List<bool>();
	bool isInitialized = false;
	private void OnEnable()
	{
		ListProp = serializedObject.FindProperty("EnemyList");
		RL = new ReorderableList(serializedObject, ListProp);
		if (!isInitialized)
		{
			isExpandeds = new List<bool>();
			for (int i = 0; i < RL.count; i++)
				isExpandeds.Add(RL.serializedProperty.GetArrayElementAtIndex(i).isExpanded);
			isInitialized = true;
		}
		RL.drawHeaderCallback = (rect) =>
		{
			EditorGUI.LabelField(rect, "EnemyMovements");
		};
		RL.onChangedCallback = (list) =>
		{
			isExpandeds = new List<bool>(list.count);
			for (int i = 0; i < list.count; i++)
				isExpandeds[i] = list.serializedProperty.GetArrayElementAtIndex(i).isExpanded;
		};
		RL.drawElementCallback = (rect, index, isActive, isFocused) =>
		{
			var element = RL.serializedProperty.GetArrayElementAtIndex(index);
			var foldoutRect = new Rect(rect)
			{
				height = EditorGUIUtility.singleLineHeight,
				width = 100,
				x = 45
			};
			element.isExpanded = EditorGUI.Foldout(foldoutRect, element.isExpanded, index.ToString());
			isExpandeds[index] = element.isExpanded;
			EditorGUI.PropertyField(rect, element);
		};
		RL.elementHeightCallback = (index) =>
		{
			if (!isExpandeds[index])
				return EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
			return (EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing) * 10;
		};
	}
	public override void OnInspectorGUI()
	{
		EditorGUI.BeginChangeCheck();
		serializedObject.Update();

		if (GUILayout.Button("Fold All Enemy Movements"))
			for (int i = 0; i < RL.count; i++)
				RL.serializedProperty.GetArrayElementAtIndex(i).isExpanded = false;

		RL.DoLayoutList();
		if (EditorGUI.EndChangeCheck())
			serializedObject.ApplyModifiedProperties();

		if (GUILayout.Button("Fold All Enemy Movements"))
			for (int i = 0; i < RL.count; i++)
				RL.serializedProperty.GetArrayElementAtIndex(i).isExpanded = false;
		if (GUILayout.Button("Open All Enemy Movements"))
			for (int i = 0; i < RL.count; i++)
				RL.serializedProperty.GetArrayElementAtIndex(i).isExpanded = true;
	}
}
[CustomPropertyDrawer(typeof(AreaEnemy.EnemyMovement))]
public class EnemyMovementAttribute : PropertyDrawer
{
	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
	{
		using (new EditorGUI.PropertyScope(position, label, property))
		{
			float labelWidth = EditorGUIUtility.labelWidth;
			EditorGUIUtility.labelWidth = 120;
			var ele1 = property.FindPropertyRelative("EnemyReference");
			var ele1Rect = new Rect(position)
			{
				height = position.height,
				width = position.width - 70,
				x = 100,
				y = position.y
			};
			EditorGUI.PropertyField(ele1Rect, ele1);
			if (!property.isExpanded)
				return;
			EditorGUIUtility.labelWidth = labelWidth;
			position.height = EditorGUIUtility.singleLineHeight;
			var ele2 = property.FindPropertyRelative("EnemyBezier");
			var ele3 = property.FindPropertyRelative("SpeedCurve");
			var ele4 = property.FindPropertyRelative("SpeedRatio");
			var ele5 = property.FindPropertyRelative("EnemyPos");
			var ele6 = property.FindPropertyRelative("EnemyRot");
			var ele7 = property.FindPropertyRelative("BezierPos");
			var ele8 = property.FindPropertyRelative("BezierRot");
			var ele9 = property.FindPropertyRelative("InitTime");
			var ele10 = property.FindPropertyRelative("health");


			var ele2Rect = new Rect(position)
			{
				height = position.height,
				y = ele1Rect.y + EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing
			};
			var ele3Rect = new Rect(position)
			{
				height = position.height,
				y = ele2Rect.y + EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing
			};
			var ele4Rect = new Rect(position)
			{
				height = position.height,
				y = ele3Rect.y + EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing
			};
			var ele5Rect = new Rect(position)
			{
				height = position.height,
				y = ele4Rect.y + EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing
			};
			var ele6Rect = new Rect(position)
			{
				height = position.height,
				y = ele5Rect.y + EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing
			};
			var ele7Rect = new Rect(position)
			{
				height = position.height,
				y = ele6Rect.y + EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing
			};
			var ele8Rect = new Rect(position)
			{
				height = position.height,
				y = ele7Rect.y + EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing
			};
			var ele9Rect = new Rect(position)
			{
				height = position.height,
				y = ele8Rect.y + EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing
			};
			var ele10Rect = new Rect(position)
			{
				height = position.height,
				y = ele9Rect.y + EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing
			};
			EditorGUI.PropertyField(ele2Rect, ele2);
			EditorGUI.PropertyField(ele3Rect, ele3);
			EditorGUI.PropertyField(ele4Rect, ele4);
			EditorGUI.PropertyField(ele5Rect, ele5);
			EditorGUI.PropertyField(ele6Rect, ele6);
			EditorGUI.PropertyField(ele7Rect, ele7);
			EditorGUI.PropertyField(ele8Rect, ele8);
			EditorGUI.PropertyField(ele9Rect, ele9);
			EditorGUI.PropertyField(ele10Rect, ele10);
		}
	}
}
#endif
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Character))]
public class CharacterEditor : Editor
{
    SerializedProperty health;
    SerializedProperty speed;
    SerializedProperty acceleration;
    SerializedProperty airAccelerationMultiplier;
    SerializedProperty drag;
    SerializedProperty jumpForce;

    SerializedProperty weapon;
    SerializedProperty weaponPosition;
    SerializedProperty weaponArm;

    SerializedProperty item;

    SerializedProperty moveLeft;
    SerializedProperty moveRight;
    SerializedProperty jump;
    SerializedProperty interact;
    SerializedProperty attack;

    protected Character targetCharacter;

    protected virtual void OnEnable()
    {
        targetCharacter = (Character)target;

        health = serializedObject.FindProperty("health");
        speed = serializedObject.FindProperty("speed");
        acceleration = serializedObject.FindProperty("acceleration");
        airAccelerationMultiplier = serializedObject.FindProperty("airAccelerationMultiplier");
        drag = serializedObject.FindProperty("drag");
        jumpForce = serializedObject.FindProperty("jumpForce");

        weapon = serializedObject.FindProperty("weapon");
        weaponPosition = serializedObject.FindProperty("weaponPosition");
        weaponArm = serializedObject.FindProperty("weaponArm");

        item = serializedObject.FindProperty("item");

        moveLeft = serializedObject.FindProperty("moveLeft");
        moveRight = serializedObject.FindProperty("moveRight");
        jump = serializedObject.FindProperty("jump");
        interact = serializedObject.FindProperty("interact");
        attack = serializedObject.FindProperty("attack");
    }

    bool showCharacterProperties = true;
    bool showWeaponProperties = true;
    bool showItemProperties = true;
    bool showKeyBindingProperties = true;

    GUIContent healthLbl = new GUIContent("Health", "Player's health.");
    GUIContent speedLbl = new GUIContent("Speed", "Maximum speed of the player.");
    GUIContent accelLbl = new GUIContent("Acceleration", "Acceleration of the player.");
    GUIContent airAccelLbl = new GUIContent("Air Acceleration Multiplier", "Multiplier to the acceleration when the player is in the air.");
    GUIContent dragLbl = new GUIContent("Drag", "Drag acceleration when the player is not pressing the move key.");
    GUIContent jumpForceLbl = new GUIContent("Jump Force", "Force to jump.");

    GUIContent itemLbl = new GUIContent("Item", "Item to use");

    GUIContent weaponLbl = new GUIContent("Weapon", "Weapon to use");
    GUIContent weaponPosLbl = new GUIContent("Weapon Position", "Child of the arm which will hold the weapon where the weapon will be.");
    GUIContent weaponArmLbl = new GUIContent("Weapon Arm", "Bone of the arm which will hold the weapon");

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        showCharacterProperties = EditorGUILayout.Foldout(showCharacterProperties, "Character Properties", true);
        EditorGUI.indentLevel++;
        if (showCharacterProperties)
        {
            health.floatValue = EditorGUILayout.Slider(healthLbl, health.floatValue, 0, 1000);
            EditorGUILayout.Space();

            speed.floatValue = EditorGUILayout.Slider(speedLbl, speed.floatValue, 0.0001f, 30f);
            acceleration.floatValue = EditorGUILayout.Slider(accelLbl, acceleration.floatValue, 0.0001f, 60f);
            airAccelerationMultiplier.floatValue = EditorGUILayout.Slider(airAccelLbl, airAccelerationMultiplier.floatValue, 0f, 1f);
            drag.floatValue = EditorGUILayout.Slider(dragLbl, speed.floatValue, 0.0001f, 120f);
            jumpForce.floatValue = EditorGUILayout.Slider(jumpForceLbl, jumpForce.floatValue, 0.0001f, 500f);
        }
        EditorGUI.indentLevel--;
        EditorGUILayout.Space();

        showWeaponProperties = EditorGUILayout.Foldout(showWeaponProperties, "Weapon Properties", true);
        EditorGUI.indentLevel++;
        if (showWeaponProperties)
        {
            EditorGUILayout.ObjectField(weapon, weaponLbl);
            EditorGUILayout.ObjectField(weaponPosition, weaponPosLbl);
            EditorGUILayout.ObjectField(weaponArm, weaponArmLbl);
        }
        EditorGUI.indentLevel--;
        EditorGUILayout.Space();

        showItemProperties = EditorGUILayout.Foldout(showItemProperties, "Item Properties", true);
        EditorGUI.indentLevel++;
        if (showItemProperties)
        {
            EditorGUILayout.ObjectField(item, itemLbl);
        }
        EditorGUI.indentLevel--;
        EditorGUILayout.Space();

        showKeyBindingProperties = EditorGUILayout.Foldout(showKeyBindingProperties, "Key Bindings", true);
        EditorGUI.indentLevel++;
        if (showKeyBindingProperties)
        {
            EditorGUILayout.PropertyField(moveLeft);
            EditorGUILayout.PropertyField(moveRight);
            EditorGUILayout.PropertyField(jump);
            EditorGUILayout.PropertyField(interact);
            EditorGUILayout.PropertyField(attack);
        }
        EditorGUI.indentLevel--;

        serializedObject.ApplyModifiedProperties();
    }
}
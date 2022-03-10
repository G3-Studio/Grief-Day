/*
MIT License

Copyright(c) 2019 Mitchel Thompson
www.angryarugula.com

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.LowLevel;

[CustomEditor(typeof(KeyboardSplitter))]
public class KeboardSplitterInspector : Editor
{
    int pickerId;

    public override void OnInspectorGUI()
    {
        KeyboardSplitter keyboardSplitter = (KeyboardSplitter)target;
        EditorGUI.BeginDisabledGroup(keyboardSplitter.players == null || keyboardSplitter.players.Count == 0);
        if (GUILayout.Button("Import Bindings"))
            OpenInputActionAssetPicker();
        EditorGUI.EndDisabledGroup();

        if (Event.current.commandName == "ObjectSelectorUpdated" && EditorGUIUtility.GetObjectPickerControlID() == pickerId)
        {
            InputActionAsset asset = (InputActionAsset)EditorGUIUtility.GetObjectPickerObject();
            AddInputActionAssetBindings(asset);
            pickerId = 0;
        }
        base.OnInspectorGUI();
    }

    void AddInputActionAssetBindings(InputActionAsset asset)
    {
        if (asset == null)
            return;

        KeyboardSplitter keyboardSplitter = (KeyboardSplitter)target;

        foreach (var p in keyboardSplitter.players)
        {
            foreach (var map in asset.actionMaps)
            {
                foreach (var binding in map.bindings)
                {
                    if (binding.path.StartsWith("<Keyboard>"))
                    {
                        var route = new KeyboardSplitter.KeyRemap(binding);

                        if (p.routes.Count(x => x.original == route.original) == 0)
                            p.routes.Add(route);
                    }
                }
            }
        }

        serializedObject.ApplyModifiedProperties();
    }

    void OpenInputActionAssetPicker()
    {
        pickerId = EditorGUIUtility.GetControlID(FocusType.Passive) + 100;
        EditorGUIUtility.ShowObjectPicker<InputActionAsset>(null, false, "", pickerId);
    }
}

[CustomPropertyDrawer(typeof(KeyboardSplitter.KeyRemap))]
public class KeyRemapDrawer : PropertyDrawer
{
    //static event System.Action<Key> onListen;
    static SerializedProperty listeningProp;
    static bool listening;

    static void ListenForKey(SerializedProperty prop)
    {
        InputSystem.onEvent -= OnEvent;
        InputSystem.onEvent += OnEvent;
        listeningProp = prop;
    }

    static void StopListening()
    {
        InputSystem.onEvent -= OnEvent;
        listeningProp = null;
    }

    static unsafe void OnEvent(InputEventPtr eventPtr, InputDevice device)
    {
        if (device != Keyboard.current)
            return;

        if (!eventPtr.IsA<StateEvent>())
            return;

        int controlCount = device.allControls.Count;
        var controls = device.allControls;

        for (int i = 0; i < controlCount; i++)
        {
            var control = controls[i];

            var statePtr = control.GetStatePtrFromStateEvent(eventPtr);
            if (statePtr == null)
                continue;

            if (control is KeyControl)
            {
                KeyControl keyControl = (KeyControl)control;
                if (keyControl.keyCode.IsModifierKey())
                    continue;

                if ((float)control.ReadValueFromStateAsObject(statePtr) > 0)
                {
                    Array values = Enum.GetValues(typeof(Key));

                    listeningProp.enumValueIndex = Array.IndexOf(values, keyControl.keyCode);
                    listeningProp.serializedObject.ApplyModifiedProperties();
                    listeningProp = null;
                    StopListening();

                    eventPtr.handled = true;
                    break;
                }
            }
        }
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginDisabledGroup(listeningProp != null);
        SerializedProperty original = property.FindPropertyRelative("original");
        SerializedProperty remapped = property.FindPropertyRelative("remapped");
        Rect originalRect = position;
        originalRect.width /= 2;

        Rect remappedRect = originalRect;
        remappedRect.x += originalRect.width;
        remappedRect.width -= 22;
        originalRect.width -= 22;
        Rect originalListenRect = new Rect(originalRect.xMax, originalRect.y, 22, originalRect.height);
        Rect remappedListenRect = new Rect(remappedRect.xMax, remappedRect.y, 22, remappedRect.height);
        Rect symbolRect = new Rect(originalListenRect.xMax + 15, originalRect.y, 40, originalRect.height);



        EditorGUI.BeginChangeCheck();

        Array values = Enum.GetValues(typeof(Key));
        original.enumValueIndex = Array.IndexOf(values, EditorGUI.EnumPopup(originalRect, (Key)values.GetValue(original.enumValueIndex)));
        remapped.enumValueIndex = Array.IndexOf(values, EditorGUI.EnumPopup(remappedRect, (Key)values.GetValue(remapped.enumValueIndex)));

        if (EditorGUI.EndChangeCheck())
            property.serializedObject.ApplyModifiedProperties();


        if (GUI.Button(originalListenRect, "...", EditorStyles.miniButton))
            ListenForKey(original);
        if (GUI.Button(remappedListenRect, "...", EditorStyles.miniButton))
            ListenForKey(remapped);

        GUI.Label(symbolRect, "→");
        EditorGUI.EndDisabledGroup();

    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorGUIUtility.singleLineHeight;
    }
}
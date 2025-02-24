using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace BogGames.Tools
{
    /// <summary>
    /// Editor class for the generic list property attribute
    /// Essentially filter the types that are passed through and creates various property drawers for each type
    /// In this case as we are typically using multiple types we will use a popup to select the type
    /// Add a case to switch below to handle specific types and filter specficic types using a high level type management system
    /// This system is mostly based on objects derived from Unity (mono or scriptables)
    /// To use this system using custom assemblies, you should create a similar script, comment the "CustomPropertyDrawer" aspect out and add this to your custom script.
    /// If you are intending to use generic built-in types then this is not required.
    /// </summary>
#if UNITY_EDITOR
    //[CustomPropertyDrawer(typeof(GenericListPropertyAttribute))]
    public class GenericListPropertyEditor : PropertyDrawer
    {
        public static void GenericField(SerializedProperty property, GUIContent label, string defaultText,
                                    Func<Type, bool> filter, Func<string, int, string[], int> drawer = null)
        {
            List<string> options = new List<string> { defaultText };
            List<object> values = new List<object> { null };

            int index = 0;
            int selectedIndex = 0;

            object currentValue = property.objectReferenceValue;

            // Find which type we're dealing with
            Type targetType = null;
            if (filter(typeof(System.String))) targetType = typeof(System.String);
            //else if (filter(typeof(YourOtherType))) targetType = typeof(YourOtherType);
            // Add more type checks as needed

            // Handle the type
            switch (targetType)
            {
                //case Type t when t == typeof(YourOtherType):
                //    var otherList = GetOtherTypeList();
                //    break;

                default:
                    Debug.LogWarning("Unsupported type");
                    break;
            }

            if (drawer == null)
            {
                selectedIndex = EditorGUILayout.Popup(label.text, selectedIndex, options.ToArray());
            }
            else
            {
                selectedIndex = drawer(label.text, selectedIndex, options.ToArray());
            }

            property.objectReferenceValue = values[selectedIndex] as UnityEngine.Object;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            GenericListPropertyAttribute listProperty = attribute as GenericListPropertyAttribute;

            if (listProperty == null)
            {
                return;
            }

            EditorGUI.BeginProperty(position, label, property);

            // Filter the types found in the attribute and pass this property to a method that handles specific types
            // Ideally you will define specific types using your type manangement system but for now we allow any types in the given project
            Func<Type, bool> compare = t =>
            {
                if (t == null)
                {
                    return false;
                }
                if (listProperty.ListedTypes == null || listProperty.ListedTypes.Length == 0)
                {
                    return true;
                }

                return listProperty.ListedTypes.Contains<Type>(t);
            };

            // Call the method to draw the field
            GenericListPropertyEditor.GenericField(property, label, listProperty.defaultText, compare, (s, t, u) => EditorGUI.Popup(position, s, t, u));

            EditorGUI.EndProperty();
        }
    }
#endif
}
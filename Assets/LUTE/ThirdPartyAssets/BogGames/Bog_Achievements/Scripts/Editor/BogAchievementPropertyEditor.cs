using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace BogGames.Tools.Achievements
{
    /// <summary>
    /// Editor class for the achievement property
    /// Essentially filter the types that are passed through and creates various property drawers for each type
    /// In this case as we are typically using multiple types we will use a popup to select the types found within our achievement system
    /// Add a case to switch below to handle specific types and filter specficic types
    /// This system is mostly based on objects derived from Unity (mono or scriptables)
    /// </summary>
    [CustomPropertyDrawer(typeof(GenericListPropertyAttribute))]
    public class BogAchievementPropertyEditor : PropertyDrawer
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
            if (filter(typeof(BogAchievement))) targetType = typeof(BogAchievement);
            //else if (filter(typeof(YourOtherType))) targetType = typeof(YourOtherType);
            // Add more type checks as needed

            // Handle the type
            switch (targetType)
            {
                case Type t when t == typeof(BogAchievement):
                    BogAchievementRules achievementRules = GameObject.FindFirstObjectByType<BogAchievementRules>();
                    if (achievementRules == null)
                    {
                        Debug.LogError("No achievement rules found in the scene");
                        break;
                    }

                    foreach (BogAchievement achievement in achievementRules.AchievementList.Achievements)
                    {
                        if (achievement == null)
                        {
                            continue;
                        }

                        options.Add(achievement.AchievementID);
                        values.Add(achievement);

                        index++;

                        if (currentValue != null)
                        {
                            if (Equals(achievement, currentValue))
                            {
                                selectedIndex = index;
                            }
                        }
                    }
                    break;

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
            BogAchievementPropertyEditor.GenericField(property, label, listProperty.defaultText, compare, (s, t, u) => EditorGUI.Popup(position, s, t, u));

            EditorGUI.EndProperty();
        }
    }
}

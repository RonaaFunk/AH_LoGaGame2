using UnityEditor;

namespace LoGaCulture.LUTE
{
    [CustomEditor(typeof(InvokeEvent))]
    public class InvokeEventEditor : OrderEditor
    {
        protected SerializedProperty descriptionProp;
        protected SerializedProperty delayProp;
        protected SerializedProperty invokeTypeProp;
        protected SerializedProperty staticEventProp;
        protected SerializedProperty booleanParameterProp;
        protected SerializedProperty booleanEventProp;
        protected SerializedProperty integerParameterProp;
        protected SerializedProperty integerEventProp;
        protected SerializedProperty floatParameterProp;
        protected SerializedProperty floatEventProp;
        protected SerializedProperty stringParameterProp;
        protected SerializedProperty stringEventProp;

        public override void OnEnable()
        {
            base.OnEnable();

            descriptionProp = serializedObject.FindProperty("description");
            delayProp = serializedObject.FindProperty("delay");
            invokeTypeProp = serializedObject.FindProperty("invokeType");
            staticEventProp = serializedObject.FindProperty("staticEvent");
            booleanParameterProp = serializedObject.FindProperty("booleanParameter");
            booleanEventProp = serializedObject.FindProperty("booleanEvent");
            integerParameterProp = serializedObject.FindProperty("integerParameter");
            integerEventProp = serializedObject.FindProperty("integerEvent");
            floatParameterProp = serializedObject.FindProperty("floatParameter");
            floatEventProp = serializedObject.FindProperty("floatEvent");
            stringParameterProp = serializedObject.FindProperty("stringParameter");
            stringEventProp = serializedObject.FindProperty("stringEvent");
        }

        public override void DrawOrderGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(descriptionProp);
            EditorGUILayout.PropertyField(delayProp);
            EditorGUILayout.PropertyField(invokeTypeProp);

            switch ((InvokeType)invokeTypeProp.enumValueIndex)
            {
                case InvokeType.Static:
                    EditorGUILayout.PropertyField(staticEventProp);
                    break;
                case InvokeType.DynamicBoolean:
                    EditorGUILayout.PropertyField(booleanEventProp);
                    EditorGUILayout.PropertyField(booleanParameterProp);
                    break;
                case InvokeType.DynamicInteger:
                    EditorGUILayout.PropertyField(integerEventProp);
                    EditorGUILayout.PropertyField(integerParameterProp);
                    break;
                case InvokeType.DynamicFloat:
                    EditorGUILayout.PropertyField(floatEventProp);
                    EditorGUILayout.PropertyField(floatParameterProp);
                    break;
                case InvokeType.DynamicString:
                    EditorGUILayout.PropertyField(stringEventProp);
                    EditorGUILayout.PropertyField(stringParameterProp);
                    break;
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}

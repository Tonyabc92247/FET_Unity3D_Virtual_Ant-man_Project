using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

[CustomEditor(typeof(FurRenderer))]
sealed class FurRendererEditor : Editor
{
    ReorderableList _renderers;
    SerializedProperty _submeshIndex;

    //Grooming Inputs
    SerializedProperty _furDistanceField;
    SerializedProperty _furDensity;
    SerializedProperty _furSpacing;
    SerializedProperty _furAlphaRemapMin;
    SerializedProperty _furAlphaRemapMax;
    SerializedProperty _furCombMap;
    SerializedProperty _furCombStrength;
    SerializedProperty _furHeightMap;
    SerializedProperty _furHeightRemapMin;
    SerializedProperty _furHeightRemapMax;

    //Shading Inputs
    SerializedProperty _furSpecularShift;
    SerializedProperty _furSpecularColor;
    SerializedProperty _furSecondarySmoothness;
    SerializedProperty _furSecondarySpecularShift;
    SerializedProperty _furSecondarySpecularColor;
    SerializedProperty _furScatter;
    SerializedProperty _furScatterColor;
    SerializedProperty _furSelfOcclusionTerm;
    SerializedProperty _furAORemapMin;
    SerializedProperty _furAORemapMax;

    //Overcoat
    SerializedProperty _furOvercoatDistanceField;
    SerializedProperty _furOvercoatAlbedoMap;
    SerializedProperty _furOvercoatColor;
    SerializedProperty _furOvercoatDensity;
    SerializedProperty _furOvercoatHairSpacing;
    SerializedProperty _furOvercoatOffsetAmount;
    SerializedProperty _furOvercoatCombStrength;
    SerializedProperty _furOvercoatHeightMap;
    SerializedProperty _furOvercoatHeight;
    SerializedProperty _furOvercoatMinimumHeight;
    SerializedProperty _furOvercoatAlphaRemap;
    SerializedProperty _furOvercoatSilhouette;
    SerializedProperty _furOvercoatAORemapMin;
    SerializedProperty _furOvercoatAORemapMax;
    SerializedProperty _furOvercoatSelfOcclusionTerm;
    SerializedProperty _furOvercoatScatter;
    SerializedProperty _furOvercoatScatterColor;
    SerializedProperty _furOvercoatRootColor;
    SerializedProperty _furOvercoatTipColor;

    //Basic Overrides
    SerializedProperty _furBaseColorMap;
    SerializedProperty _furBaseColor;
    SerializedProperty _furMinNormalBlend;
    SerializedProperty _furMaxNormalBlend;
    SerializedProperty _furMinNormalBlendStrength;
    SerializedProperty _furMaxNormalBlendStrength;

    SerializedProperty _furUndercoatRootColor;
    SerializedProperty _furUndercoatTipColor;
    SerializedProperty _furCombStrengthMap;
    SerializedProperty _furUndercoatScatterMap;
    SerializedProperty _furUndercoatSpecularMap;
    SerializedProperty _furUndercoatSecondarySpecularMap;

    void OnEnable()
    {
        _furDistanceField = serializedObject.FindProperty("_furDistanceField");
        _furDensity = serializedObject.FindProperty("_furDensity");
        _furSpacing = serializedObject.FindProperty("_furSpacing");
        _furAlphaRemapMin = serializedObject.FindProperty("_furAlphaRemapMin");
        _furAlphaRemapMax = serializedObject.FindProperty("_furAlphaRemapMax");
        _furCombMap = serializedObject.FindProperty("_furCombMap");
        _furCombStrength = serializedObject.FindProperty("_furCombStrength");
        _furHeightMap = serializedObject.FindProperty("_furHeightMap");
        _furHeightRemapMin = serializedObject.FindProperty("_furHeightRemapMin");
        _furHeightRemapMax = serializedObject.FindProperty("_furHeightRemapMax");

        _furSpecularShift = serializedObject.FindProperty("_furSpecularShift");
        _furSpecularColor = serializedObject.FindProperty("_furSpecularColor");
        _furSecondarySmoothness = serializedObject.FindProperty("_furSecondarySmoothness");
        _furSecondarySpecularShift = serializedObject.FindProperty("_furSecondarySpecularShift");
        _furSecondarySpecularColor = serializedObject.FindProperty("_furSecondarySpecularColor");
        _furScatter = serializedObject.FindProperty("_furScatter");
        _furScatterColor = serializedObject.FindProperty("_furScatterColor");
        _furSelfOcclusionTerm = serializedObject.FindProperty("_furSelfOcclusionTerm");
        _furAORemapMin = serializedObject.FindProperty("_furAORemapMin");
        _furAORemapMax = serializedObject.FindProperty("_furAORemapMax");

        _furOvercoatDistanceField = serializedObject.FindProperty("_furOvercoatDistanceField");
        _furOvercoatAlbedoMap = serializedObject.FindProperty("_furOvercoatAlbedoMap");
        _furOvercoatColor = serializedObject.FindProperty("_furOvercoatColor");
        _furOvercoatDensity = serializedObject.FindProperty("_furOvercoatDensity");
        _furOvercoatHairSpacing = serializedObject.FindProperty("_furOvercoatHairSpacing");
        _furOvercoatOffsetAmount = serializedObject.FindProperty("_furOvercoatOffsetAmount");
        _furOvercoatCombStrength = serializedObject.FindProperty("_furOvercoatCombStrength");
        _furOvercoatHeightMap = serializedObject.FindProperty("_furOvercoatHeightMap");
        _furOvercoatHeight = serializedObject.FindProperty("_furOvercoatHeight");
        _furOvercoatMinimumHeight = serializedObject.FindProperty("_furOvercoatMinimumHeight");
        _furOvercoatAlphaRemap = serializedObject.FindProperty("_furOvercoatAlphaRemap");
        _furOvercoatSilhouette = serializedObject.FindProperty("_furOvercoatSilhouette");
        _furOvercoatAORemapMin = serializedObject.FindProperty("_furOvercoatAORemapMin");
        _furOvercoatAORemapMax = serializedObject.FindProperty("_furOvercoatAORemapMax");
        _furOvercoatSelfOcclusionTerm = serializedObject.FindProperty("_furOvercoatSelfOcclusionTerm");
        _furOvercoatScatter = serializedObject.FindProperty("_furOvercoatScatter");
        _furOvercoatScatterColor = serializedObject.FindProperty("_furOvercoatScatterColor");
        _furOvercoatRootColor = serializedObject.FindProperty("_furOvercoatRootColor");
        _furOvercoatTipColor = serializedObject.FindProperty("_furOvercoatTipColor");

        _furBaseColorMap = serializedObject.FindProperty("_furBaseColorMap");
        _furBaseColor = serializedObject.FindProperty("_furBaseColor");

        _furMinNormalBlend = serializedObject.FindProperty("_furMinNormalBlend");
        _furMaxNormalBlend = serializedObject.FindProperty("_furMaxNormalBlend");
        _furMinNormalBlendStrength = serializedObject.FindProperty("_furMinNormalBlendStrength");
        _furMaxNormalBlendStrength = serializedObject.FindProperty("_furMaxNormalBlendStrength");

        _furUndercoatRootColor = serializedObject.FindProperty("_furUndercoatRootColor");
        _furUndercoatTipColor = serializedObject.FindProperty("_furUndercoatTipColor");
        _furCombStrengthMap = serializedObject.FindProperty("_furCombStrengthMap");
        _furUndercoatScatterMap = serializedObject.FindProperty("_furUndercoatScatterMap");
        _furUndercoatSpecularMap = serializedObject.FindProperty("_furUndercoatSpecularMap");
        _furUndercoatSecondarySpecularMap = serializedObject.FindProperty("_furUndercoatSecondarySpecularMap");

        _submeshIndex = serializedObject.FindProperty("_submeshIndex");

        _renderers = new ReorderableList(
            serializedObject,
            serializedObject.FindProperty("_renderers"),
            true, // draggable
            true, // displayHeader
            true, // displayAddButton
            true  // displayRemoveButton
        );

        _renderers.drawHeaderCallback = (Rect rect) => {
            EditorGUI.LabelField(rect, "Target Renderers");
        };

        _renderers.drawElementCallback = (Rect frame, int index, bool isActive, bool isFocused) => {
            var rect = frame;
            rect.y += 2;
            rect.height = EditorGUIUtility.singleLineHeight;
            var element = _renderers.serializedProperty.GetArrayElementAtIndex(index);
            EditorGUI.PropertyField(rect, element, GUIContent.none);
        };
    }

    static class Styles
    {
        public static readonly GUIContent AlphaRemap = new GUIContent("Alpha Remap");
        public static readonly GUIContent HeightRemap = new GUIContent("Height Remap");
        public static readonly GUIContent SelfOcclusionMultiplier = new GUIContent("Self Occlusion Muliplier");
        public static readonly GUIContent AORemap = new GUIContent("Self Occlusion");
        public static readonly GUIContent MaterialIndex = new GUIContent("Submesh Index");
        public static readonly GUIContent NormalBlend = new GUIContent("Normal Blend Range");
        public static readonly GUIContent NormalBlendStrength = new GUIContent("Normal Blend Strength");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.LabelField("Fur Material Properties");
        EditorGUI.indentLevel++;

        EditorGUILayout.LabelField("Grooming Inputs", EditorStyles.miniBoldLabel);

        GUI.color = Color.green;
        EditorGUILayout.PropertyField(_furDistanceField);
        GUI.color = Color.white;

        EditorGUILayout.Space();

        EditorGUILayout.PropertyField(_furDensity);
        EditorGUILayout.PropertyField(_furSpacing);


        Vector2 remap;
        //EditorGUI.BeginChangeCheck();
        //Vector2 remap = new Vector2(_furAlphaRemapMin.floatValue, _furAlphaRemapMax.floatValue);
        //EditorGUILayout.MinMaxSlider(Styles.AlphaRemap, ref remap.x, ref remap.y, 0.0f, 1.0f);
        //if (EditorGUI.EndChangeCheck())
        //{
        //    _furAlphaRemapMin.floatValue = remap.x;
        //    _furAlphaRemapMax.floatValue = remap.y;
        //}

        EditorGUILayout.PropertyField(_furAlphaRemapMin);
        //EditorGUILayout.PropertyField(_furAlphaRemapMax);

        EditorGUILayout.Space();

        EditorGUILayout.PropertyField(_furCombMap);
        EditorGUILayout.PropertyField(_furCombStrengthMap);
        EditorGUILayout.PropertyField(_furCombStrength);

        EditorGUILayout.Space();

        EditorGUILayout.PropertyField(_furHeightMap);

        EditorGUI.BeginChangeCheck();
        remap = new Vector2(_furHeightRemapMin.floatValue, _furHeightRemapMax.floatValue);
        EditorGUILayout.MinMaxSlider(Styles.HeightRemap, ref remap.x, ref remap.y, 0.0f, 1.0f);
        if (EditorGUI.EndChangeCheck())
        {
            _furHeightRemapMin.floatValue = remap.x;
            _furHeightRemapMax.floatValue = remap.y;
        }

        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Shading Inputs", EditorStyles.miniBoldLabel);

        EditorGUILayout.PropertyField(_furSpecularShift);
        EditorGUILayout.PropertyField(_furUndercoatSpecularMap);
        EditorGUILayout.PropertyField(_furSpecularColor);

        EditorGUILayout.Space();

        EditorGUILayout.PropertyField(_furSecondarySmoothness);
        EditorGUILayout.PropertyField(_furSecondarySpecularShift);
        EditorGUILayout.PropertyField(_furUndercoatSecondarySpecularMap);
        EditorGUILayout.PropertyField(_furSecondarySpecularColor);

        EditorGUILayout.Space();

        EditorGUILayout.PropertyField(_furScatter);
        EditorGUILayout.PropertyField(_furUndercoatScatterMap);
        EditorGUILayout.PropertyField(_furScatterColor);

        EditorGUILayout.Space();

        EditorGUI.BeginChangeCheck();
        remap = new Vector2(_furAORemapMin.floatValue, _furAORemapMax.floatValue);
        EditorGUILayout.MinMaxSlider(Styles.AORemap, ref remap.x, ref remap.y, 0.0f, 1.0f);
        if (EditorGUI.EndChangeCheck())
        {
            _furAORemapMin.floatValue = remap.x;
            _furAORemapMax.floatValue = remap.y;
        }
        EditorGUILayout.PropertyField(_furSelfOcclusionTerm, Styles.SelfOcclusionMultiplier);

        EditorGUILayout.Space();

        EditorGUILayout.PropertyField(_furUndercoatRootColor);
        EditorGUILayout.PropertyField(_furUndercoatTipColor);

        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Overcoat", EditorStyles.miniBoldLabel);

        GUI.color = Color.green;
        EditorGUILayout.PropertyField(_furOvercoatDistanceField);
        GUI.color = Color.white;

        EditorGUILayout.Space();

        EditorGUILayout.PropertyField(_furOvercoatAlbedoMap);
        EditorGUILayout.PropertyField(_furOvercoatColor);

        EditorGUILayout.Space();

        EditorGUILayout.PropertyField(_furOvercoatOffsetAmount);
        EditorGUILayout.PropertyField(_furOvercoatDensity);
        EditorGUILayout.PropertyField(_furOvercoatHairSpacing);
        EditorGUILayout.PropertyField(_furOvercoatCombStrength);

        EditorGUILayout.Space();

        EditorGUILayout.PropertyField(_furOvercoatHeightMap);
        EditorGUILayout.PropertyField(_furOvercoatMinimumHeight);
        EditorGUILayout.PropertyField(_furOvercoatHeight);

        EditorGUI.BeginChangeCheck();
        remap = _furOvercoatAlphaRemap.vector2Value;
        EditorGUILayout.MinMaxSlider(Styles.AlphaRemap, ref remap.x, ref remap.y, 0.0f, 1.0f);
        if (EditorGUI.EndChangeCheck())
        {
            _furOvercoatAlphaRemap.vector2Value = remap;
        }

        EditorGUILayout.Space();

        EditorGUI.BeginChangeCheck();
        remap = new Vector2(_furOvercoatAORemapMin.floatValue, _furOvercoatAORemapMax.floatValue);
        EditorGUILayout.MinMaxSlider(Styles.AORemap, ref remap.x, ref remap.y, 0.0f, 1.0f);
        if (EditorGUI.EndChangeCheck())
        {
            _furOvercoatAORemapMin.floatValue = remap.x;
            _furOvercoatAORemapMax.floatValue = remap.y;
        }
        EditorGUILayout.PropertyField(_furOvercoatSelfOcclusionTerm);

        EditorGUILayout.Space();

        EditorGUILayout.PropertyField(_furOvercoatScatter);
        EditorGUILayout.PropertyField(_furOvercoatScatterColor);

        EditorGUILayout.Space();

        EditorGUILayout.PropertyField(_furOvercoatRootColor);
        EditorGUILayout.PropertyField(_furOvercoatTipColor);

        EditorGUILayout.Space();

        EditorGUILayout.PropertyField(_furOvercoatSilhouette);


        EditorGUILayout.Space();
        EditorGUILayout.LabelField("General", EditorStyles.miniBoldLabel);
        EditorGUILayout.PropertyField(_furBaseColorMap);
        EditorGUILayout.PropertyField(_furBaseColor);

        EditorGUI.BeginChangeCheck();
        remap = new Vector2(_furMinNormalBlend.floatValue, _furMaxNormalBlend.floatValue);
        EditorGUILayout.MinMaxSlider(Styles.NormalBlend, ref remap.x, ref remap.y, 0.0f, 1.0f);
        if (EditorGUI.EndChangeCheck())
        {
            _furMinNormalBlend.floatValue = remap.x;
            _furMaxNormalBlend.floatValue = remap.y;
        }

        EditorGUI.BeginChangeCheck();
        remap = new Vector2(_furMinNormalBlendStrength.floatValue, _furMaxNormalBlendStrength.floatValue);
        EditorGUILayout.MinMaxSlider(Styles.NormalBlendStrength, ref remap.x, ref remap.y, 0.0f, 1.0f);
        if (EditorGUI.EndChangeCheck())
        {
            _furMinNormalBlendStrength.floatValue = remap.x;
            _furMaxNormalBlendStrength.floatValue = remap.y;
        }

        EditorGUILayout.Space();

        EditorGUI.indentLevel--;

        _renderers.DoLayoutList();

        //If there is more than one submesh, expose option to choose different index.
        if (_renderers.count > 0)
        {
            MeshRenderer test = _renderers.serializedProperty.GetArrayElementAtIndex(0).objectReferenceValue as MeshRenderer;

            if (test != null && test.sharedMaterials.Length > 1)
            {
                GUI.color = Color.red;
                EditorGUILayout.LabelField("Multiple Submeshes Detected:", EditorStyles.miniBoldLabel);
                GUI.color = Color.white;

                EditorGUILayout.PropertyField(_submeshIndex, Styles.MaterialIndex);
            }
        }

        serializedObject.ApplyModifiedProperties();
    }
}
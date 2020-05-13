using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using System.Collections.Generic;

[ExecuteInEditMode]
public sealed class FurRenderer : MonoBehaviour
{
    [SerializeField] Renderer[] _renderers = null;
    [SerializeField] int _submeshIndex = 0;

    //Grooming Inputs
    [SerializeField] Texture3D _furDistanceField = null;
    [SerializeField] [Range(0f, 1000f)] float _furDensity = 1f;
    [SerializeField] [Range(0f, 2f)] float _furSpacing = 0.5f;
    [SerializeField] [Range(0f, 1f)] float _furAlphaRemapMin = 0f;
    [SerializeField] float _furAlphaRemapMax = 1f;
    [SerializeField] Texture2D _furCombMap = null;
    [SerializeField] [Range(-3f, 3f)] float _furCombStrength = 0f;
    [SerializeField] Texture2D _furHeightMap = null;
    [SerializeField] float _furHeightRemapMin = 0f;
    [SerializeField] float _furHeightRemapMax = 1f;

    //Shading Inputs
    [SerializeField] [Range(-1f, 1f)] float _furSpecularShift = 0f;
    [SerializeField] Color _furSpecularColor = Color.white;
    [SerializeField] [Range(0f, 1f)] float _furSecondarySmoothness = 0.5f;
    [SerializeField] [Range(-1f, 1f)] float _furSecondarySpecularShift = 0f;
    [SerializeField] Color _furSecondarySpecularColor = Color.white;
    [SerializeField] [Range(0f, 4f)] float _furScatter = 1f;
    [SerializeField] Color _furScatterColor = Color.white;
    [SerializeField] [Range(0f, 2f)] float _furSelfOcclusionTerm = 1f;
    [SerializeField] float _furAORemapMin = 0f;
    [SerializeField] float _furAORemapMax = 1f;

    //Overcoat
    [SerializeField] Texture3D _furOvercoatDistanceField = null;
    [SerializeField] Texture2D _furOvercoatAlbedoMap = null;
    [SerializeField] Color _furOvercoatColor = Color.white;
    [SerializeField] [Range(0f, 1000f)] float _furOvercoatDensity = 1f;
    [SerializeField] [Range(0f, 2f)] float _furOvercoatHairSpacing = 0.5f;
    [SerializeField] [Range(0f, 3f)] float _furOvercoatOffsetAmount = 0.5f;
    [SerializeField] [Range(-3f, 3f)] float _furOvercoatCombStrength = 0f;
    [SerializeField] Texture2D _furOvercoatHeightMap = null;
    [SerializeField] float _furOvercoatHeight = 0f;
    [SerializeField] float _furOvercoatMinimumHeight = 1f;
    [SerializeField] Vector2 _furOvercoatAlphaRemap = Vector2.zero;
    [SerializeField] [Range(0f, 2f)] float _furOvercoatSilhouette = 0f;
    [SerializeField] [Range(0f, 2f)] float _furOvercoatSelfOcclusionTerm = 1f;
    [SerializeField] float _furOvercoatAORemapMin = 0f;
    [SerializeField] float _furOvercoatAORemapMax = 1f;
    [SerializeField] [Range(0f, 4f)] float _furOvercoatScatter = 1f;
    [SerializeField] Color _furOvercoatScatterColor = Color.white;
    [SerializeField] Color _furOvercoatRootColor = Color.white;
    [SerializeField] Color _furOvercoatTipColor = Color.white;

    //Basic Overrides
    [SerializeField] Texture2D _furBaseColorMap = null;
    [SerializeField] Color _furBaseColor = Color.white;
    [SerializeField] float _furMinNormalBlend = 0f;
    [SerializeField] float _furMaxNormalBlend = 0f;
    [SerializeField] float _furMinNormalBlendStrength = 0f;
    [SerializeField] float _furMaxNormalBlendStrength = 1f;

    [SerializeField] Color _furUndercoatRootColor = Color.white;
    [SerializeField] Color _furUndercoatTipColor = Color.white;
    [SerializeField] Texture2D _furCombStrengthMap = null;
    [SerializeField] Texture2D _furUndercoatScatterMap = null;
    [SerializeField] Texture2D _furUndercoatSpecularMap = null;
    [SerializeField] Texture2D _furUndercoatSecondarySpecularMap = null;
 
    static class ShaderIDs
    {
        public static readonly int _FurGeometrySDF = Shader.PropertyToID("_FurGeometrySDF");
        public static readonly int _FurDensity = Shader.PropertyToID("_FurDensity");
        public static readonly int _FurSpacing = Shader.PropertyToID("_FurSpacing");
        public static readonly int _FurAlphaRemapMin = Shader.PropertyToID("_FurAlphaFalloff");
        public static readonly int _FurAlphaRemapMax = Shader.PropertyToID("_FurAlphaFalloffBias");
        public static readonly int _FurCombMap = Shader.PropertyToID("_FurGroomMap");
        public static readonly int _FurCombStrength = Shader.PropertyToID("_FurCombStrength");
        public static readonly int _FurHeightMap = Shader.PropertyToID("_FurHeightMap");
        public static readonly int _FurHeightRemapMin = Shader.PropertyToID("_FurShellMinimumHeight");
        public static readonly int _FurHeightRemapMax = Shader.PropertyToID("_FurShellHeight");

        public static readonly int _FurSpecularShift = Shader.PropertyToID("_SpecularShift");
        public static readonly int _FurSpecularColor = Shader.PropertyToID("_SpecularTint");
        public static readonly int _FurSecondarySmoothness = Shader.PropertyToID("_SecondaryPerceptualSmoothness");
        public static readonly int _FurSecondarySpecularShift = Shader.PropertyToID("_SecondarySpecularShift");
        public static readonly int _FurSecondarySpecularColor = Shader.PropertyToID("_SecondarySpecularTint");
        public static readonly int _FurScatter = Shader.PropertyToID("_FurScatter");
        public static readonly int _FurScatterTint = Shader.PropertyToID("_ScatterTint");
        public static readonly int _FurSelfOcclusionTerm = Shader.PropertyToID("_FurSelfOcclusionMultiplier");
        public static readonly int _FurAORemapMin = Shader.PropertyToID("_AORemapMin");
        public static readonly int _FurAORemapMax = Shader.PropertyToID("_AORemapMax");

        public static readonly int _FurOvercoatDistanceField = Shader.PropertyToID("_FurOvercoatDistanceField");
        public static readonly int _FurOvercoatAlbedoMap = Shader.PropertyToID("_FurOvercoatAlbedoMap");
        public static readonly int _FurOvercoatColor = Shader.PropertyToID("_FurOvercoatColor");
        public static readonly int _FurOvercoatDensity = Shader.PropertyToID("_FurOvercoatDensity");
        public static readonly int _FurOvercoatSpacing = Shader.PropertyToID("_FurOvercoatSpacing");
        public static readonly int _FurOvercoatOffsetAmount = Shader.PropertyToID("_FurOvercoatOffsetAmount");
        public static readonly int _FurOvercoatCombStrength = Shader.PropertyToID("_FurOvercoatCombStrength");
        public static readonly int _FurOvercoatHeightMap = Shader.PropertyToID("_FurOvercoatHeightMap");
        public static readonly int _FurOvercoatHeight = Shader.PropertyToID("_FurOvercoatHeight");
        public static readonly int _FurOvercoatMinimumHeight = Shader.PropertyToID("_FurOvercoatMinimumHeight");
        public static readonly int _FurOvercoatAlphaRemap = Shader.PropertyToID("_FurOvercoatAlphaRemap");
        public static readonly int _FurOvercoatSilhouette = Shader.PropertyToID("_FurOvercoatSilhouette");
        public static readonly int _FurOvercoatSelfOcclusionTerm = Shader.PropertyToID("_FurOvercoatSelfOcclusionMultiplier");
        public static readonly int _FurOvercoatAORemapMin = Shader.PropertyToID("_FurAOOvercoatRemapMin");
        public static readonly int _FurOvercoatAORemapMax = Shader.PropertyToID("_FurAOOvercoatRemapMax");
        public static readonly int _FurOvercoatScatter = Shader.PropertyToID("_FurOvercoatScatter");
        public static readonly int _FurOvercoatScatterColor = Shader.PropertyToID("_FurOvercoatScatterColor");
        public static readonly int _FurOvercoatRootColor = Shader.PropertyToID("_FurOvercoatRootColor");
        public static readonly int _FurOvercoatTipColor = Shader.PropertyToID("_FurOvercoatTipColor");

        public static readonly int _FurBaseColorMap = Shader.PropertyToID("_BaseColorMap");
        public static readonly int _FurBaseColor = Shader.PropertyToID("_BaseColor");

        //TODO: Consolidate to vectors
        public static readonly int _FurUndercoatGroomingParams = Shader.PropertyToID("_FurUndercoatGroomingParams");
        public static readonly int _FurOvercoatGroomingParams = Shader.PropertyToID("_FurOvercoatGroomingParams");
        public static readonly int _FurMinNormalBlend = Shader.PropertyToID("_FurMinNormalBlend");
        public static readonly int _FurMaxNormalBlend = Shader.PropertyToID("_FurMaxNormalBlend");
        public static readonly int _FurMinNormalBlendStrength = Shader.PropertyToID("_FurMinNormalBlendStrength");
        public static readonly int _FurMaxNormalBlendStrength = Shader.PropertyToID("_FurMaxNormalBlendStrength");

        public static readonly int _FurUndercoatRootColor = Shader.PropertyToID("_FurUndercoatRootColor");
        public static readonly int _FurUndercoatTipColor = Shader.PropertyToID("_FurUndercoatTipColor");
        public static readonly int _FurCombStrengthMap = Shader.PropertyToID("_FurCombStrengthMap");
        public static readonly int _FurUndercoatScatterColor = Shader.PropertyToID("_FurUndercoatScatterColor");
        public static readonly int _FurUndercoatSpecularColor = Shader.PropertyToID("_FurUndercoatSpecularColor");
        public static readonly int _FurUndercoatSecondarySpecularColor = Shader.PropertyToID("_FurUndercoatSecondarySpecularColor");
  
    }
    

    MaterialPropertyBlock _sheet;

    private void OnDisable()
    {
        foreach (var renderer in _renderers)
        {
            renderer.SetPropertyBlock(null, _submeshIndex);
        }
    }

    void LateUpdate()
    {
        if (_renderers == null || _renderers.Length == 0) return;

        if (_sheet == null) _sheet = new MaterialPropertyBlock();


        foreach (var renderer in _renderers)
        {
            if (renderer == null || renderer.sharedMaterials.Length == 0) continue;
            renderer.GetPropertyBlock(_sheet, _submeshIndex);

            if(_furDistanceField != null) _sheet.SetTexture(ShaderIDs._FurGeometrySDF, _furDistanceField);
            _sheet.SetFloat(ShaderIDs._FurDensity, _furDensity);
            _sheet.SetFloat(ShaderIDs._FurSpacing, _furSpacing);
            _sheet.SetFloat(ShaderIDs._FurAlphaRemapMin, _furAlphaRemapMin);
            _sheet.SetFloat(ShaderIDs._FurAlphaRemapMax, _furAlphaRemapMax);
            if(_furCombMap != null) _sheet.SetTexture(ShaderIDs._FurCombMap, _furCombMap);
            _sheet.SetFloat(ShaderIDs._FurCombStrength, _furCombStrength);
            if(_furHeightMap != null) _sheet.SetTexture(ShaderIDs._FurHeightMap, _furHeightMap);
            _sheet.SetFloat(ShaderIDs._FurHeightRemapMin, _furHeightRemapMin);
            _sheet.SetFloat(ShaderIDs._FurHeightRemapMax, _furHeightRemapMax);

            _sheet.SetFloat(ShaderIDs._FurSpecularShift, _furSpecularShift);
            _sheet.SetColor(ShaderIDs._FurSpecularColor, _furSpecularColor);
            _sheet.SetFloat(ShaderIDs._FurSecondarySmoothness, _furSecondarySmoothness);
            _sheet.SetFloat(ShaderIDs._FurSecondarySpecularShift, _furSecondarySpecularShift);
            _sheet.SetColor(ShaderIDs._FurSecondarySpecularColor, _furSecondarySpecularColor);
            _sheet.SetFloat(ShaderIDs._FurScatter, _furScatter);
            _sheet.SetColor(ShaderIDs._FurScatterTint, _furScatterColor);
            _sheet.SetFloat(ShaderIDs._FurSelfOcclusionTerm, _furSelfOcclusionTerm);
            _sheet.SetFloat(ShaderIDs._FurAORemapMin, _furAORemapMin);
            _sheet.SetFloat(ShaderIDs._FurAORemapMax, _furAORemapMax);

            if(_furOvercoatDistanceField != null) _sheet.SetTexture(ShaderIDs._FurOvercoatDistanceField, _furOvercoatDistanceField);
            if(_furOvercoatAlbedoMap != null) _sheet.SetTexture(ShaderIDs._FurOvercoatAlbedoMap, _furOvercoatAlbedoMap);
            _sheet.SetColor(ShaderIDs._FurOvercoatColor, _furOvercoatColor);
            _sheet.SetFloat(ShaderIDs._FurOvercoatDensity, _furOvercoatDensity);
            _sheet.SetFloat(ShaderIDs._FurOvercoatSpacing, _furOvercoatHairSpacing);
            _sheet.SetFloat(ShaderIDs._FurOvercoatOffsetAmount, _furOvercoatOffsetAmount);
            _sheet.SetFloat(ShaderIDs._FurOvercoatCombStrength, _furOvercoatCombStrength);
            if (_furOvercoatHeightMap != null) _sheet.SetTexture(ShaderIDs._FurOvercoatHeightMap, _furOvercoatHeightMap);
            _sheet.SetFloat(ShaderIDs._FurOvercoatHeight, _furOvercoatHeight);
            _sheet.SetFloat(ShaderIDs._FurOvercoatMinimumHeight, _furOvercoatMinimumHeight);
            _sheet.SetVector(ShaderIDs._FurOvercoatAlphaRemap, _furOvercoatAlphaRemap);
            _sheet.SetFloat(ShaderIDs._FurOvercoatSilhouette, _furOvercoatSilhouette);
            //_sheet.SetFloat(ShaderIDs._FurOvercoatSelfOcclusionTerm, _furOvercoatSelfOcclusionTerm);
            //_sheet.SetFloat(ShaderIDs._FurOvercoatAORemapMin, _furOvercoatAORemapMin);
            //_sheet.SetFloat(ShaderIDs._FurOvercoatAORemapMax, _furOvercoatAORemapMax);
            _sheet.SetFloat(ShaderIDs._FurOvercoatScatter, _furOvercoatScatter);
            _sheet.SetColor(ShaderIDs._FurOvercoatScatterColor, _furOvercoatScatterColor);
            _sheet.SetColor(ShaderIDs._FurOvercoatRootColor, _furOvercoatRootColor);
            _sheet.SetColor(ShaderIDs._FurOvercoatTipColor, _furOvercoatTipColor);
            
            _sheet.SetVector(ShaderIDs._FurOvercoatGroomingParams, new Vector4(_furOvercoatDensity, _furOvercoatHairSpacing, _furOvercoatCombStrength, _furOvercoatHeight));

            if (_furBaseColorMap != null) _sheet.SetTexture(ShaderIDs._FurBaseColorMap, _furBaseColorMap);
            _sheet.SetColor(ShaderIDs._FurBaseColor, _furBaseColor);

            _sheet.SetFloat(ShaderIDs._FurMinNormalBlend, _furMinNormalBlend);
            _sheet.SetFloat(ShaderIDs._FurMaxNormalBlend, _furMaxNormalBlend);
            _sheet.SetFloat(ShaderIDs._FurMinNormalBlendStrength, _furMinNormalBlendStrength);
            _sheet.SetFloat(ShaderIDs._FurMaxNormalBlendStrength, _furMaxNormalBlendStrength);

            _sheet.SetColor(ShaderIDs._FurUndercoatRootColor, _furUndercoatRootColor);
            _sheet.SetColor(ShaderIDs._FurUndercoatTipColor, _furUndercoatTipColor);

            if (_furCombStrengthMap != null) _sheet.SetTexture(ShaderIDs._FurCombStrengthMap, _furCombStrengthMap);
            else _sheet.SetTexture(ShaderIDs._FurCombStrengthMap, Texture2D.whiteTexture);

            if(_furUndercoatScatterMap != null) _sheet.SetTexture(ShaderIDs._FurUndercoatScatterColor, _furUndercoatScatterMap);
            if(_furUndercoatSpecularMap != null) _sheet.SetTexture(ShaderIDs._FurUndercoatSpecularColor, _furUndercoatSpecularMap);
            if(_furUndercoatSecondarySpecularMap != null) _sheet.SetTexture(ShaderIDs._FurUndercoatSecondarySpecularColor, _furUndercoatSecondarySpecularMap);

            renderer.SetPropertyBlock(_sheet, _submeshIndex);
        }
    }
}
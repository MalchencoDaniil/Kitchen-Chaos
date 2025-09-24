using KitchenChaos.Player;
using UnityEngine;

namespace KitchenChaos.Player
{
    public class InteractVisual : MonoBehaviour
    {
        [Header("Highlight Settings")]
        [SerializeField] private HighlightSettings _normalHighlight;
        [SerializeField] private HighlightSettings _interactableHighlight;

        [SerializeField] private bool _enablePulseEffect = true;
        [SerializeField] private float _pulseSpeed = 2f;

        [Header("Performance")]
        [SerializeField] private bool _useMaterialInstancing = true;
        [SerializeField] private LayerMask _interactableLayers = -1;

        private Renderer[] _currentRenderers;
        private Material[] _originalMaterials, _highlightMaterials;

        private GameObject _currentInteractable;

        private float _pulseTimer;
        private bool _isHighlighted;

        private void Awake()
        {
            _currentRenderers = new Renderer[0];
            _originalMaterials = new Material[0];
            _highlightMaterials = new Material[0];
        }

        private void OnEnable()
        {
            Interact.OnInteractableFound += HandleInteractableFound;
            Interact.OnInteractableLost += HandleInteractableLost;
        }

        private void OnDisable()
        {
            Interact.OnInteractableFound -= HandleInteractableFound;
            Interact.OnInteractableLost -= HandleInteractableLost;
            ResetHighlight();
        }

        private void Update()
        {
            if (_isHighlighted && _enablePulseEffect)
            {
                UpdatePulseEffect();
            }
        }

        private void HandleInteractableFound(GameObject interactableObject)
        {
            if (interactableObject == null) 
                return;

            if (!IsInInteractableLayer(interactableObject)) 
                return;

            if (_currentInteractable == interactableObject) 
                return;

            ResetHighlight();
            ApplyHighlight(interactableObject, _normalHighlight);

            _currentInteractable = interactableObject;
            _isHighlighted = true;
        }

        private void HandleInteractableLost()
        {
            if (_currentInteractable != null)
            {
                ResetHighlight();

                _currentInteractable = null;
                _isHighlighted = false;
            }
        }

        private void ApplyHighlight(GameObject interactableObject, HighlightSettings settings)
        {
            var _renderers = interactableObject.GetComponentsInChildren<Renderer>();

            if (_renderers.Length == 0)
                return;

            _currentRenderers = _renderers;

            _originalMaterials = new Material[_renderers.Length];
            _highlightMaterials = new Material[_renderers.Length];

            for (int i = 0; i < _renderers.Length; i++)
            {
                var _renderer = _renderers[i];
                if (_renderer == null) continue;

                _originalMaterials[i] = _renderer.material;

                if (_useMaterialInstancing)
                {
                    _highlightMaterials[i] = new Material(_renderer.material);
                }
                else
                {
                    _highlightMaterials[i] = _renderer.material;
                }

                ApplyHighlightToMaterial(_highlightMaterials[i], settings, 1f);
                _renderer.material = _highlightMaterials[i];
            }

            _pulseTimer = 0f;
        }

        private void ApplyHighlightToMaterial(Material material, HighlightSettings settings, float pulseMultiplier)
        {
            if (material == null) return;

            material.color = settings.Color * (settings.Intensity * pulseMultiplier);

            if (material.HasProperty("_EmissionColor"))
            {
                material.EnableKeyword("_EMISSION");
                material.SetColor("_EmissionColor", settings.Color * (settings.EmissionStrength * pulseMultiplier));
            }
        }

        private void UpdatePulseEffect()
        {
            if (_currentRenderers == null || _highlightMaterials == null) return;

            _pulseTimer += Time.deltaTime * _pulseSpeed;

            float _pulseValue = _normalHighlight.PulseCurve.Evaluate(_pulseTimer % 1f);

            for (int i = 0; i < _currentRenderers.Length; i++)
            {
                if (_currentRenderers[i] != null && _highlightMaterials[i] != null)
                {
                    ApplyHighlightToMaterial(_highlightMaterials[i], _normalHighlight, _pulseValue);
                }
            }
        }

        private void ResetHighlight()
        {
            if (_currentRenderers != null && _originalMaterials != null)
            {
                for (int i = 0; i < _currentRenderers.Length; i++)
                {
                    if (_currentRenderers[i] != null && _originalMaterials[i] != null)
                    {
                        _currentRenderers[i].material = _originalMaterials[i];
                    }

                    if (_highlightMaterials != null && i < _highlightMaterials.Length)
                    {
                        if (_useMaterialInstancing && _highlightMaterials[i] != null)
                        {
                            Destroy(_highlightMaterials[i]);
                        }
                    }
                }
            }

            _currentRenderers = new Renderer[0];
            _originalMaterials = new Material[0];
            _highlightMaterials = new Material[0];
            _isHighlighted = false;
        }

        private bool IsInInteractableLayer(GameObject obj)
        {
            return _interactableLayers == (_interactableLayers | (1 << obj.layer));
        }

        public void ForceHighlight(GameObject obj, HighlightSettings settings)
        {
            ResetHighlight();
            ApplyHighlight(obj, settings);
            _currentInteractable = obj;
            _isHighlighted = true;
        }

        public void SetInteractableHighlight(bool isInteractable)
        {
            if (_currentInteractable == null) return;

            var settings = isInteractable ? _interactableHighlight : _normalHighlight;
            ApplyHighlight(_currentInteractable, settings);
        }
    }
}
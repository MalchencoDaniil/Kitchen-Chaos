using KitchenChaos.Player;
using UnityEngine;

public class InteractVisual : MonoBehaviour
{
    [Header("Highlight Settings")]
    [SerializeField] private Color _highlightColor = Color.white;
    [SerializeField] private float _highlightIntensity = 1.2f;

    private Renderer _currentRenderer;
    private Material _originalMaterial;
    private Material _highlightMaterial;

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

    private void HandleInteractableFound(GameObject _interactableObject)
    {
        var renderer = _interactableObject.GetComponent<Renderer>();

        if (renderer != null)
        {
            ApplyHighlight(renderer);
        }
    }

    private void HandleInteractableLost()
    {
        ResetHighlight();
    }

    private void ApplyHighlight(Renderer renderer)
    {
        ResetHighlight();

        _currentRenderer = renderer;
        _originalMaterial = renderer.material;

        _highlightMaterial = new Material(_originalMaterial);
        _highlightMaterial.color = _originalMaterial.color * _highlightIntensity;

        _highlightMaterial.EnableKeyword("_EMISSION");
        _highlightMaterial.SetColor("_EmissionColor", _highlightColor * 0.3f);

        renderer.material = _highlightMaterial;
    }

    private void ResetHighlight()
    {
        if (_currentRenderer != null && _originalMaterial != null)
        {
            _currentRenderer.material = _originalMaterial;

            if (_highlightMaterial != null)
            {
                Destroy(_highlightMaterial);
            }
        }

        _currentRenderer = null;
        _originalMaterial = null;
        _highlightMaterial = null;
    }
}
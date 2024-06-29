using System;
using System.Collections;
using UnityEngine;

namespace Effects
{
    public class DamageFlash : MonoBehaviour
    {
        [SerializeField] private Color flashColor = Color.white;
        [SerializeField] private float flashTime = 0.25f;

        private SpriteRenderer[] _spriteRenderers;
        private Material[] _materials;

        private Coroutine _flashCoroutine;
        
        private void Awake()
        {
            _spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
            
            _materials = new Material[_spriteRenderers.Length];
            for (var i = 0; i < _materials.Length; i++)
            {
                _materials[i] = _spriteRenderers[i].material;
            }
        }

        public Coroutine DoDamageFlash()
        {
            _flashCoroutine = StartCoroutine(IEDamageFlash());
            return _flashCoroutine;
        }

        private IEnumerator IEDamageFlash()
        {
            // Set the color
            SetFlashColor();
            
            // Lerp the flash amount
            var currentFlashAmount = 0f;
            var elapsedTime = 0f;

            while (elapsedTime < flashTime)
            {
                // iterate elapsedTime
                elapsedTime += Time.deltaTime;
                
                // lerp the flash amount
                currentFlashAmount = Mathf.Lerp(1f, 0f, (elapsedTime / flashTime));
                SetFlashAmount(currentFlashAmount);

                yield return null;
            }
        }

        private void SetFlashColor()
        {
            foreach (var material in _materials)
            {
                material.SetColor("_FlashColor", flashColor);
            }
        }

        private void SetFlashAmount(float amount)
        {
            foreach (var material in _materials)
            {
                material.SetFloat("_FlashAmount", amount);
            }
        }
    }
}
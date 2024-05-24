using System;
using UnityEngine;

namespace Scripts.PlayerSettings
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Player Character SO", fileName = "PlayerCharacterSO", order = 1)]
    public class PlayerCharacterSO : ScriptableObject
    {
        [field: SerializeField] public int Id { get; private set; }
        
        [field: Header("Stats")]
        [field: SerializeField] public int MaxHealth { get; private set; } = 100;
        [field: SerializeField] public int Health { get; private set; } = 2;
        [field: SerializeField] public float Speed { get; private set; } = 10;
        
        [field: Header("Visual")]
        [field: SerializeField] public Sprite AvatarSprite { get; private set; }

        [field: SerializeField] public Color Color { get; private set; }

        [field: Header("Price")] 
        [field: SerializeField] public int UnlockPrice { get; private set; }

        [field: SerializeField] public bool UnlockByDefault { get; private set; }
    }
}
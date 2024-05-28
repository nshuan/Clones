using System;
using System.Collections.Generic;
using System.Linq;
using Core.DataHandle;
using UnityEngine;
using UnityEngine.Serialization;

namespace Scripts.PlayerSettings
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Player Character Collection", fileName = "PlayerCharacterCollection", order = 1)]
    public class PlayerCharacterCollection : ScriptableObject
    {
        private const string CharacterUnlockStatusKey = "CharacterUnlockStatusKey";
        private const string CharactersCollectionPath = "PlayerCharacters";
        
        [SerializeField] private List<PlayerCharacterSO> characters = new List<PlayerCharacterSO>();

        public CharacterUnlockStatus CharactersStatus =>
            DataHandler.Load<CharacterUnlockStatus>(CharacterUnlockStatusKey, new CharacterUnlockStatus(characters));
        
        public PlayerCharacterSO GetCharacterById(int id)
        {
            return characters.FirstOrDefault(character => character.Id == id);
        }

        public List<PlayerCharacterSO> AllCharacters()
        {
            return characters;
        }

        public void UpdateStatus(int id, bool status)
        {
            var newStatus = CharactersStatus;
            newStatus.UpdateStatus(id, status);
            DataHandler.Save<CharacterUnlockStatus>(CharacterUnlockStatusKey, newStatus);
        }

        [ContextMenu("Load Characters Data")]
        private void LoadCharactersData()
        {
            var charactersAsset = Resources.LoadAll<PlayerCharacterSO>(CharactersCollectionPath);
            characters = charactersAsset.ToList();
        }
    }

    [Serializable]
    public class CharacterUnlockStatus
    {
        [SerializeField] private Dictionary<int, bool> charactersStatus;

        public CharacterUnlockStatus(List<PlayerCharacterSO> characters)
        {
            charactersStatus = new Dictionary<int, bool>();
            foreach (var character in characters)
            {
                charactersStatus[character.Id] = false;
            }
        }
        
        public bool LookupStatus(int id)
        {
            return charactersStatus.ContainsKey(id) && charactersStatus[id];
        }

        public void UpdateStatus(int id, bool value)
        {
            charactersStatus[id] = value;
        }
    }
}

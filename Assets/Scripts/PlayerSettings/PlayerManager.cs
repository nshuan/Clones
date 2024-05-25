using System;
using System.Collections.Generic;
using Core;
using Core.DataHandle;
using Managers;
using UnityEngine;

namespace Scripts.PlayerSettings
{
    public class PlayerManager : MonoSingleton<PlayerManager>
    {
        private const string CurrentCharIdKey = "CurrentCharacterId";

        private PlayerCharacterCollection _charCollection;
        public PlayerCharacterCollection CharCollection
        {
            get
            {
                // _charCollection ??= Resources.FindObjectsOfTypeAll<PlayerCharacterCollection>()[0];;
                _charCollection = Resources.Load<PlayerCharacterCollection>("PlayerCharacterCollection");
                return _charCollection;
            }
        }

        public PlayerCharacterSO CurrentCharacter => CharCollection.GetCharacterById(DataHandler.Load<int>(CurrentCharIdKey, 0));
        public List<PlayerCharacterSO> AllCharacters => CharCollection.AllCharacters();

        public static event Action<int> OnChangeCharacter;
        public static event Action<int> OnUnlockCharacter;
        
        public void SetCharacter(int id)
        {
            DataHandler.Save(CurrentCharIdKey, id);
            OnChangeCharacter?.Invoke(id);
        }

        public bool GetCharacterStatus(int id)
        {
            if (CharCollection.GetCharacterById(id).UnlockByDefault) return true;
            return _charCollection.CharactersStatus.LookupStatus(id);
        }
        
        public void UnlockCharacter(PlayerCharacterSO character)
        {
            CoinManager.CurrentCoin -= character.UnlockPrice;
            _charCollection.UpdateStatus(character.Id, true);
            OnUnlockCharacter?.Invoke(character.Id);
        }
    }
}

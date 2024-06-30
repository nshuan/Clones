using System;
using System.Collections.Generic;
using System.Linq;
using Core;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Managers
{
   
    public class GunManager : MonoSingleton<GunManager>
    {
        private const string GunCollectionPath = "GunCollection";
        private GunCollection Collection => Resources.Load<GunCollection>(GunCollectionPath);
        public int GunCount => Collection.guns.Count;

        public Gun GetGun(int id)
        {
            return Collection.guns.FirstOrDefault(gun => gun.GetId() == id);
        }

        public Gun GetGun(GunType type)
        {
            return Collection.guns.FirstOrDefault(gun => gun.GetGunType() == type);
        }

        public Gun GetRandomGun()
        {
            return Collection.guns[Random.Range(0, Collection.guns.Count)];
        }

        public Gun GetRandomGunExcept(Gun except)
        {
            var random = new System.Random();
            var filteredList = Collection.guns.FindAll(element => !EqualityComparer<Gun>.Default.Equals(element, except));
        
            if (filteredList.Count == 0)
            {
                return except;
            }

            var index = random.Next(filteredList.Count);
            return filteredList[index];
        }
    }
}
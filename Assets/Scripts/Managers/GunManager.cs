using System.Linq;
using Core;
using UnityEngine;

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
            return Collection.guns[UnityEngine.Random.Range(0, Collection.guns.Count)];
        }
    }
}
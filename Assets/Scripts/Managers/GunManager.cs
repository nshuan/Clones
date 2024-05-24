using System.Linq;
using Core;

namespace Managers
{
   
    public class GunManager : MonoSingleton<GunManager>
    {
        private const string GunCollectionPath = "GunCollection";
        private GunCollection gunCollection;
        public int gunCount => gunCollection.guns.Count;

        protected override void Awake()
        {
            base.Awake();

            gunCollection = UnityEngine.Resources.Load<GunCollection>(GunCollectionPath);
        }

        public Gun GetGun(int id)
        {
            return gunCollection.guns.FirstOrDefault(gun => gun.GetId() == id);
        }

        public Gun GetGun(GunType type)
        {
            return gunCollection.guns.FirstOrDefault(gun => gun.GetGunType() == type);
        }

        public Gun GetRandomGun()
        {
            return gunCollection.guns[UnityEngine.Random.Range(0, gunCollection.guns.Count)];
        }
    }
}
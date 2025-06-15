using UnityEngine;

namespace UI
{
    public class InventorySlot : MonoBehaviour
    {
        [SerializeField] private WeaponInfo weaponInfo;

        public WeaponInfo GetWeaponInfo()
        {
            return weaponInfo;
        }
    }
}

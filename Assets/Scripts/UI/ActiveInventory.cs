using Managements;
using Player;
using UnityEngine;

namespace UI
{
    public class ActiveInventory : Singleton<ActiveInventory>
    {
        private int activeSlotIndexNum = 0;

        private PlayerControls playerControls;

        protected override void Awake()
        {
            base.Awake();

            playerControls = new PlayerControls();
        }

        private void Start()
        {
            playerControls.Inventory.Keyboard.performed += ctx => ToggleActiveSlot((int)ctx.ReadValue<float>());
        }

        private void OnEnable()
        {
            playerControls.Enable();
        }

        public void EquipStartingWeapon()
        {
            ToggleActiveHighlight(0);
        }

        private void ToggleActiveSlot(int numValue)
        {
            ToggleActiveHighlight(numValue - 1);
        }

        private void ToggleActiveHighlight(int indexNum)
        {
            activeSlotIndexNum = indexNum;

            foreach (Transform inventorySlot in this.transform)
            {
                inventorySlot.GetChild(0).gameObject.SetActive(false);
            }

            this.transform.GetChild(indexNum).GetChild(0).gameObject.SetActive(true);

            ChangeActiveWeapon();
        }

        private void ChangeActiveWeapon()
        {
            // Kiểm tra xem ActiveWeapon.Instance có tồn tại không
            if (ActiveWeapon.Instance == null)
            {
                Debug.LogError("ActiveWeapon.Instance is null! Cannot change weapon.");
                return;
            }

            if (ActiveWeapon.Instance.CurrentActiveWeapon != null)
            {
                Destroy(ActiveWeapon.Instance.CurrentActiveWeapon.gameObject);
            }

            Transform childTransform = transform.GetChild(activeSlotIndexNum);
            InventorySlot inventorySlot = childTransform.GetComponentInChildren<InventorySlot>();
            WeaponInfo weaponInfo = inventorySlot.GetWeaponInfo();
            
            if (weaponInfo == null)
            {
                ActiveWeapon.Instance.WeaponNull();
                return;
            }
            
            GameObject weaponToSpawn = weaponInfo.weaponPrefab;
            GameObject newWeapon = Instantiate(weaponToSpawn, ActiveWeapon.Instance.transform);

            //ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, 0, 0);
            //newWeapon.transform.parent = ActiveWeapon.Instance.transform;

            ActiveWeapon.Instance.NewWeapon(newWeapon.GetComponent<MonoBehaviour>());
        }
    }
}

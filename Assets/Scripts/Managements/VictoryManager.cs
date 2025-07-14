using UnityEngine;

namespace Managements
{
    public class VictoryManager : MonoBehaviour
    {
        [SerializeField] private GameObject victoryObject;
        
        private void Start()
        {
            // Find the Victory object if not assigned
            if (victoryObject == null)
            {
                victoryObject = GameObject.Find("Victory");
            }
            
            // Initially hide the victory object
            if (victoryObject != null)
            {
                victoryObject.SetActive(false);
                Debug.Log("VictoryManager: Victory object hidden at start.");
            }
            else
            {
                Debug.LogWarning("VictoryManager: Victory object not found!");
            }
        }
        
        public void ShowVictory()
        {
            if (victoryObject != null)
            {
                victoryObject.SetActive(true);
                Debug.Log("VictoryManager: Victory object shown!");
            }
        }
        
        public void HideVictory()
        {
            if (victoryObject != null)
            {
                victoryObject.SetActive(false);
                Debug.Log("VictoryManager: Victory object hidden!");
            }
        }
    }
}

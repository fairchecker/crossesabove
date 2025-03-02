using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    [SerializeField] private GameObject mainGrid;
    [SerializeField] private GameObject tileMap;
    private void Awake()
    {
        Instantiate(tileMap, mainGrid.transform);
    }
        
}

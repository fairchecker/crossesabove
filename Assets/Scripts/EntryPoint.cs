using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using Unity.Cinemachine;
using View;

public class EntryPoint : MonoBehaviour
{
    [SerializeField] private GameObject mainGrid;
    [SerializeField] private GameObject tileMap;
    [SerializeField] private GameObject wallMap;
    
    [SerializeField] private GameObject actor;
    [SerializeField] private CinemachineCamera cinemachineCamera;
    private void Awake()
    {
        Instantiate(tileMap, mainGrid.transform);
        Instantiate(wallMap, mainGrid.transform);
        var actorObject = Instantiate(actor);
        actorObject.GetComponent<CharacterView>().Initialize(0.5f);
        
        cinemachineCamera.Follow = actorObject.transform;
    }
        
}

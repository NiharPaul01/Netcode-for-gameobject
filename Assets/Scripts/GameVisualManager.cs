using Unity.Netcode;
using UnityEngine;

public class GameVisualManager : NetworkBehaviour
{
    private const float GRID_SIZE = 3.1f;

    [SerializeField] private Transform crossPrefab;
    [SerializeField] private Transform cirlcePrefab;

    private void Start ()
    {
        GameManager.Instance.OnClickedOnGridPosition += GameManager_OnclickedOnGridPosition;
    }


    private void GameManager_OnclickedOnGridPosition(object sender, GameManager.OnClickedOnGridPositionEventArgs e)
    {
        Debug.Log("GameManager_OnClickOnGridPosition");
        SpawnObjectRpc(e.x, e.y);
    }

    [Rpc(SendTo.Server)]
    private void SpawnObjectRpc(int x, int y)
    {
        Debug.Log("SpawnObject");
        Transform spawnedCrossTransform = Instantiate(crossPrefab, GetGridWorldPosition(x,y), Quaternion.identity);
        spawnedCrossTransform.GetComponent<NetworkObject>().Spawn(true);
    }

    private Vector2 GetGridWorldPosition(int x, int y)
    {
        return new Vector2(-GRID_SIZE + x * GRID_SIZE, -GRID_SIZE + y * GRID_SIZE);
    }
}

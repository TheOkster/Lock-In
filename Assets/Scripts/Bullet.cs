using UnityEngine;
using Unity.Netcode;

public class Bullet : NetworkBehaviour
{
    public NetworkVariable<Vector3> startPosition = new NetworkVariable<Vector3>();
    public NetworkVariable<Vector3> endPosition = new NetworkVariable<Vector3>();
    public float speed = 100f;
    private float timeElapsed = 0f;
    private float totalTime;

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        if (IsServer)
        {
            timeElapsed = 0f;
            totalTime = Vector3.Distance(startPosition.Value, endPosition.Value) / speed;
        }
    }

    void Update()
    {
        if (!IsServer) return;

        float t = timeElapsed / totalTime;
        transform.position = Vector3.Lerp(startPosition.Value, endPosition.Value, t);
        timeElapsed += Time.deltaTime;
        if (t >= 1f)
        {
            NetworkObject.Despawn();
        }
    }

}

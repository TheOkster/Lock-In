using Unity.Mathematics;
using Unity.Netcode;
using UnityEngine;

public class HealthSystem : NetworkBehaviour
{
    private NetworkVariable<bool> health = new NetworkVariable<bool>(
        true,
        NetworkVariableReadPermission.Everyone,
        NetworkVariableWritePermission.Owner
    ); // Temporarily
}

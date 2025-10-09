using Unity.Netcode.Components;
using UnityEngine;

public class ClientNetworkZTransform : NetworkTransform
{
    protected override bool OnIsServerAuthoritative()
    {
        return false;
    }
}

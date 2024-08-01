using UnityEngine;
using extOSC;

public class OSCReceiverComponent : MonoBehaviour
{
    public string oscAddress = "/cube/move";
    public int localPort = 9001;
    public GameObject cube;
    public float movementScale = 0.01f; // Adjust this value to slow down the movement

    private OSCReceiver receiver;

    void Start()
    {
        // Create and configure the OSC receiver
        receiver = gameObject.AddComponent<OSCReceiver>();
        receiver.LocalPort = localPort;

        // Bind the callback to the address
        receiver.Bind(oscAddress, ReceivedMessage);

        Debug.Log("OSC receiver started on port " + localPort);
    }

    private void ReceivedMessage(OSCMessage message)
    {
        // Check if the message has values and if the address matches
        if (message.Address == oscAddress && message.Values.Count > 0)
        {
            if (message.Values[0].Type == OSCValueType.Float)
            {
                float moveValue = message.Values[0].FloatValue;
                MoveCube(moveValue);
                Debug.Log("Received OSC message: " + moveValue);
            }
        }
    }

    void MoveCube(float value)
    {
        Vector3 position = cube.transform.position;
        position.x += value * movementScale; // Scale down the movement
        cube.transform.position = position;
    }
}

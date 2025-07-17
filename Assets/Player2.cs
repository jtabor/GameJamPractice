using UnityEngine;
using UnityEngine.InputSystem;

public class Player2 : MonoBehaviour
{
    //instance of Tank.cs
    public GameObject controlledObject;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        // Handle tab key for switching tanks
        if (Keyboard.current.tabKey.wasPressedThisFrame)
        {
            SwitchToNextTank();
        }

        // Handle movement input if we have a controlled object
        if (controlledObject != null)
        {
            Tank tank = controlledObject.GetComponent<Tank>();
            if (tank != null)
            {
                // Handle WS movement (forward/back)
                Vector3 movement = Vector3.zero;
                
                if (Keyboard.current.wKey.isPressed)
                    movement += Vector3.forward;
                if (Keyboard.current.sKey.isPressed)
                    movement += Vector3.back;

                // Send movement to tank
                tank.HandleInput(movement);

                // Handle AD rotation
                float rotation = 0f;
                if (Keyboard.current.aKey.isPressed)
                    rotation = -1f;
                if (Keyboard.current.dKey.isPressed)
                    rotation = 1f;

                tank.HandleRotation(rotation);

                // Handle QE turret rotation
                float turretRotation = 0f;
                if (Keyboard.current.qKey.isPressed)
                    turretRotation = -1f;
                if (Keyboard.current.eKey.isPressed)
                    turretRotation = 1f;

                tank.HandleTurretRotation(turretRotation);
            }
        }
    }

    void SwitchToNextTank()
    {
        // Find all tanks with PlayerTank tag and sort by vehicleId
        GameObject[] tankObjects = GameObject.FindGameObjectsWithTag("PlayerTank");
        Tank[] allTanks = new Tank[tankObjects.Length];

        for (int i = 0; i < tankObjects.Length; i++)
        {
            allTanks[i] = tankObjects[i].GetComponent<Tank>();
        }

        System.Array.Sort(allTanks, (tank1, tank2) => tank1.vehicleId.CompareTo(tank2.vehicleId));

        if (allTanks.Length == 0) return;

        // Find current tank index
        int currentIndex = -1;
        for (int i = 0; i < allTanks.Length; i++)
        {
            if (allTanks[i].gameObject == controlledObject)
            {
                currentIndex = i;
                break;
            }
        }

        // Switch to next tank (or first if none selected)
        int nextIndex = (currentIndex + 1) % allTanks.Length;
        Tank old_tank = controlledObject.GetComponent<Tank>();
        old_tank.deactivateTank();
        controlledObject = allTanks[nextIndex].gameObject;
        Tank new_tank = controlledObject.GetComponent<Tank>();
        new_tank.activateTank();
        
    }
}

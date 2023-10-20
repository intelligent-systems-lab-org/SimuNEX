using System.Collections.Generic;
using UnityEngine;

public class ActuatorSystem : MonoBehaviour
{
    public List<Actuator> actuators = new();
    private Dynamics dynamics;

    // Start is called before the first frame update
    void Start()
    {
        dynamics = GetComponent<Dynamics>();
        actuators = new List<Actuator>(GetComponentsInChildren<Actuator>());
    }

    // Update is called once per frame
    void Update()
    {

    }
}

public class Actuator : MonoBehaviour
{

}

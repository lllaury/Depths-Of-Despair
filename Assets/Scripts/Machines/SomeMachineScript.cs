using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SomeMachineScript : AbstractMachine
{
    public override void MachineFunction()
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        toolForFixing = "Some Item";
    }
}

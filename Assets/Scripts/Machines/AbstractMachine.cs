using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractMachine : MonoBehaviour
{
    protected string toolForFixing; // Protected for subclass access
    [SerializeField] protected float repairProgress = 100f; // 100 is working, below is broken
    [SerializeField] private GameObject repairMachineText;
    [SerializeField] private bool isRepairing = false;

    private void Update()
    {
        if (isRepairing)
        {
            repairProgress += Time.deltaTime * 5; // Adjust the repair rate as needed
            if (repairProgress >= 100f)
            {
                repairProgress = 100f;
                isRepairing = false;
                repairMachineText.SetActive(false);
                Debug.Log("Machine fully repaired.");
            }
        }
    }

    public virtual void FixMachine()
    {
        isRepairing = true;
    }

    public bool IsMachineBroken()
    {
        return repairProgress < 100f;
    }

    public void StopRepairing()
    {
        isRepairing = false;
    }

    public bool CheckCorrectTool(string tool)
    {
        return tool.Contains(toolForFixing);
    }

    public abstract void MachineFunction(); // Called by machine manager if it's working

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && repairProgress < 100f)
        {
            repairMachineText.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && repairMachineText.activeSelf)
        {
            repairMachineText.SetActive(false);
            isRepairing = false;
        }
    }
}

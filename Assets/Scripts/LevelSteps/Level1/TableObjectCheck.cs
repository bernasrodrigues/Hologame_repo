
using UnityEngine;
using UnityEngine.Events;

public class TableObjectCheck : MonoBehaviour
{
    public Level1Step9 level1Step9;


    private void OnTriggerEnter(Collider other)
    {
        level1Step9.ObjectEnteredTrigger(other);
    }


    private void OnTriggerExit(Collider other)
    {
        level1Step9.ObjectExitTrigger(other);
    }


}

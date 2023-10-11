using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1Step9 : LevelSteps
{
    public TableObjectCheck tableObjectCheck;


    public bool mirrorInTable = false;
    public bool laserInTable = false;
    public bool beamSplitterInTable = false;
    public bool expanderInTable = false;



    public void ObjectEnteredTrigger(Collider other)
    {
        BaseObject baseObject = other.GetComponent<BaseObject>();

        if (baseObject != null)
        {
            if (baseObject.objectInfo != null)
            {
                if (baseObject.objectInfo.tag == Tag.BeamSplitter)
                {
                    beamSplitterInTable = true;
                }
                else if (baseObject.objectInfo.tag == Tag.Mirror)
                {
                    mirrorInTable = true;
                }
                else if (baseObject.objectInfo.tag == Tag.LaserEmitter)
                {
                    laserInTable = true;
                }
                else if (baseObject.objectInfo.tag == Tag.Expander)
                {
                    expanderInTable = true;
                }

                else
                {
                    return;
                }

                UpdateUI();
            }


        }
    }

    public void ObjectExitTrigger(Collider other)
    {
        BaseObject baseObject = other.GetComponent<BaseObject>();

        if (baseObject != null)
        {
            if (baseObject.objectInfo != null)
            {
                if (baseObject.objectInfo.tag == Tag.BeamSplitter)
                {
                    beamSplitterInTable = false;
                }
                else if (baseObject.objectInfo.tag == Tag.Mirror)
                {
                    mirrorInTable = false;
                }
                else if (baseObject.objectInfo.tag == Tag.LaserEmitter)
                {
                    laserInTable = false;
                }
                else if (baseObject.objectInfo.tag == Tag.Expander)
                {
                    expanderInTable = false;
                }
                else
                {
                    return;
                }

                UpdateUI();
            }


        }
    }

    public void UpdateUI()
    {
        string text = "Equipamento na mesa:\n";


        if (mirrorInTable) text += "\tEspelho\n";
        if (laserInTable) text += "\tLaser\n;";
        if (beamSplitterInTable) text += "\tBeamSplitter\n";
        if (expanderInTable) text += "\tExpansor\n";





    }



}

using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.UI;

public class ObjectCopier : MonoBehaviour
{
    public XRSocketInteractor socket;
    public ObjectInfo selectedObjectInfo;

    public Button createButton;
    public ObjectCopierUI objectCopierUI;

    public GameObject spawnPoint;
    public GameObject ExpanderPrefab;
    public GameObject MirrorPrefab;
    public GameObject GoalPrefab;
    public GameObject BeamSplitterPrefab;
    public GameObject LaserEmitterPrefab;
    public GameObject objectHolderPrefab;






    // Start is called before the first frame update
    void Start()
    {
        socket = GetComponent<XRSocketInteractor>();

        createButton.onClick.RemoveListener(ButtonPressHandler);
        createButton.onClick.AddListener(ButtonPressHandler);

        objectCopierUI.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


     public void Entered()
    {
        BaseObject selectedBaseObject = socket.selectTarget.gameObject.GetComponentInChildren<BaseObject>();
        selectedObjectInfo = selectedBaseObject.objectInfo;

        //print(selectedObjectInfoTag);

        objectCopierUI.setText(selectedObjectInfo.objectName, selectedObjectInfo.description);

    }


    public void ButtonPressHandler()
    {
        switch (selectedObjectInfo.tag)
        {
            case ObjectInfoTag.Expander:
                Instantiate(ExpanderPrefab, spawnPoint.transform.position, Quaternion.identity);
                break;
            case ObjectInfoTag.Mirror:
                Instantiate(MirrorPrefab, spawnPoint.transform.position, Quaternion.identity);
                break;
            case ObjectInfoTag.Goal:
                Instantiate(GoalPrefab, spawnPoint.transform.position, Quaternion.identity);
                break;
            case ObjectInfoTag.BeamSplitter:
                Instantiate(BeamSplitterPrefab, spawnPoint.transform.position, Quaternion.identity);
                break;
            case ObjectInfoTag.LaserEmitter:
                Instantiate(LaserEmitterPrefab, spawnPoint.transform.position, Quaternion.identity);
                break;
            case ObjectInfoTag.ObjectHolder:
                Instantiate(objectHolderPrefab, spawnPoint.transform.position, Quaternion.identity);
                break;
            default:
                print("Incorrect Object");
                break;
        }


    }
}

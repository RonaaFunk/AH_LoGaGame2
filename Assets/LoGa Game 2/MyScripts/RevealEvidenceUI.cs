using Unity.VisualScripting;
using UnityEngine;

namespace LoGaCulture.LUTE
{
    public class RevealEvidenceUI : MonoBehaviour
    {
        [SerializeField] GameObject _unfoundIcon;
        [SerializeField] GameObject _objectRender;
        [SerializeField] EvidenceManager evidenceManager;
        [SerializeField] string evidenceID;
        [VariableProperty(typeof(BooleanVariable))]
        [SerializeField] private BooleanVariable _evidenceBool;
        public bool isFound = false;
        void Start()
        {
            // if (_evidenceBool == true)
            // {
            //     _unfoundIcon.SetActive(true);
            //     _objectRender.SetActive(false);
            // }
            // else
            // {
            //     _unfoundIcon.SetActive(false);
            //     _objectRender.SetActive(true);
            // }
            
        }

        void Update()
        {
            // if (evidenceManager.evidenceDict[evidenceID] == true){
            //     isFound = true;
            // }

            if (_evidenceBool.Value == false)
            {
                _unfoundIcon.SetActive(true);
                _objectRender.SetActive(false);
            }
            else
            {
                _unfoundIcon.SetActive(false);
                _objectRender.SetActive(true);
            }
        }

        // private void Update()
        // {
        //     if (evidenceManager.evidenceDict[evidenceID] == true)
        //     {
        //         _unfoundIcon.SetActive(false);
        //         _objectRender.SetActive(true);
        //     }
        // }        
    }
}

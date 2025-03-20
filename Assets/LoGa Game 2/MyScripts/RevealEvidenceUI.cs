using UnityEngine;

namespace LoGaCulture.LUTE
{
    public class RevealEvidenceUI : MonoBehaviour
    {
        [SerializeField] GameObject _unfoundIcon;
        [SerializeField] GameObject _objectRender;
        public bool isFound;
        void Start()
        {
            if (isFound == false)
            {
                _unfoundIcon.SetActive(true);
                _objectRender.SetActive(false);
            }
        }

        public void FoundEvidence()
        {
            isFound = true;
            _unfoundIcon.SetActive(false);
            _objectRender.SetActive(true);
        }
        
    }
}

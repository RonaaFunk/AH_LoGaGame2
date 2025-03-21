using System.Collections.Generic;
using UnityEngine;

namespace LoGaCulture.LUTE
{
    public class EvidenceManager : MonoBehaviour
    {
        public bool Auroch1Found;
        public bool Auroch2Found;
        public bool Auroch3Found;
        public bool RedDeer1Found;
        public bool RedDeer2Found;
        public bool RedDeer3Found;
        public bool WildBoar1Found;
        public bool WildBoar2Found;
        public bool WildBoar3Found;

        public Dictionary<string, bool> evidenceDict;
    
        void Start()
        {
            evidenceDict = new Dictionary<string, bool>();
            
            evidenceDict.Add("A1", Auroch1Found);
            evidenceDict.Add("A2", Auroch2Found);
            evidenceDict.Add("A3", Auroch3Found);
            evidenceDict.Add("RD1", RedDeer1Found);
            evidenceDict.Add("RD2", RedDeer2Found);
            evidenceDict.Add("RD3", RedDeer3Found);
            evidenceDict.Add("WB1", WildBoar1Found);
            evidenceDict.Add("WB2", WildBoar2Found);
            evidenceDict.Add("WB3", WildBoar3Found);

            Debug.Log(evidenceDict["A1"]);
        }
        
        public void FoundEvidence(string evidenceID)
        {
                evidenceDict[evidenceID] = true;
                Debug.Log(evidenceDict[evidenceID]);
        }    
    
    }
}

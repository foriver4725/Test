using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SO
{
    public class GetParamsSO : MonoBehaviour
    {
        void Update()
        {
            // Debug.Log(TestParamsSO.Entity.DataBases[0].name);

            List<Database> found = TestParamsSO.Entity.GetData(Database.Type.SLIME);
            foreach (Database database in found)
            {
                Debug.Log(database.name);
            }
        }
    }
}

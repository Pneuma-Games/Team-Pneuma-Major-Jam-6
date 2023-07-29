using UnityEngine;

namespace Life
{
    public class DrillUIControllerTester : MonoBehaviour
    {
        private DrillUIController _ctrlr;

        private int RPM = 1000;
        
        private void Start()
        {
            _ctrlr = GetComponent<DrillUIController>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                _ctrlr.SetInput("Present");
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                _ctrlr.SetStatus("Drilling");
            }
            if (Input.GetKeyDown(KeyCode.LeftBracket))
            {
                RPM -= 1000;
                _ctrlr.SetRPM(RPM);
            }
            if (Input.GetKeyDown(KeyCode.RightBracket))
            {
                RPM += 1000;
                _ctrlr.SetRPM(RPM);
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                _ctrlr.SetLube(true);
            }
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                _ctrlr.SetError(true);
            }
        }
    }
}
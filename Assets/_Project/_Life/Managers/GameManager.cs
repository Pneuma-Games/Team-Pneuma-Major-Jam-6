using UnityEngine;
using UnityEngine.SceneManagement;

namespace Life.Managers
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField]
        public int numberOfMandatorySpecimens = 4;
        public int numberOfStoredMandatorySpecimens = 0;
        // magic will happen here
        
        private void Start () { Cursor.lockState = CursorLockMode.Locked; }
        public void ResetScene()
        {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }

        private void Update()
        {
            if (numberOfStoredMandatorySpecimens == numberOfMandatorySpecimens)
            {
                FindObjectOfType<VictorySequence>().PlayVictory();
            }
        }
    }
}

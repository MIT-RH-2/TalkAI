using UnityEngine;

namespace NRKernal.NRExamples
{
    /**
    * @brief  Controls the HelloAR example.
    */
    public class HelloMRController : MonoBehaviour
    {
        /**
        * @brief  A model to place when a raycast from a user touch hits a plane.
        */
        public GameObject AndyPlanePrefab;
        public GameObject target;
        //Vector3 pose = new Vector3(0.0f, 0.0f, 0.0f);

        void Start()
        {
        
            //Instantiate(AndyPlanePrefab, pose, Quaternion.identity);

            for (var i = 1; i < 10; i++)
            {

                Vector3 targetPosition = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z);
                Quaternion targetRotation = new Quaternion(target.transform.rotation.x, target.transform.rotation.y, target.transform.rotation.z, 1);

                float x;
                float y;
                float z;
                Vector3 pos;

                x = Random.Range(-10,10)*0.1f;
                y = -0.5f;
                z = Random.Range(10, 20)* 0.1f;
                pos = new Vector3(x, y, z);
                //transform.position = pos;

                //GameObject clone = InstantiateRandomScale(AndyPlanePrefab, 0.5f, 2);
                Instantiate(InstantiateRandomScale(AndyPlanePrefab, 0.5f, 2),pos, Quaternion.LookRotation(targetPosition));

            }
        }

        GameObject InstantiateRandomScale(GameObject source, float minScale, float maxScale)
        {
            GameObject clone = Instantiate(source) as GameObject;
            clone.transform.localScale = Vector3.one * Random.Range(minScale, maxScale);
            return clone;
        }

        void Update()
        {

       
            // If the player doesn't click the trigger button, we are done with this update.
            if (!NRInput.GetButtonDown(ControllerButton.TRIGGER))
            {
                return;
            }

            //code to Instantiate multiple Andy Models


            // Get controller laser origin.
            Transform laserAnchor = NRInput.AnchorsHelper.GetAnchor(ControllerAnchorEnum.RightLaserAnchor);

            RaycastHit hitResult;
            if (Physics.Raycast(new Ray(laserAnchor.transform.position, laserAnchor.transform.forward), out hitResult, 10))
            {
                if (hitResult.collider.gameObject != null && hitResult.collider.gameObject.GetComponent<NRTrackableBehaviour>() != null)
                {
                    var behaviour = hitResult.collider.gameObject.GetComponent<NRTrackableBehaviour>();
                    if (behaviour.Trackable.GetTrackableType() != TrackableType.TRACKABLE_PLANE)
                    {
                        return;
                    }

                    // Instantiate Andy model at the hit point / compensate for the hit point rotation.
                    //Instantiate(AndyPlanePrefab, hitResult.point, Quaternion.identity, behaviour.transform);
                    //Debug.Log(hitResult.point.ToString());

                    //code to Instantiate multiple Andy Models

                }
            }
        }
    }
}

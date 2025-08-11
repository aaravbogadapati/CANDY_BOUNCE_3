using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PartialResetterWithDestroyAndBallReset : MonoBehaviour
{
    public Button restartButton;
    public GameObject[] objectsToReset;
    public GameObject prefabToDestroyClonesOf;
    public GameObject ballToReset;

    private Vector3[] originalPositions;
    private Quaternion[] originalRotations;
    private Vector3 ballOriginalPosition;

    // How fast to move the ball back (units per second)
    public float ballMoveSpeed = 5f;

    private void Start()
    {
        if (restartButton != null)
            restartButton.onClick.AddListener(ResetAndDestroyClones);

        originalPositions = new Vector3[objectsToReset.Length];
        originalRotations = new Quaternion[objectsToReset.Length];

        for (int i = 0; i < objectsToReset.Length; i++)
        {
            if (objectsToReset[i] != null)
            {
                originalPositions[i] = objectsToReset[i].transform.position;
                originalRotations[i] = objectsToReset[i].transform.rotation;
            }
        }

        if (ballToReset != null)
            ballOriginalPosition = ballToReset.transform.position;
    }

    private void ResetAndDestroyClones()
    {
        // Reset other objects instantly
        for (int i = 0; i < objectsToReset.Length; i++)
        {
            if (objectsToReset[i] != null)
            {
                objectsToReset[i].transform.position = originalPositions[i];
                objectsToReset[i].transform.rotation = originalRotations[i];
            }
        }

        // Move ball smoothly back to original position
        if (ballToReset != null)
        {
            StopAllCoroutines(); // Stop any previous moves if needed
            StartCoroutine(MoveBallBack());
        }

        // Destroy clones
        if (prefabToDestroyClonesOf != null)
        {
            string cloneName = prefabToDestroyClonesOf.name + "(Clone)";
            GameObject[] allObjects = FindObjectsOfType<GameObject>();

            foreach (GameObject obj in allObjects)
            {
                if (obj.name == cloneName)
                    Destroy(obj);
            }
        }
    }

    private IEnumerator MoveBallBack()
    {
        while (Vector3.Distance(ballToReset.transform.position, ballOriginalPosition) > 0.01f)
        {
            ballToReset.transform.position = Vector3.MoveTowards(ballToReset.transform.position, ballOriginalPosition, ballMoveSpeed * Time.deltaTime);
            yield return null;
        }
        // Ensure exact final position
        ballToReset.transform.position = ballOriginalPosition;
    }

    private void OnDestroy()
    {
        if (restartButton != null)
            restartButton.onClick.RemoveListener(ResetAndDestroyClones);
    }
}

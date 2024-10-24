using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [Header("Studnet Settings")]
    [SerializeField] private byte StudCount;
    [SerializeField] GameObject StudPrefab;
    [SerializeField] private Transform StudSpawnpoint;
    [SerializeField] private DataRandomGenerator dataGen;
    public StudentScript currentStudnet;
	public StudentScript CurrentStudnet => currentStudnet;
	private List<StudentClass> students;
    public List<StudentClass> Students => students;
    private byte studIndex = 0;

    [Header("Document Settings")]
    [SerializeField] private GameObject docPrefab;
    [SerializeField] private List<Transform> DocSpawnpoints;
	[SerializeField] private List<Transform> DocEndpoints;
	private DocumentScript doc;
    

    private (byte, byte) time;

	private void Awake()
	{
        students = new List<StudentClass>();
		dataGen.generateData(students, StudCount);
        students.Sort((s1, s2) => s1.course.CompareTo(s2.course));
	}
	private void Start()
	{
        time = (9, 30);
        spawnStud();
	}

    private void spawnStud()
    {
        //time logic ete jamanak mnac
        if (StudCount >= 0)
        {
			currentStudnet = Instantiate(StudPrefab, StudSpawnpoint).GetComponentInParent<StudentScript>();
			currentStudnet.isGirl = students[studIndex].isGirl;
            currentStudnet.setSprite();
		}
		else
        {
            // game end realisation
        }
		StudCount--;
	}

	private void StudentLeft()
    { 
        currentStudnet = null;
        currentStudnet.gameObject.SetActive(false);
        Destroy(currentStudnet);
        studIndex++;
        spawnStud();
    }

    public void docSpawn()
    {
        int index = Random.Range(0, DocSpawnpoints.Count);

        doc = Instantiate(docPrefab, DocSpawnpoints[index]).GetComponent<DocumentScript>();
        (doc.studName, doc.studSurname, doc.course) =
        (students[studIndex].studName, students[studIndex].studSurname, students[studIndex].course);
        doc.endPoint = DocEndpoints[index];
    }
}

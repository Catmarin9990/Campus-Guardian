using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [Header("Studnet Settings")]
    [SerializeField] private byte StudCount;
    [SerializeField] GameObject StudPrefab;
    [SerializeField] private Transform StudSpawnpoint;
    [SerializeField] private DataRandomGenerator dataGen;
    private StudentScript currentStudnet;
	public StudentScript CurrentStudnet => currentStudnet;
	private List<StudentClass> students;
    public List<StudentClass> Students => students;
    private byte studIndex = 0;

    [Header("Document Settings")]
    [SerializeField] private GameObject docPrefab;
    [SerializeField] private List<Transform> DocSpawnpoints;
	[SerializeField] private List<Transform> DocEndpoints;
	private DocumentScript doc;

	[Space]
	[Header("Chance Settings")]
	[SerializeField] private float forgotChance = 15f;
	[SerializeField] private float wrongChance = 8f;
    private bool isForgot = false;
    private bool isWrong = false;

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

	public void StudentLeft()
    { 
		studIndex++;
		Destroy(currentStudnet.transform.GetChild(0));
		currentStudnet = null;
		spawnStud();
	}

	public void docSpawn()
    {
        int index = Random.Range(0, DocSpawnpoints.Count);
		if (forgotChance < Random.Range(0f, 100f))
        {
			doc = Instantiate(docPrefab, DocSpawnpoints[index]).GetComponent<DocumentScript>();
            isForgot = true;
			(doc.studName, doc.studSurname, doc.course) =
            (students[studIndex].studName, students[studIndex].studSurname, students[studIndex].course);
		}
		else
		{
			(currentStudnet.studName, currentStudnet.studSurname, currentStudnet.course) =
			(students[studIndex].studName, students[studIndex].studSurname, students[studIndex].course);
		}

		if (wrongChance > Random.Range(0f, 100f))
		{
			StudentClass stud = dataGen.generateData();
            if (isForgot)
            {
				(currentStudnet.studName, currentStudnet.studSurname, currentStudnet.course) =
                (stud.studName, stud.studSurname, stud.course);
			}
            else
            {
				(doc.studName, doc.studSurname, doc.course) =
				(stud.studName, stud.studSurname, stud.course);
			}
            isWrong = true;
		}
        if(isForgot)
		    doc.endPoint = DocEndpoints[index];
    }

    public void choiceChacker(bool choice)
    {
		if (currentStudnet != null)
		{
			if (choice)
			{
				currentStudnet.fadeOut = true;
			}
			else
			{
				currentStudnet.goBack = true;
			}
		}
	}
}

using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [Header("Studnet Settings")]
    [SerializeField] private byte StudCount;
	private byte studLeft;
	private byte allStudCount = 40;
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
	private float forgotChance = 10f;
	private float wrongChance = 8f;
    private bool isForgot = false;
    private bool isWrong = false;

	private (byte, byte) time;
	[SerializeField] private TextSpawnScript console;

	private void Awake()
	{
        students = new List<StudentClass>();
		dataGen.generateData(students, allStudCount);
        students.Sort((s1, s2) => s1.course.CompareTo(s2.course));
		studIndex = (byte)Random.Range(0, allStudCount);
	}
	private void Start()
	{
		studLeft = allStudCount;
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
		studLeft--;
		Students.RemoveAt(studIndex);
		studIndex = (byte)Random.Range(0, studLeft);
		Destroy(currentStudnet.transform.GetChild(0).gameObject);
		currentStudnet = null;
		spawnStud();
	}

	public void docSpawn()
    {
        int index = Random.Range(0, DocSpawnpoints.Count);

		(currentStudnet.studName, currentStudnet.studSurname, currentStudnet.course) =
		(students[studIndex].studName, students[studIndex].studSurname, students[studIndex].course);

		if (forgotChance < Random.Range(0f, 100f))
        {
			doc = Instantiate(docPrefab, DocSpawnpoints[index]).GetComponent<DocumentScript>();
			(doc.studName, doc.studSurname, doc.course) =
            (students[studIndex].studName, students[studIndex].studSurname, students[studIndex].course);
		}
		else
			isForgot = true;

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
        if(!isForgot)
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

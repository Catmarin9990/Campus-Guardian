using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [Header("Studnet Settings")]
    private byte StudCount = 12;
	private byte studLeft;
	private byte allStudCount = 40;

	[SerializeField] GameObject StudPrefab;
    [SerializeField] private Transform StudSpawnpoint;
    private StudentScript currentStudnet;
	public StudentScript CurrentStudnet => currentStudnet;
	private List<StudentClass> students = new List<StudentClass>();
    public List<StudentClass> Students => students;
    private byte studIndex = 0;

    [Header("Document Settings")]
    [SerializeField] private GameObject docPrefab;
    [SerializeField] private List<Transform> DocSpawnpoints;
	[SerializeField] private List<Transform> DocEndpoints;
	private DocumentScript doc;

	// Chance Settings
	private float forgotChance = 10f;
	private float wrongChance = 8f;
    private bool isForgot = false;
    private bool isWrong = false;
	private byte wrongCount = 0;

	[Space]
	private (byte, byte) time;
	[SerializeField] private TextSpawnScript console;
	[SerializeField] private DataRandomGenerator dataGen;

	private void Awake()
	{
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
		}
		else
        {
			console.sentenses.Add(console.TypeSentånce($"Wrongs count: {wrongCount}\n"));
			StartCoroutine(console.MainCoroutine());
        }
		StudCount--;
	}

	public void StudentLeft()
    {
		studLeft--;
		isForgot = false;
		isWrong = false;
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

		console.setIntroductionAnswer(students[studIndex].studName, students[studIndex].studSurname, students[studIndex].course);

		if (forgotChance < Random.Range(0f, 100f))
        {
			doc = Instantiate(docPrefab, DocSpawnpoints[index]).GetComponent<DocumentScript>();
			(doc.studName, doc.studSurname, doc.course) =
            (students[studIndex].studName, students[studIndex].studSurname, students[studIndex].course);
		}
		else
		{
			isForgot = true;
			currentStudnet.isGetDoc = true;
		}

		if (wrongChance > Random.Range(0f, 100f))
		{
			StudentClass stud = dataGen.generateData();
			console.setIntroductionAnswer(stud.studName, stud.studSurname, stud.course);
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
		//add later
		if (currentStudnet != null)
		{
			StopAllCoroutines();
			console.canWrite = false;
			if (choice)
			{
				if (isWrong)
				{
					console.sentenses.Add(console.TypeSentånce(console.WrongOneIn));
					StartCoroutine(console.MainCoroutine());
					wrongCount++;
				}
				currentStudnet.leave(true);
			}
			else
			{
				if (!isWrong)
				{
					console.sentenses.Add(console.TypeSentånce(console.WasAlright));
					StartCoroutine(console.MainCoroutine());
					wrongCount++;
				}
				currentStudnet.leave(false);
			}
			console.canWrite = true;
		}
	}
}

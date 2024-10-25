using System.Collections.Generic;
using UnityEngine;

public class DataRandomGenerator : MonoBehaviour
{
	private string[] engBoyNames =
	{
		"Ethan", "Olivia", "Benjamin", "Alexander",
		"William", "Daniel", "James", "Christopher"
	};

	private string[] engGrlNames =
	{
		"Emma", "Sophia", "Ava", "Charlotte",
		"Mia", "Michael", "Emily"
	};

	private string[] engSurnames =
	{
		"Thompson", "Davis", "Clark", "Johnson", "Martinez",
		"White", "Carter", "Brown", "Rodriguez", "Miller",
		"Wilson", "Taylor", "Anderson", "Moore", "Garcia",
	};

	private string[] armBoyNames =
	{
		 "Arman", "Gevorg", "Harutyun","Hayk", "Tigran", "Vardan",
		 "Artur", "Suren", "Karen", "Gor", "Gagik", "Hovhannes",
		 "Vahagn", "David","Aram", "Ruben", "Mher", "Ashot", "Hrayr", "Vahan",
		 "Aramayis", "Gurgen", "Sargis"
	};
	
	private string[] armGrlNames =
	{
		 "Ani", "Lilit", "Anna", "Siranush", "Nune",
		 "Marine", "Satenik", "Tatevik", "Silva",
		 "Gayane", "Mariam", "Astghik", "Anahit",
		 "Lusine", "Naira", "Emma", "Zara",
		 "Narine", "Liana",  "Anush"
	};

	private string[] armSurnames =
	{
		"Harutyunyan", "Avetisyan", "Hakobyan", "Grigoryan", "Manukyan",
		"Ghazaryan", "Petrosyan", "Mkrtchyan", "Makaryan","Sargsyan", "Asatryan",
		"Ohanyan", "Melkonyan", "Vardanyan", "Minasyan", "Hovhannisyan",
		"Martirosyan", "Sahakyan", "Karapetyan", "Poghosyan", "Davtyan",
		"Gasparyan", "Hakobyan", "Kokoshyan", "Tovmasyan", "Hovsepyan", "Sahakyan",
		"Grigoryan", "Mkhitaryan", "Gevorgyan", "Petrosyan", "Kazaryan",
		"Martirosyan", "Sargsyan", "Gasparyan", "Avagyan", "Harutyunyan",
		"Sahakyan", "Karapetyan", "Hovhannisyan", "Mkrtchyan", "Avetisyan"
	};

	[SerializeField] private float foreignerChance = 15;
	[SerializeField] private float zeroCourseChance = 8;

	public void generateData(List<StudentClass> students, byte size)
	{
		bool isGirl;
		bool isForeigner;
		byte counter = 1;
		for (int i = 0; i < size; i++)
		{
			if (counter > 4) counter = 1;
			isGirl = Random.Range(0, 2) == 1;
			isForeigner = Random.Range(0f, 100f) < foreignerChance;
			if (isForeigner)
				students.Add(foreignGen(isGirl, counter));
			else
				students.Add(armGen(isGirl, counter));
			counter++;
		}
	}

	public StudentClass generateData()
	{
		bool isGirl;
		bool isForeigner;
		byte course = (byte)Random.Range(1, 4);
		StudentClass stud;

		isGirl = Random.Range(0, 2) == 1;
		isForeigner = Random.Range(0f, 100f) < foreignerChance;

		float defaultChance = zeroCourseChance;
		zeroCourseChance = 0;

		if (isForeigner)
			stud = foreignGen(isGirl, course);
		else
			stud = armGen(isGirl, course);

		zeroCourseChance = defaultChance;
		return stud;
	}

	private StudentClass foreignGen(bool isGirl, byte counter)
	{
		string name;
		string surname;
		StudentClass stud;

		if (isGirl)
			name = engGrlNames[Random.Range(0, engGrlNames.Length)];
		else
			name = engBoyNames[Random.Range(0, engBoyNames.Length)];
		surname = engSurnames[Random.Range(0, engSurnames.Length)];

		if (Random.Range(0f, 100f) < zeroCourseChance)
			counter = 0;

		stud = new StudentClass(name, surname, counter, isGirl);
		return stud;
	}

	private StudentClass armGen(bool isGirl, byte counter)
	{
		string name;
		string surname;
		StudentClass stud;

		if (isGirl)
			name = armGrlNames[Random.Range(0, engGrlNames.Length)];
		else
			name = armBoyNames[Random.Range(0, engBoyNames.Length)];
		surname = armSurnames[Random.Range(0, engSurnames.Length)];
		if (Random.Range(0f, 100f) <= 1) 
			name = "Gerasim";
		if (Random.Range(0f, 100f) < zeroCourseChance)
			counter = 0;

		stud = new StudentClass(name, surname, counter, isGirl);
		return stud;
	}
}

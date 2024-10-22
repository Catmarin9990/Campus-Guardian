using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StudentClass
{ 
	public string studName { get; }
	public string studSurname { get; }
	public byte course { get; }
	public bool isGirl { get; }

	public StudentClass(string studName, string studSurname, byte course, bool isGirl)
	{
		this.studName = studName;
		this.studSurname = studSurname;
		this.course = course;
		this.isGirl = isGirl;
	}


}

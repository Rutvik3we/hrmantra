using System;


public class Class1
{
	public Class1()
	{
		[DataNames("User_Id", "User_Id")]

		public int User_Id { get; set; }

		[DataNames("User_Name", "User_Name")]

		public string User_Name { get; set; }

		[DataNames("User_Pwd", "User_Pwd")]

		public string User_Pwd { get; set; }

		[DataNames("Is_Login", "Is_Login")]

		public bool Is_Login { get; set; }

		[DataNames("Updated_By", "Updated_By")]

		public int Updated_By { get; set; }

		[DataNames("Updated_Date", "Updated_Date")]

		public DateTime Updated_Date { get; set; }





	}
}

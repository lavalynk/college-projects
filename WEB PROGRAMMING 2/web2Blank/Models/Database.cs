using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web2Blank.Models
{
	public class Database
	{
		public User GetUser(long ID) {
			User u = new User();

			switch (ID)
			{
				case 1:
					u.FirstName = "Jane";
					u.LastName = "Doe";
					u.UserID = "jdoe911";
					u.Password = "1234567";

					u.CellPhone = new PhoneNumber();
					u.CellPhone.AreaCode = "610";
					u.CellPhone.Prefix = "441";
					u.CellPhone.Suffix = "8110";

					u.HomeAddress = new Address();
					u.HomeAddress.Address1 = "100 Oak St.";
					u.HomeAddress.Address2 = "Apt. 1b";
					u.HomeAddress.City = "Allyntown";
					u.HomeAddress.State = "PA";
					u.HomeAddress.Zip = "18001-5459";
					break;
				case 2:
					u.FirstName = "Chris";
					u.LastName = "Jackson";
					u.UserID = "cjackson";
					u.Password = "1111111111";

					u.CellPhone = new PhoneNumber();
					u.CellPhone.AreaCode = "513";
					u.CellPhone.Prefix = "569";
					u.CellPhone.Suffix = "0001";

					u.CellPhone = new PhoneNumber();
					u.CellPhone.AreaCode = "859";
					u.CellPhone.Prefix = "581";
					u.CellPhone.Suffix = "5771";

					u.HomeAddress = new Address();
					u.HomeAddress.Address1 = "51112 West Liberty Road";
					u.HomeAddress.City = "Cincinnati";
					u.HomeAddress.State = "Ohio";
					u.HomeAddress.Zip = "45223";

					u.WorkAddress = new Address();
					u.WorkAddress.Address1 = "120 3rd St.";
					u.WorkAddress.City = "Cincinnati";
					u.WorkAddress.State = "Ohio";
					u.WorkAddress.Zip = "44522";

					u.Home2Address = new Address();
					u.Home2Address.Address1 = "3 Main St.";
					u.Home2Address.City = "Columbus";
					u.Home2Address.State = "Ohio";
					u.Home2Address.Zip = "45999";
					break;
				case 3:
					u.FirstName = "Teddy";
					u.LastName = "Roosevelt";
					u.UserID = "tRoosevelt32";
					u.Password = "32301882";

					u.CellPhone = new PhoneNumber();
					u.CellPhone.AreaCode = "606";
					u.CellPhone.Prefix = "447";
					u.CellPhone.Suffix = "3978";

					u.HomeAddress = new Address();
					u.HomeAddress.Address1 = "12 Rabbit Creek Road";
					u.HomeAddress.Address2 = "";
					u.HomeAddress.City = "Hazard";
					u.HomeAddress.State = "KY";
					u.HomeAddress.Zip = "41701";
					break;
				default:
					break;
				
			}




			return u;		
		}
	}
}
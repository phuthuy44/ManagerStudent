using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerStudent.DTO

{
    public class People
    {
        public int ID
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public string Gender
        {
            get;
            set;
        }
        public string Address
        {
            get;
            set;
        }
        public DateTime Birthday
        {
            get;
            set;
        }
        public string Birthplace
        {
            get;
            set;
        }
        public string Email
        {
            get;
            set;
        }
        public string Phone
        {
            get;
            set;
        }
        public string Image
        {
            get;
            set;
        }
        public DateTime createDate
        {
            /*set{
                this.createDate = value;
            }
            get
            {
                return this.createDate;
            }*/
            get;
            set;
        }
        public DateTime updateDate
        {
            /*set
            {
                this.updateDate = value;
            }
            get
            {
                return this.updateDate;
            }*/
            get;set;
        }
        public People(int id, string name, string gender, string address, DateTime birthday, string birthplace, string email, string phone, string image)
        {
            ID = id;
            Name = name;
            Gender = gender;
            Address = address;
            Birthday = birthday;
            Birthplace = birthplace;
            Email = email;
            Phone = phone;
            Image = image;
        }
        
        public People()
        {

        }

        public People(string name, string gender, string address, DateTime birthday, string email, string phone, string image)
        {
            Name = name;
            Gender = gender;
            Address = address;
            Birthday = birthday;
            Email = email;
            Phone = phone;
            Image = image;
        }

        public People(string name, string gender, string address, DateTime birthday, string email, string phone, string image, DateTime createDate, DateTime updateDate) : this(name, gender, address, birthday, email, phone, image)
        {
            this.createDate = createDate;
            this.updateDate = updateDate;
        }

        public People(int iD, string name, string gender, string address, DateTime birthday, string email, string phone, string image)
        {
            ID = iD;
            Name = name;
            Gender = gender;
            Address = address;
            Birthday = birthday;
            Email = email;
            Phone = phone;
            Image = image;
        }

        public People(int iD, string name, string gender, string address, DateTime birthday, string email, string phone, string image, DateTime createDate) : this(iD, name, gender, address, birthday, email, phone, image)
        {
            this.createDate = createDate;
        }

        public People(int iD, string name, string gender, string address, DateTime birthday, string phone, string image)
        {
            ID = iD;
            Name = name;
            Gender = gender;
            Address = address;
            Birthday = birthday;
            Phone = phone;
            Image = image;
          
        }

        public People(int iD, string name, string gender, string address, DateTime birthday, string email, string phone, string image, DateTime createDate, DateTime updateDate) : this(iD, name, gender, address, birthday, email, phone, image, createDate)
        {
            this.updateDate = updateDate;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC
{
    public class User 
    {
        private int userId;
        private string userName;
        private string password;
        private string email;
        private string firstName;
        private string lastName;
        private string adress;
        private int phone;
        private DateTime dateCreation;
       // public virtual ICollection<Media> Medias { get; set; }
      //  public virtual ICollection<Comment> Comments { get; set; }
      //  public virtual ICollection<Rate> Rates { get; set; }

        public User()
        {

        }
        public int UserId
        {
            get
            {
                return userId;
            }

            set
            {
                userId = value;
            }
        }

        public string UserName
        {
            get
            {
                return userName;
            }

            set
            {
                userName = value;
            }
        }

        public string Password
        {
            get
            {
                return password;
            }

            set
            {
                password = value;
            }
        }

        public string Email
        {
            get
            {
                return email;
            }

            set
            {
                email = value;
            }
        }

        public string FirstName
        {
            get
            {
                return firstName;
            }

            set
            {
                firstName = value;
            }
        }

        public string LastName
        {
            get
            {
                return lastName;
            }

            set
            {
                lastName = value;
            }
        }

      

        public int Phone
        {
            get
            {
                return phone;
            }

            set
            {
                phone = value;
            }
        }

        public DateTime DateCreation
        {
            get
            {
                return dateCreation;
            }

            set
            {
                dateCreation = value;
            }
        }

        public string Adress
        {
            get
            {
                return adress;
            }

            set
            {
                adress = value;
            }
        }
    }
}

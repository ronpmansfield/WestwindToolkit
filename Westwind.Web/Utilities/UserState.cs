using System;
using System.Diagnostics;
using System.Web;
using System.Web.Security;
using Westwind.Utilities;

namespace Westwind.Web
{
    /// <summary>
    /// User information container that can easily 'serialize'
    /// to a string and back. Meant to hold basic logon information
    /// to avoid trips to the database for common information required
    /// by an app to validate and display user info.
    /// 
    /// I use this class a lot to attach as Forms Authentication
    /// Ticket data to keep basic user data without having to
    /// hit the database
    /// </summary>    
    public class UserState
    {

        public UserState()
        {
            Name = string.Empty;
            Email = string.Empty;
            UserId = string.Empty;
            IsAdmin = false;
            SecurityToken = string.Empty;
        }

        /// <summary>
        /// The display name for the userId
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The user's email address or login acount
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// The user's user Id as a string
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// The users admin status
        /// </summary>
        public bool IsAdmin { get; set; }

        /// <summary>
        /// Returns the User Id as an int if convertiable
        /// </summary>
        public int UserIdInt
        {
            get
            {
                if (string.IsNullOrEmpty(UserId))
                    return 0;

                int id = -1;
                if (!int.TryParse(UserId, out id))
                    return -1;

                return id;
            }
            set
            {
                if (value == -1)
                    return;

                UserId = value.ToString();
            }
        }

        /// <summary>
        /// A unique id created for this entry that can be used to
        /// identify the user outside of the UserState context
        /// </summary>
        public string SecurityToken { 
            get 
            {
                if (string.IsNullOrEmpty(_SecurityToken))
                    _SecurityToken = StringUtils.NewStringId();

                return _SecurityToken;
            }
            set
            {
                _SecurityToken = value;
            }
        }
        private string _SecurityToken = null;

        

        /// <summary>
        /// Exports a short string list of Id, Email, Name separated by |
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return StringSerializer.SerializeObject(this);
        }

        /// <summary>
        /// Imports Id, Email and Name from a | separated string
        /// </summary>
        /// <param name="itemString"></param>
        public bool FromString(string itemString)
        {
            if (string.IsNullOrEmpty(itemString))
                return false;

            var state = CreateFromString(itemString);
            if (state == null)
                return false;

            // copy the properties
            DataUtils.CopyObjectData(state, this);

            return true;
        }


        /// <summary>
        /// Creates an instance of a userstate object from serialized
        /// data.
        /// 
        /// IsEmpty() will return true if data was not loaded. A 
        /// UserData object is always returned.
        /// </summary>
        /// <param name="userData"></param>
        /// <returns></returns>
        public static UserState CreateFromString(string userData)
        {
            return CreateFromString<UserState>(userData);            
        }

        /// <summary>
        /// Creates an instance of a userstate object from serialized
        /// data.
        /// 
        /// IsEmpty() will return true if data was not loaded. A 
        /// UserData object is always returned.
        /// </summary>
        /// <param name="userData"></param>
        /// <returns></returns>
        public static T CreateFromString<T>(string userData)
            where T :  class, new()
        {
            if (string.IsNullOrEmpty(userData))
                return null;
            
            T result = null;
            try
            {
                object res = StringSerializer.DeserializeObject(userData, typeof(T));
                result = res as T;
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return new T();
            }

            return result;
        }



        /// <summary>
        /// Creates a UserState object from authentication information in the 
        /// Forms Authentication ticket.
        /// 
        /// IsEmpty() will return false if no data was loaded but
        /// a Userdata object is always returned
        /// </summary>
        /// <returns></returns>
        public static UserState CreateFromFormsAuthTicket()
        {
            var identity = HttpContext.Current.User.Identity as FormsIdentity;
            if (identity == null)
                return new UserState();

            return CreateFromString(identity.Ticket.UserData);
        }

        /// <summary>
        /// Creates a UserState object from authentication information in the 
        /// Forms Authentication ticket.
        /// 
        /// IsEmpty() will return false if no data was loaded but
        /// a Userdata object is always returned
        /// </summary>
        /// <returns></returns>
        public static T CreateFromFormsAuthTicket<T>()
            where T : UserState, new()
        {
            var identity = HttpContext.Current.User.Identity as FormsIdentity;
            if (identity == null)
                return new T();            

            return CreateFromString<T>(identity.Ticket.UserData) as T;
        }



        /// <summary>
        /// Determines whether UserState instance
        /// holds user information.
        /// </summary>
        /// <returns></returns>
        public bool IsEmpty()
        {
            return string.IsNullOrEmpty(this.UserId);
        }
    }
}

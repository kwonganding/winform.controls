using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;

namespace WindowsTest
{
    public class TestData
    {


        public static List<User> GetTreeData(int len = 10)
        {
            List<User> us = new List<User>();
            for (int i = 0; i < len; i++)
            {
                User u1 = new User();
                u1.Value = i;
                u1.Name = "Name_" + i;
                u1.Users = new List<User>();
                for (int n = 0; n < len; n++)
                {
                    User u2=new User();
                    u2.Name = u1.Name + "_" + n;
                    u2.Value = n;
                    u1.Users.Add(u2);
                }
                us.Add(u1);
            }
            return us;
        }
    }

    public class User
    {
        public string Name { get; set; }
        public int Value { get; set; }

        public DateTime Birthday { get; set; }

        public string Dept { get; set; }

        public List<User> Users { get; set; }
    }
}

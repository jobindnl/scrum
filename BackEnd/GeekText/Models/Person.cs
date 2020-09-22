using System;
using System.Collections.Generic;

namespace reactiveFormWeb.Models
{
    public class Person
    {
        public int Id { get; set; }
        public int ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public string Name { get; set; }
        public string NickName { get; set; }
        public DateTime DateOFBirth { get; set; }
        public List<Address> Direcciones { get; set; }
    }
}

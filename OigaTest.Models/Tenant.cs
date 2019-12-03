using System;

namespace OigaTest.Models
{
    public class Tenant
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public bool isDefault{ get; set; }
    }
}

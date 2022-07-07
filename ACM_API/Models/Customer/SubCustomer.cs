using System.Collections.Generic;

namespace ACM_API.Models.Customer
{
    public class SubCustomer
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string FullName { get; set; }

        public ContactPerson ContactPerson { get; set; }

        public bool IsDelete { get; set; }

        public string OGRN { get; set; }

        public string INN { get; set; }

        public string KPP { get; set; }

        public string Address { get; set; }

        public string ActualAddress { get; set; }
        public CustomerType CustomerType { get; set; }

        public User User { get; set; }

        public List<Chat.Chat> Chats { get; set; }
    }
}

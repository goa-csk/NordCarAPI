using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NordCar.Shared.Domain.UniqueMessages
{
      public class UniqueMessageTemplate
    {
        public Guid Id { get; set; }
        public string Text { get; set; }

        public UniqueMessageTemplate()
        {
            //we need this and the public setters for serialization.            
        }

        public UniqueMessageTemplate(Guid guid, string text)
        {
            Text = text;
            Id = guid;
        }

        public UniqueMessageTemplate(string guid, string text)
            :this(Guid.Parse(guid),text)
        {
        }

        protected bool Equals(UniqueMessageTemplate other)
        {
            return Id.Equals(other.Id);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((UniqueMessageTemplate) obj);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public override string ToString()
        {
            //return $"Id = {Id}, TextTemplate = {Text}";
            return string.Format("Id= {0}, TextTemplate = {1}", Id, Text);
        }
    }
}

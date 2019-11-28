using Flunt.Notifications;
using System.Collections.Generic;
using System.Linq;

namespace SqTec.Domain.Core.Models
{
    public abstract class Entity : Notifiable
    {
        public IReadOnlyCollection<Notification> Errors()
        {
            return Notifications;
        }

        public string ConcatErrors()
        {
            return string.Join(",", Errors().Select(p => string.Format("{0} - {1}", p.Property, p.Message)).ToArray());
        }
    }
}

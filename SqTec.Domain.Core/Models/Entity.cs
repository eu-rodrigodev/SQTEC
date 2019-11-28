using Flunt.Notifications;
using System.Collections.Generic;

namespace SqTec.Domain.Core.Models
{
    public abstract class Entity : Notifiable
    {
        public IReadOnlyCollection<Notification> Errors()
        {
            return Notifications;
        }
    }
}

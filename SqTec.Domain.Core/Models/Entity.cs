using Flunt.Notifications;
using System.Collections.Generic;

namespace SqTec.Domain.Core.Models
{
    /// <summary>
    /// Classe abstrata de Entidades com Flunt.Notifiable para retorno de inconsistências
    /// </summary>
    public abstract class Entity : Notifiable
    {
        public IReadOnlyCollection<Notification> Errors()
        {
            return Notifications;
        }
    }
}

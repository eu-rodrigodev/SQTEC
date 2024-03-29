﻿using Flunt.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SqtTec.Exceptions
{
    /// <summary>
    /// Classe de exceção para subir as inconstâncias das entidades
    /// </summary>
    public class NotifiableException : Exception
    {
        public NotifiableException(IReadOnlyCollection<Notification> notifications) : base(string.Join(",", notifications.Select(p => string.Format("{0} - {1}", p.Property, p.Message)).ToArray())) { }
    }
}

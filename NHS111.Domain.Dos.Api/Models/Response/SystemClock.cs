using System;

namespace NHS111.Domain.Dos.Api.Models.Response
{
    public class SystemClock : IClock
    {
        public DateTime Now { get { return DateTime.Now; } }
    }

    public interface IClock
    {
        DateTime Now { get; }
    }
}

using System;
using System.ComponentModel.DataAnnotations;

namespace eMeeting.Model
{
    public class ApplicationSettings
    {
        public string JWT_Secret { get; set; }
        public string Client_URL { get; set; }
    }
}

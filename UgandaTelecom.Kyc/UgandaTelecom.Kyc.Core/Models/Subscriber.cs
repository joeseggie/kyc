using System;
using System.Collections.Generic;
using System.Text;

namespace UgandaTelecom.Kyc.Core.Models
{
    public class Subscriber
    {
        public Guid SubscriberId { get; set; }
        public string Surname { get; set; }
        public string GivenName { get; set; }
        public string Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string IdentificationNumber { get; set; }
        public string Msisdn { get; set; }
        public string IdentificationType { get; set; }
        public string Village { get; set; }
        public string District { get; set; }
        public string FaceImg { get; set; }
        public string IdFrontimg { get; set; }
        public string IdBackimg { get; set; }
        public string AgentMsisdn { get; set; }
        public DateTime RegistrationDate { get; set; }
        public TimeSpan RegistrationTime { get; set; }
        public string Mode { get; set; }
        public bool Verified { get; set; }
        public string VerificationRequest { get; set; }
        public string NiraValidation { get; set; }
        public string OtherNames { get; set; }
        public string IdCardNumber { get; set; }
        public DateTime? VisaExpiry { get; set; }
    }
}

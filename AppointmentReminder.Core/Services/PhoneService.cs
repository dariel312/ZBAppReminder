using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace AppointmentReminder.Core
{
   /// <summary>
   /// Allows Sending Messages\Phone calls
   /// </summary>
    public static class PhoneService
    {

        public static void Init(string AccountID, string AuthToken)
        {
            TwilioClient.Init(AccountID, AuthToken);
        }
        public static string SendMessage(string To, string From, string Message)
        {
           
            var to = new PhoneNumber(To);
            var from = new PhoneNumber(From);
            var msg = MessageResource.Create(
                     from: from,
                     to: to,
                     body: Message);

            if (msg.Status == MessageResource.StatusEnum.Failed)
                return msg.ErrorMessage;
            else
                return msg.Sid;
        }

        public static string SendPhoneCall(string To, string From, string TwiMLUrl)
        {

            var to = new PhoneNumber(To);
            var from = new PhoneNumber(From);

            var call = CallResource.Create(to,
                                       from,
                                       url: new Uri(TwiMLUrl));
            if (call.Status == MessageResource.StatusEnum.Failed)
                return "ERROR";
            else
                return call.Sid + " " + call.Duration;
        }
    }
}

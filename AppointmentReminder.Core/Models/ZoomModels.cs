using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentReminder.Core.Models.Zoom
{
    public class UserModel
    {
        public string id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string email { get; set; }
        public int type { get; set; }
        public long pmi { get; set; }
        public string timezone { get; set; }
        public int verified { get; set; }
        public string dept { get; set; }
        public DateTimeOffset created_at { get; set; }
        public DateTimeOffset last_login_time { get; set; }
        public string pic_url { get; set; }
        public string language { get; set; }
        public string phone_number { get; set; }
        public string status { get; set; }
        public int role_id { get; set; }
    }


    public class Pager
    {
        public int page_count { get; set; }
        public int page_number { get; set; }
        public int page_size { get; set; }
        public int total_records { get; set; }
        public string next_page_token { get; set; }
    }

    public class JsonPlaceholder<T>
    {
        public T data { get; set; }
    }

    public class UserPageResponse : Pager
    {
        public IEnumerable<UserModel> users { get; set; }


        public static UserPageResponse FromJSON(string JSON)
        {
            var model = JsonConvert.DeserializeObject<UserPageResponse>(JSON);
            return model;
        }
    }


    /// <summary>
    /// Model for creating a meeting
    /// </summary>
    public class CreateMeetingRequest
    {
        public string topic { get; set; }
        public MeetingType type { get; set; }
        public DateTime start_time { get; set; }
        public int duration { get; set; }
        public string timezone { get; set; }
        public string password { get; set; }
        public string agenda { get; set; }
        public MeetingSettings settings { get; set; }



    }

    /// <summary>
    /// Model to update meeting 
    /// Note: Many missing fields, as we only want to update time for now since C# will include nulls
    /// </summary>
    public class UpdateMeetingRequest
    {
        public DateTime start_time { get; set; }
        public int duration { get; set; }
    }

    public class MeetingSettings
    {
        public bool host_video { get; set; } = false;
        public bool participant_video { get; set; } = false;
        public bool cn_meeting { get; set; } = false;
        public bool in_meeting { get; set; } = false;
        public bool join_before_host { get; set; } = false;
        public bool mute_upon_entry { get; set; } = true;
        public bool watermark { get; set; }
        public bool use_pmi { get; set; }
        public int approve_type { get; set; }
        public int registration_type { get; set; }
        public string audio { get; set; } = "both";
        public string auto_recording { get; set; } = "none";
        public bool waiting_room { get; set; } = true;
        public bool registrants_email_notification { get; set; }
    }

    public enum MeetingType
    {
        Instant = 1,
        Scheduled = 2,
        NonFixedRecurring = 3,
        FixedRecurring = 8
    }


    public class MeetingModel
    {
        public string uuid { get; set; }
        public long id { get; set; }
        public string host_id { get; set; }
        public string host_email { get; set; }
        public string topic { get; set; }
        public MeetingType type { get; set; }
        public string status { get; set; }
        public DateTimeOffset start_time { get; set; }
        public int duration { get; set; }
        public string timezone { get; set; }
        public string agenda { get; set; }
        public DateTimeOffset created_at { get; set; }
        public string start_url { get; set; }
        public string join_url { get; set; }
        public string password { get; set; }
        public MeetingSettings settings { get; set; }
    }

    public class MeetingListItem
    {
        public string uuid { get; set; }
        public long id { get; set; }
        public string host_id { get; set; }
        public string topic { get; set; }
        public MeetingType type { get; set; }
        public DateTimeOffset start_time { get; set; }
        public int duration { get; set; }
        public string timezone { get; set; }
        public DateTimeOffset created_at { get; set; }
        public string join_Url { get; set; }

    }

    public class MeetingListResponse
    {
        public int page_size { get; set; }
        public int total_records { get; set; }
        public string next_page_token { get; set; }
        public IEnumerable<MeetingListItem> meetings { get; set; }
    }

    /// <summary>
    /// Time zones constants 
    /// Ref: https://marketplace.zoom.us/docs/api-reference/other-references/abbreviation-lists#timezones
    /// </summary>
    public static class MeetingTimeZone
    {
        public const string Eastern = "America/New_York";
    }
}

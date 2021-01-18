using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AppointmentReminder.Meetings
{
    public class CachedMeeting
    {
        public int TranId { get; set; }
        public long ZoomId { get; set; }
        public string JoinUrl { get; set; }
    }

    public class MeetingCache : IDisposable
    {
        private List<CachedMeeting> meetings = new List<CachedMeeting>();
        private string filePath;

        public MeetingCache(string FilePath)
        {
            this.filePath = FilePath;

            //create cache if not exists
            if (File.Exists(FilePath))
            {
                var body = File.ReadAllText(FilePath);
                this.meetings = JsonConvert.DeserializeObject<List<CachedMeeting>>(body);
            }

        }

        public CachedMeeting GetMeeting(int TranId)
        {
            var res = this.meetings.FirstOrDefault(m => m.TranId == TranId);
            return res;
        }

        public bool AddMeeting(CachedMeeting Body)
        {
            var exists = this.GetMeeting(Body.TranId) != null;

            //remove if exists
            if (exists)
                this.RemoveMeeting(Body);

            //add new
            this.meetings.Add(Body);

            return true;
        }

        public void RemoveMeeting(CachedMeeting Body)
        {
            this.meetings.RemoveAt(this.meetings.IndexOf(Body));
        }


        public void Flush()
        {
            var json = JsonConvert.SerializeObject(this.meetings);
            File.WriteAllText(this.filePath, json);
        }

        public void Dispose()
        {
            this.Flush();
        }
    }
}

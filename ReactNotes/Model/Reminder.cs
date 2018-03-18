using System;
using System.Collections.Generic;
using System.Text;

namespace ReactNotes.Model
{
    public class Reminder
    {
        public string Id { get; set; }

        public DateTime Time { get; set; }

        public string Text { get; set; }
    }
}

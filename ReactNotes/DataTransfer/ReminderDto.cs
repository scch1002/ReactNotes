using Microsoft.WindowsAzure.Storage.Table;
using ReactNotes.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReactNotes.DataTransfer
{
    public class ReminderDto : TableEntity
    {
        public ReminderDto()
        {
            var guid = Guid.NewGuid().ToString();

            PartitionKey = "default";
            RowKey = guid;
        }

        public ReminderDto(Reminder reminder)
        {
            PartitionKey = "default";
            RowKey = reminder.Id;
            Time = reminder.Time;
            Text = reminder.Text;
        }

        public DateTime Time { get; set; }

        public string Text { get; set; }
    }
}

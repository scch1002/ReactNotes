using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using ReactNotes.DataTransfer;
using ReactNotes.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Grace.Core.Repositories
{
    public class ReminderRepository
    {
        private readonly TableConfiguration _tableConfiguration;

        public ReminderRepository(TableConfiguration config)
        {
            _tableConfiguration = config;
        }

        public void AddReminder(Reminder reminder)
        {
            var table = _tableConfiguration.GetTableReference();

            var reminderDto = new ReminderDto
            {
                Time = reminder.Time,
                Text = reminder.Text
            };

            var insertOperation = TableOperation.Insert(reminderDto);

            table.ExecuteAsync(insertOperation).Wait();

            reminder.Id = reminderDto.RowKey;
        }

        public void DeleteReminder(Reminder reminder)
        {
            var table = _tableConfiguration.GetTableReference();

            var deleteOperation = TableOperation.Delete(new ReminderDto(reminder) { ETag = "*" });

            table.ExecuteAsync(deleteOperation).Wait();
        }

        public List<Reminder> GetReminders(DateTime beginTime, DateTime endTime)
        {
            var table = _tableConfiguration.GetTableReference();

            var query = new TableQuery<ReminderDto>();

            var results = table.ExecuteQuerySegmentedAsync(query, null).Result;

            return results
                .Results
                .Where(w => w.Time >= beginTime && w.Time <= endTime)
                .Select(s => new Reminder { Id = s.RowKey, Time = s.Time, Text = s.Text })
                .ToList();
        }
    }
}

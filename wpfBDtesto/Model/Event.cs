using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;

namespace wpfBDtesto.Model
{
    internal class Event : INotifyPropertyChanged
    {

        private string timestamp;
        private int eventNumber;
        private string severity;
        private int? eventID;
        private string eventName;
        private string serverID;
        private string serverName;
        private int? deviceID;
        private string deviceName;
        private int? dataID;
        private int? userID;

        public Event()
        {

        }


        public static ObservableCollection<Event> GetAllEvents()
        {

            var ds = SqlWork.selectData("SELECT * FROM COMMON");

            if (ds != null)
            {
                var SelectEvent = ds.Tables[0].AsEnumerable().Select(dataRow => new Event
                {
                    Timestamp = dataRow.Field<string>("Timestamp"),
                    EventNumber = dataRow.Field<int>("EventNumber"),
                    Severity = dataRow.Field<string>("Severity"),
                    EventID = dataRow.Field<int?>("EventID"),
                    EventName = dataRow.Field<string>("EventName"),
                    DeviceID = dataRow.Field<int?>("DeviceID"),
                    ServerID = dataRow.Field<string>("ServerID"),
                    ServerName = dataRow.Field<string>("ServerName"),
                    DeviceName = dataRow.Field<string>("DeviceName"),
                    DataID = dataRow.Field<int?>("DataID"),
                    UserID = dataRow.Field<int?>("UserID")

                }).ToList();


                return new ObservableCollection<Event>(SelectEvent);

            }
            return null;
        }

        public static void InsertEvent(Event eventIns)
        {
            var ds = SqlWork.selectData("SELECT * FROM COMMON");

            DataRow newRow = ds.Tables[0].NewRow();

            newRow["Timestamp"] = eventIns.Timestamp;
            newRow["EventNumber"] = eventIns.EventNumber;
            newRow["Severity"] = eventIns.Severity;
            newRow["EventID"] = eventIns.EventID;
            newRow["EventName"] = eventIns.EventName;
            newRow["ServerID"] = eventIns.ServerID;
            newRow["ServerName"] = eventIns.ServerName;
            newRow["DeviceID"] = DBNull.Value;
            newRow["DeviceName"] = DBNull.Value;
            newRow["DataID"] = DBNull.Value;
            newRow["UserID"] = DBNull.Value;

            ds.Tables[0].Rows.Add(newRow);

            SqlWork.updateData(ds, "SELECT * FROM COMMON");
        }

        public static void UpdateEvent(Event eventOld, Event eventNew)
        {
            var ds = SqlWork.selectData("SELECT * FROM COMMON");

            DataRow newRow = ds.Tables[0].Select("EventNumber="+ eventOld.EventNumber).FirstOrDefault();

            newRow["Timestamp"] = eventNew.Timestamp;
            newRow["EventNumber"] = eventOld.EventNumber;
            newRow["Severity"] = eventNew.Severity;
            newRow["EventID"] = eventNew.EventID;
            newRow["EventName"] = eventNew.EventName;
            newRow["ServerID"] = eventNew.ServerID;
            newRow["ServerName"] = eventNew.ServerName;
            newRow["DeviceID"] = eventNew.DeviceID;
            newRow["DeviceName"] = eventNew.DeviceName;
            newRow["DataID"] = eventNew.DataID;
            newRow["UserID"] = eventNew.UserID;

            SqlWork.updateData(ds, "SELECT * FROM COMMON");

        }


        public static void DeleteEvent(Event eventdel)
        {
            var ds = SqlWork.selectData("SELECT * FROM COMMON");
            ds.Tables[0].Rows.Find(eventdel.EventNumber).Delete();

            SqlWork.updateData(ds, "SELECT * FROM COMMON");

        }


        public static DataTable FromOBtoDT<T>(ObservableCollection<T> data)
        {
            PropertyDescriptorCollection properties =
                TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;
        }

        public static ObservableCollection<Event> FromDTtoOC(DataSet ds)
        {
            var SelectEvent = ds.Tables[0].AsEnumerable().Select(dataRow => new Event
            {
                Timestamp = dataRow.Field<string>("Timestamp"),
                EventNumber = dataRow.Field<int>("EventNumber"),
                Severity = dataRow.Field<string>("Severity"),
                EventID = dataRow.Field<int?>("EventID"),
                EventName = dataRow.Field<string>("EventName"),
                DeviceID = dataRow.Field<int?>("DeviceID"),
                ServerID = dataRow.Field<string>("ServerID"),
                ServerName = dataRow.Field<string>("ServerName"),
                DeviceName = dataRow.Field<string>("DeviceName"),
                DataID = dataRow.Field<int?>("DataID"),
                UserID = dataRow.Field<int?>("UserID")

            }).ToList();

            var ret = new ObservableCollection<Event>(SelectEvent);
            return ret;

        }



        public string Timestamp { get => timestamp; set => timestamp = value; }
        public int EventNumber { get => eventNumber; set => eventNumber = value; }
        public string Severity { get => severity; set => severity = value; }
        public int? EventID { get => eventID; set => eventID = value; }
        public string EventName { get => eventName; set => eventName = value; }
        public string ServerID { get => serverID; set => serverID = value; }
        public string ServerName { get => serverName; set => serverName = value; }
        public int? DeviceID { get => deviceID; set => deviceID = value; }
        public string DeviceName { get => deviceName; set => deviceName = value; }
        public int? DataID { get => dataID; set => dataID = value; }
        public int? UserID { get => userID; set => userID = value; }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public static implicit operator ObservableCollection<object>(Event v)
        {
            throw new NotImplementedException();
        }
    }
}

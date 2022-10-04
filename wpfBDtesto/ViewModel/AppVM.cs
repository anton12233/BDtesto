using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Data;
using wpfBDtesto.Model;
using wpfBDtesto.View;

namespace wpfBDtesto.ViewModel
{
    internal class AppVM : INotifyPropertyChanged
    {
        private ObservableCollection<Event> eventGrid = Event.GetAllEvents();

        private Event selectEvent;

        public ObservableCollection<Event> EventGrid { get { return eventGrid; } set { eventGrid = value; OnPropertyChanged("EventGrid"); } }

        public Event SelectEvent { get => selectEvent; set { selectEvent = value; OnPropertyChanged("SelectEvent"); } }


        private string timestampInsert;
        public string TimestampInsert { get => timestampInsert; set { timestampInsert = value; OnPropertyChanged("timestampInsert"); } }

        private int eventNumberInsert;
        public int EventNumberInsert { get => eventNumberInsert; set { eventNumberInsert = value; OnPropertyChanged("SeverityInsert"); } }

        private int? eventIDInsert;
        public int? EventIDInsert { get => eventIDInsert; set { eventIDInsert = value; OnPropertyChanged("EventIDInsert"); } }





        public AppVM()
        {

            EventGrid = Event.GetAllEvents();
            OnPropertyChanged();

        }



        private RelayCommand openInsertEventWidnow;
        public RelayCommand OpenInsertEventWidnow
        {
            get
            {
                return openInsertEventWidnow ?? new RelayCommand(obj =>
                {
                    InsertEventWindow insertEventWindow = new InsertEventWindow();
                    insertEventWindow.Owner = Application.Current.MainWindow;
                    insertEventWindow.ShowDialog();

                    EventGrid = Event.GetAllEvents();
                    EventGrid.CollectionChanged += EventGrid_CollectionChanged;


                });
            }
        }



        private RelayCommand insertEvent;
        public RelayCommand InsertEvent
        {
            get {
                return insertEvent ?? new RelayCommand(obj =>
                {
                    
                    Window window = obj as Window;
                    Event ev = new Event
                    {
                        Timestamp = TimestampInsert,
                        EventNumber = EventNumberInsert,
                        EventID = EventIDInsert,
                    };

                    Event.InsertEvent(ev);

                    TimestampInsert = "";
                    EventNumberInsert = 0;
                    EventIDInsert = null;

                    window.Close();
                    
                }

                );
                }
        }



        private RelayCommand updateEvent;
        public RelayCommand UpdateEvent
        {
            get
            {
                return updateEvent ?? new RelayCommand(obj =>
                {

                });
            }
        }


        private RelayCommand deleteEvent;
        public RelayCommand DeleteEvent
        {
            get {
                return deleteEvent ?? new RelayCommand(obj =>
                {
                    Event.DeleteEvent(selectEvent);
                    eventGrid.Remove(SelectEvent);
                    OnPropertyChanged();
                });
                }
        }



        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        private void EventGrid_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

    }
}

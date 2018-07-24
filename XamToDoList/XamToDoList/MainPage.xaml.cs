using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace XamToDoList
{
    public partial class MainPage : ContentPage
    {
        public ObservableCollection<string> ToDoList { get; set; }

        private object listSelectedItem;
        public object ListSelectedItem
        {
            get { return listSelectedItem; }
            set { listSelectedItem = value; OnPropertyChanged(); ((Command)DeleteItem).ChangeCanExecute(); }
        }


        private string toDoItem = "";
        public string ToDoItem
        {
            get { return toDoItem; }
            set { toDoItem = value; OnPropertyChanged(); ((Command)AddItem).ChangeCanExecute(); }
        }

        private ICommand addItem;
        public ICommand AddItem
        {
            get
            {
                if (addItem is null)
                {
                    addItem = new Command(
                        () =>
                        {
                            ToDoList.Add(ToDoItem);
                        },
                        () =>
                        {
                            if (ToDoItem == "")
                                return false;
                            else
                                return true;

                        });
                }

                return addItem;
            }
        }

        private ICommand deleteItem;
        public ICommand DeleteItem
        {
            get
            {
                if (deleteItem is null)
                {
                    deleteItem = new Command(
                        () =>
                        {
                            ToDoList.Remove(ListSelectedItem.ToString());
                            ListSelectedItem = null;
                            ((Command)DeleteItem).ChangeCanExecute();
                        },
                        () => 
                        {
                            if (ListSelectedItem == null)
                                return false;
                            else
                                return true;
                        }); 
                }

                return deleteItem;
            }
        }


        public MainPage()
        {
            InitializeComponent();
            ToDoList = new ObservableCollection<string>();
            BindingContext = this;
        }
    }
}

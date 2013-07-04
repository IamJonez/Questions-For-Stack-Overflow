using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace DataBindingQuestionForSourceForge.Models
{
	

	public class ColumnData : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
		private void NotifyPropertyChanged(string propertyName)
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
		}

		string _name;
		public string Name
		{
			get { return _name; }
			set 
			{ 
				_name = value;
				NotifyPropertyChanged("Name");
			}
		}

		int _id;
		public int ID
		{
			get { return _id; }
			set 
			{ 
				_id = value;
				NotifyPropertyChanged("ID");
			}
		}

		FirstColumn _firstColumn;
		public FirstColumn FirstColumn
		{
			get { return _firstColumn; }
			set 
			{ 
				_firstColumn = value;
				NotifyPropertyChanged("FirstColumn");
			}
		}

		public ColumnData() { }
		public ColumnData(string name, int id, FirstColumn firstColumn)
		{
			this.Name = name;
			this.ID = id;
			this.FirstColumn = FirstColumn;
		}
	}

	public class FirstColumn : INotifyPropertyChanged 
	{
		public event PropertyChangedEventHandler PropertyChanged;
		private void NotifyPropertyChanged(string propertyName)
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
		}

		public FirstColumn(){}
		public FirstColumn(string name, int id)
		{
			this.Name = name;
			this.ID = id;
		}

		string _name;
		public string Name
		{
			get { return _name; }
			set 
			{ 
				_name = value;
				NotifyPropertyChanged("Name");
			}
		}

		int _id;
		public int ID
		{
			get { return _id; }
			set 
			{ 
				_id = value;
				NotifyPropertyChanged("ID");
			}
		}

		ObservableCollection<ColumnData> _columnDataCollection;
		public ObservableCollection<ColumnData> ColumnDataCollection
		{
			get { return _columnDataCollection; }
			set 
			{ 
				_columnDataCollection = value;
				NotifyPropertyChanged("ColumnDataCollection");
			}
		}

		public void AddColumnData(ColumnData newColumnData)
		{
			if (_columnDataCollection == null)
				_columnDataCollection = new ObservableCollection<ColumnData>();

			_columnDataCollection.Add(newColumnData);

			if (_columnDataCollection != null)
			{
				if (newColumnData.FirstColumn == null || newColumnData.FirstColumn != this)
				{
					newColumnData.FirstColumn = this;
				}
			}
		}
	}

	public class MainWindowViewModel
	{
		public ICollectionView theCollection
		{
			get;
			private set;
		}

		public MainWindowViewModel()
		{
			//instantiate the main instance
			var _theCollection = new ObservableCollection<Models.FirstColumn>();

			for (int i = 1; i < 11; i++)
			{
				var firstColumnItem = new Models.FirstColumn("FirstColumnItem " + i, i);
				for (int j = 1; j < 11; j++)
				{
					var columnData = new Models.ColumnData("ColumnData " + i + j, j, firstColumnItem);
					firstColumnItem.AddColumnData(columnData);
				}
				_theCollection.Add(firstColumnItem);
			}

			theCollection = CollectionViewSource.GetDefaultView(_theCollection);

		}
	}
}

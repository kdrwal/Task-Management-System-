using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TaskManagerWPF.Helpers;

namespace TaskManagerWPF.ViewModels
{
    public class WorkspaceViewModel : BaseViewModel
    {
        #region FieldsAndProperties
        //each tab has display name and close command
        public string DisplayName { get; set; } //this name of the tab
        private BaseCommand _CloseCommand; //this is command to close a tab

        public WorkspaceViewModel(string displayName)
        {
            DisplayName = displayName;
            
        }

        public ICommand CloseCommand
        {
            get
            {
                if (_CloseCommand == null)
                    _CloseCommand = new BaseCommand(() => this.OnRequestClose()); //this command calls method to close a tab
                return _CloseCommand;
            }
        }
        #endregion

        #region RequestClose [event]
        public event EventHandler RequestClose;
        public void OnRequestClose()
        {
            EventHandler handler = this.RequestClose;
            if (handler != null)
                handler(this, EventArgs.Empty);
        }
        #endregion 

    }
}

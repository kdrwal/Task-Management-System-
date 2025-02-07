using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TaskManagerWPF.ViewModels
{
    public class CommandViewModel : BaseViewModel
    {
        #region FieldAndProperties

        /// <summary>
        /// This is name of the button in sidebar
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Every button contains Command, that opens tab
        /// </summary>
        public ICommand Command { get; set; }

        #endregion

        #region Konstruktor

        public CommandViewModel(string displayName, ICommand command)
        {
            if (command == null)
                throw new ArgumentNullException("Command");
            this.DisplayName = displayName;
            this.Command = command;
        }

        #endregion
    }
}

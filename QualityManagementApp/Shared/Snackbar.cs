using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QualityManagementApp.Shared
{
    public class Snackbar
    {
        public bool SnackbarIsOpen { get; set; }
        public string Message { get; set; } = null!;
    }
}

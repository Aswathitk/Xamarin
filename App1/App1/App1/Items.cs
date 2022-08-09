using System;
using System.Collections.Generic;
using System.Text;

namespace App1
{
    class Items
    {

        public string Text { get; set; }
        public bool Complete { get; set; }

        public Items(string Text, bool Complete)
        {
            this.Text = Text;
            this.Complete = Complete;
        }
    }
}

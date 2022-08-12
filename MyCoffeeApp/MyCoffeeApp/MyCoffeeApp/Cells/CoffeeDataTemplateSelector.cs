using MyCoffeeApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MyCoffeeApp.Cells
{
    public class CoffeeDataTemplateSelector : DataTemplateSelector
    {
        public CoffeeDataTemplateSelector()
        {

        }
        public DataTemplate Normal { set; get; }
        public DataTemplate Special { set; get; }
        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            var coffee = (Coffee)item;
            return coffee.Roaster == "Yes Please" ? Special : Normal;
        }
    }
}

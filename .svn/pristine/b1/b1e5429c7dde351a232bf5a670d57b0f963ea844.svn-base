using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NordCar.WebAPI.Models
{
    public class BaseControl
    {
        public virtual string TypeName { get; set; }
        public virtual string ProductId { get; set; }
        public virtual string Description { get; set; }
        public virtual string TeaserTxt { get; set; }
        

    }

    public class ddlItem
    {
        public string ElementId { get; set; }
        public string ElementText { get; set; }
        public string ElementPrice { get; set; }
       
    }

    public class Checkbox : BaseControl
    {
        public override string TypeName { get; set; }
        public override string ProductId { get; set; }
        public override string Description { get; set; }
        public override string TeaserTxt { get; set; }
        public string Price { get; set; }
        public bool Enabled { get; set; }

    }

    public class DropDownList : BaseControl
    {
        public override string TypeName { get; set; }
        public override string ProductId { get; set; }
        public override string Description { get; set; }
        public override string TeaserTxt { get; set; }
        public int NoOfElements { get; set; }
        public string SelectedElement { get; set; }
        public List<ddlItem> ddlItems { get; set; }
    }

    public class DropDown : BaseControl
    {
        public override string TypeName { get; set; }
        public override string ProductId { get; set; }
        public override string Description { get; set; }
        public override string TeaserTxt { get; set; }
        public string Price { get; set; }
        public bool Enabled { get; set; }
        public int Max { get; set; }
        public int Min { get; set; }
        public int SelectedValue { get; set; }
    }


}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BarReporting.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class Ingredient
    {
        public int IngredientId { get; set; }
        public Nullable<int> ProductId { get; set; }
        public Nullable<double> Qty { get; set; }
        public Nullable<double> CostPrice { get; set; }
        public Nullable<int> FoodMenuId { get; set; }
    
        public virtual Ingredient Ingredients1 { get; set; }
        public virtual Ingredient Ingredient1 { get; set; }
    }
}

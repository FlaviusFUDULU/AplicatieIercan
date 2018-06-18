using AplicatieSalariati.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicatieSalariati.ViewModels
{
    public class ConcediiViewmodel
    {
        public DateAngajatModel Salariat { get; set; }
        public DateManagerModel Manager { get; set; }
        public List<DateManagerModel> Manageri { get; set; }
        public List<DateAngajatModel> Salariati { get; set; }

        public List<ConcediiModel> Concedii { get; set; }

        //public double TotalSalarNeg {
        //    get {
        //        double total = 0;
        //        foreach (var salariat in Salariati) {
        //            total += salariat.Salar_Brut;
        //        }
        //        return total;
        //    }
        //}

        //public double TotalRestPlata {
        //    get
        //    {
        //        double total = 0;
        //        foreach (var salariat in Salariati)
        //        {
        //            total += salariat.RestPlata;
        //        }
        //        return total;
        //    }
        //}
    }
}

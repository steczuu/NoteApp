using NoteApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteApp.Services
{
    public class PickerService
    {
        public static List<Categories> GetCategories()
        {
            var categories = new List<Categories>()
            {
                new Categories() {Key = 1, Value="Important"},
                new Categories() {Key = 2, Value="Class note"},
                new Categories() {Key = 3, Value="Press note"},
                new Categories() {Key = 4, Value="Dictionary note"},
                new Categories() {Key = 5, Value="Scribal note"}
            };  
            
            return categories;
        }
    }
}

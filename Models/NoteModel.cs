using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteApp.Models
{
    [PrimaryKey(nameof(Id))]
    public class NoteModel
    {
        public int Id { get; set; } 
        public string Title { get; set; }
        public string NoteInput { get; set; }    
        public string Category { get; set; }    
        public DateTime NoteDate { get; set; }  
    }
}

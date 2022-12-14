using Makale_DAL;
using Makale_Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Makale_BLL
{
    public class NoteBL
    {
        Repository<Note> repoNote = new Repository<Note>();

        public List<Note> ReadNotes()
        {
            return repoNote.Read();
        }

        
    }
}

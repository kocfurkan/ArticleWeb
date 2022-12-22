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
		ResponsesBL<Note> response = new ResponsesBL<Note>();
		public List<Note> ReadNotes()
		{
			return repoNote.Read();
		}
		public IQueryable<Note> ReadNotesQueryable()
		{
			return repoNote.ReadQueryable();
		}

		public Note GetNoteById(int? id)
		{
			return repoNote.Find(x => x.Id == id);
		}

		public void SaveNote(Note note)
		{
			//response.Obj =repoNote.Find(x=>x.Id == note.Id);
			//repoNote.Create(response.Obj);
			throw new NotImplementedException();
		}

		public void UpdateNote(Note note)
		{
			throw new NotImplementedException();
		}

		public void DeleteNote(Note note)
		{
			response.Obj=repoNote.Find(x=>x.Id==note.Id);
			throw new NotImplementedException();
		}
	}
}

using Article_DAL;
using Article_Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Article_BLL
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

		public ResponsesBL<Note> SaveNote(Note note)
		{
			response.Obj = repoNote.Find(x => x.Title == note.Title && x.CategoryId == note.CategoryId);
			if (response.Obj != null)
			{
				response.errors.Add("Note with the same name does already exists");
			}
			else
			{
				int result = repoNote.Create(note);
				if (result < 1)
				{
					response.errors.Add("Creation of note has failed");
				}
			}
			return response;
		}

		public ResponsesBL<Note> UpdateNote(Note note)
		{
			response.Obj = repoNote.Find(x => x.Id == note.Id);
			if (response.Obj != null)
			{
				response.Obj.Title = note.Title;
				response.Obj.Text = note.Text;
				response.Obj.CategoryId = note.CategoryId;
				response.Obj.Draft = note.Draft;
				if (repoNote.Update(response.Obj) < 1)
				{
					response.errors.Add("Failed to update the note");
				}
			}
			return response;
		}

		public ResponsesBL<Note> DeleteNote(Note note)
		{
			response.Obj = repoNote.Find(x => x.Id == note.Id);
			if(response.Obj == null)
			{
				response.errors.Add("Could not find the note");
			}else
			{
				repoNote.Delete(response.Obj);
			}
			return response;
		}
	}
}

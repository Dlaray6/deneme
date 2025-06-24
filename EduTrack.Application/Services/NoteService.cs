using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EduTrack.Application.Interfaces;

using EduTrack.Domain;
using Microsoft.EntityFrameworkCore;

namespace EduTrack.Application.Services
{
    public class NoteService : INoteService
    {
        private readonly List<Note> _dummyNotes = new();

        public async Task AddNoteAsync(Note note)
        {
            _dummyNotes.Add(note);
            await Task.CompletedTask;
        }

        public async Task UpdateNoteAsync(Note note)
        {
            var existing = _dummyNotes.FirstOrDefault(n => n.Id == note.Id);
            if (existing != null)
            {
                existing.Score = note.Score;
                existing.ClassUserClassId = note.ClassUserClassId;
                existing.ClassUserUserId = note.ClassUserUserId;
                existing.ClassLessonClassId = note.ClassLessonClassId;
                existing.ClassLessonLessonId = note.ClassLessonLessonId;
            }
            await Task.CompletedTask;
        }

        public async Task<List<Note>> GetNotesByStudentIdAsync(int studentId)
        {
            return await Task.FromResult(_dummyNotes
                .Where(n => n.ClassUserUserId == studentId)
                .ToList());
        }

        public async Task<List<Note>> GetNotesByLessonIdAsync(int lessonId)
        {
            return await Task.FromResult(_dummyNotes
                .Where(n => n.ClassLessonLessonId == lessonId)
                .ToList());
        }
    }
}

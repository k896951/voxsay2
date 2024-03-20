using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace voxsay2
{
    public class SubNoteInfo
    {
        public string Lyric { get; set; }

        public bool defaultLyric { get; set; }

        public int Key { get; set; }

        public string Note { get; set; }

        public string NoteLen { get; set; }

        public int Tempo { get; set; }

        public double Ticks { get; set; }

    }
}
